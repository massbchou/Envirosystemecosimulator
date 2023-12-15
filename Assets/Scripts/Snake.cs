using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Animal
{
    //stores state of snake
    SnakeAbstractState currentState;

    //all states a fox can be in
    public SnakeIdleState Idle = new SnakeIdleState();
    public SnakeChasingState Chasing = new SnakeChasingState();
    public SnakeFleeingState Fleeing = new SnakeFleeingState();
    public SnakeMatingState Mating = new SnakeMatingState();

    GameObject ground;
    //bool wandering = false;
    private float belly;
    [SerializeField] public float _eatingDistance = 3f;
    [SerializeField] float _maxBelly = 20f;

    [SerializeField] GameObject _snakePrefab; //baby to spawn

    [SerializeField] float _matingTime = 1f; //time it takes to mate

    public GameObject CurrentTarget { get { return _currentTarget; } set { _currentTarget = value; } } //getter and setter for current target 

    // Start is called before the first frame update
    void Start()
    {
        _targetTag = "Rabbit";
        //wandering = false;
        base.Start();
        _agent.enabled = true;
        belly = _maxBelly / 2;
        //ground = GameObject.Find("Ground");

        //initialize to idle state
        currentState = Idle;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        //call current state's update function each update
        currentState.UpdateState(this);

        belly -= Time.deltaTime;
        if (belly < 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SwitchState(SnakeAbstractState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void wander()
    {
        //wandering = true;
        _currentTargetPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f) * _senseRadius, transform.position.y, transform.position.z + Random.Range(-1f, 1f) * _senseRadius);
        _agent.SetDestination(_currentTargetPosition);
    }

    public IEnumerator Mate(Snake other)
    {
        Debug.Log("Snakes mating");
        isMating = true;
        other.isMating = true;

        _agent.enabled = false;
        other._agent.enabled = false;

        _animator.SetBool("isMating", true);
        other._animator.SetBool("isMating", true);

        yield return new WaitForSeconds(_matingTime);

        belly /= 2;
        other.belly /= 2;

        for (int i = 0; i < Random.Range(1, 3); ++i)
        {
            GameObject newRabbit = Instantiate(_snakePrefab, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            newRabbit.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        isMating = false;
        other.isMating = false;

        //_currentTarget = null;

        _animator.SetBool("isMating", false);
        other._animator.SetBool("isMating", false);

        _agent.enabled = true;
        other._agent.enabled = true;

    }

    public void eatRabbit(GameObject rabbit)
    {
        //kill rabbit and increase hunger
        Destroy(rabbit);

        belly += _maxBelly / 2;
        if (belly > _maxBelly) belly = _maxBelly;

        //grow in size by 11%, but not over double
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
        return FindTarget("Rabbit");
    }

    public GameObject FindSnakeFood()
    {
        return FindTarget("Rabbit");
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

    public bool WantsToMate()
    {
        _readyToMate = transform.localScale.z >= 1.05f && !BadlyNeedsToEat();
        return _readyToMate;
    }


    public bool SeesMate()
    {
        return FindTarget("Snake") != null;
    }

    public bool NeedsToFlee()
    {
        return FindTarget("Fox");
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

        //Vector3 zigzagOffset = zigzagReversed ? Vector3.Cross(directionAwayFromFox, Vector3.up).normalized * -1 : Vector3.Cross(Vector3.up, directionAwayFromFox).normalized * 1;

        _agent.SetDestination(transform.position + directionAwayFromFox);

    }
}
