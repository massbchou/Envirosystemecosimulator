using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{
    //stores state of rabbit
    RabbitAbstractState currentState;

    //all states a rabbit can be in
    public RabbitIdleState Idle = new RabbitIdleState();
    public RabbitMatingState Mating = new RabbitMatingState();
    public RabbitFleeingState Fleeing = new RabbitFleeingState();
    public RabbitForagingState Foraging = new RabbitForagingState();
    public RabbitDrinkingState Drinking = new RabbitDrinkingState();

    [SerializeField] public float _eatingDistance = 2f;
    [SerializeField] public float _maxBelly = 10f;
    public float belly; //was private

    public bool zigzagReversed = false;
    public float zigzagTimer = 1.0f;

    [SerializeField] GameObject _rabbitPrefab; //baby to spawn

    [SerializeField] float _matingTime = 1f; //time it takes to mate

    public GameObject CurrentTarget { get { return _currentTarget; } set { _currentTarget = value; } } //getter and setter for current target 

    // Start is called before the first frame update
    void Start()
    {
        _targetTag = "Plant";
        base.Start();
        belly = _maxBelly / 2;
        _agent.enabled = true;

        zigzagReversed = false;
        zigzagTimer = 1.0f;
        
        bool isbrown = Random.Range(0, 5) == 0;
        if (isbrown)
        {
            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(0.5f, 0.25f, 0f);
            //vary shading by up to 10%
            float variance = Random.Range(-0.1f, 0.1f);
            GetComponentInChildren<SkinnedMeshRenderer>().material.color *= 1 - variance;
        }
        else
        {
            //set to a random shade of gray
            float gray = Random.Range(0f, 1f);
            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(gray, gray, gray);
        }

        //initialize to idle state
        currentState = Idle;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        //call current state's update function each update
        currentState.UpdateState(this);

        //decrease belly by time
        belly -= Time.deltaTime;
        if (belly < 0)
        {
            Destroy(gameObject);
        }

        zigzagTimer -= Time.deltaTime;
        if (zigzagTimer < 0)
        {
            zigzagReversed = !zigzagReversed;
            zigzagTimer += 1f;
        }
    }

    public void SwitchState(RabbitAbstractState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    //Spawn 0-3 new rabbits
    public IEnumerator Mate(Rabbit other)
    {
        Debug.Log("Rabbits mating");
        isMating = true;


        _agent.enabled = false;

        _animator.SetBool("isMating", true);

        yield return new WaitForSeconds(_matingTime);

        belly = belly / 2 - 1;

        for (int i = 0; i < Random.Range(1, 3); ++i)
        {
            GameObject newRabbit = Instantiate(_rabbitPrefab, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            newRabbit.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        isMating = false;
        _readyToMate = false;

        _animator.SetBool("isMating", false);

        _agent.enabled = true;

    }

    public void eatPlant(GameObject plant)
    {
        //destroy plant and increase hunger
        Destroy(plant.transform.parent.gameObject);

        belly += _maxBelly / 4;
        if (belly > _maxBelly) belly = _maxBelly;

        //grow in size by 5%, but not over double
        transform.localScale = new Vector3(Mathf.Min(transform.localScale.x * 1.05f, 2), Mathf.Min(transform.localScale.y * 1.05f, 2), Mathf.Min(transform.localScale.z * 1.05f, 2));
        if (transform.localScale.z > 2)
        {
            transform.localScale = new Vector3(1f, 1f, 2f);
        }
        _currentTarget = null;
    }

    public bool NeedsToEat()
    {
        return belly < _maxBelly / 2;
    }


    public bool BadlyNeedsToEat()
    {
        return belly < _maxBelly / 4;
    }


    public bool SeesFood()
    {
        return FindTarget("Plant") != null;
    }

    public bool NeedsToDrink()
    {
        return false; //eqivalent belly < _maxBelly / 2;
    }


    public bool BadlyNeedsToDrink()
    {
        return false; // belly < _maxBelly / 4;
    }


    public bool SeesWater()
    {
        return FindTarget("Water") != null;
    }


    public bool NeedsToFlee()
    {
        return FindTarget("Fox");
    }

    public bool WantsToMate()
    {
        _readyToMate = transform.localScale.z >= 1.05f && !NeedsToFlee() && !NeedsToEat();
        return _readyToMate;
    }


    public bool SeesMate()
    {
        return FindTarget("Rabbit") != null;
    }

    public float DistanceTo(Vector3 target)
    {
        return Vector3.Distance(transform.position, target);
    }

    public void GoAwayFromTarget()
    {
        if (HasNoGoodTarget()) { return; }
        
        _currentTargetPosition = _currentTarget.transform.position;
        Vector3 directionToFox = _currentTargetPosition - transform.position;
        Vector3 directionAwayFromFox = directionToFox.normalized * -1.0f;

        Vector3 zigzagOffset = zigzagReversed ? Vector3.Cross(directionAwayFromFox, Vector3.up).normalized * -1 : Vector3.Cross(Vector3.up, directionAwayFromFox).normalized * 1;

        _agent.SetDestination(transform.position + zigzagOffset + directionAwayFromFox);
        
    }
}
