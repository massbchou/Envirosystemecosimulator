using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{
    //stores state of fox
    FoxAbstractState currentState;

    //all states a fox can be in
    public FoxIdleState Idle = new FoxIdleState();

    GameObject _currentTarget = null;

    GameObject ground;
    Vector3 _currentTargetPosition;
    bool wandering = false;
    private float belly;
    [SerializeField] float _eatingDistance = 3f;
    [SerializeField] float _maxBelly = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _targetTag = "Rabbit";
        wandering = false;
        _currentTarget = null;
        base.Start();
        _agent.enabled = true;
        belly = _maxBelly;
        ground = GameObject.Find("Ground");

        //initialize to idle state
        currentState = Idle;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        belly -= Time.deltaTime;
        if (belly < 0)
        {
             Destroy(gameObject);
        }

        //continuously update target (as rabbits are moving)
        _currentTarget = FindTarget(_targetTag);
        if (_currentTarget != null)
        {
            wandering = false;
            _currentTargetPosition = _currentTarget.transform.position;
            _agent.SetDestination(_currentTargetPosition);
        }
   

        //no target, wander
        if(_currentTarget == null && !wandering)
        {
            wander();
        }

        //get distance to target
        float distance = (_currentTargetPosition - transform.position).magnitude;
        //if we are close enough to target, eat or mate
        if (distance < _eatingDistance)
        {
            if(_currentTarget == null)
            {
                wander();
            }
            else
            {
                eatRabbit(_currentTarget);
            }
        }

            
        

    }

    public void SwitchState(FoxAbstractState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void wander()
    {
        wandering = true;
        _currentTargetPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f) * _senseRadius, transform.position.y, transform.position.z + Random.Range(-1f, 1f) * _senseRadius);
        _agent.SetDestination(_currentTargetPosition);
    }

    private void eatRabbit(GameObject rabbit)
    {
        //kill rabbit and increase hunger
        Destroy(rabbit);

        belly += 10f;
        if (belly > _maxBelly) belly = _maxBelly;

        //grow in size by 11%, but not over double
        transform.localScale = new Vector3(Mathf.Max(transform.localScale.x * 1.11f, 1), Mathf.Max(transform.localScale.y * 1.11f, 1), Mathf.Max(transform.localScale.z * 1.11f, 2));
        if (transform.localScale.z > 2)
        {
            transform.localScale = new Vector3(1f, 1f, 2f);
        }
        _currentTarget = null;
        wandering = false;
    }
}
