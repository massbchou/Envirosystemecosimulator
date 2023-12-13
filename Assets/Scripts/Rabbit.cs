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

    [SerializeField] public float _eatingDistance = 2f;
    [SerializeField] public float _maxBelly = 10f;
    public float belly; //was private

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

        //initialize to idle state
        currentState = Idle;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        //decrease belly by time
        belly -= Time.deltaTime;

        if (belly < 0)
        {
            Destroy(gameObject);
        }

        //call current state's update function each update
        currentState.UpdateState(this);
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

        belly /= 2;

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


    public bool NeedsToFlee()
    {
        return FindTarget("Fox");
    }

    public bool WantsToMate()
    {
        _readyToMate = transform.localScale.z >= 1.05f && !NeedsToFlee() && !BadlyNeedsToEat();
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
        if (!HasNoGoodTarget())
        {
            _currentTargetPosition = _currentTarget.transform.position;
            Vector3 directionToFox = _currentTargetPosition - transform.position;
            Vector3 directionAwayFromFox = directionToFox.normalized * -1.0f;
            _agent.SetDestination(transform.position + directionAwayFromFox);
        }
    }
}
