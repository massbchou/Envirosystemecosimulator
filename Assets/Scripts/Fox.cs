using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{
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
    }

    // Update is called once per frame
    void Update()
    {
        // belly -= Time.deltaTime;
        // if (belly < 0)
        // {
        //     Destroy(gameObject);
        // }
        
        //Checks if fox is within eating distance of a rabbit, and if so, eats it
        GameObject _rabbit = OnRabbitWithinEatDistance();
        if (_rabbit != null)
        {
            eatRabbit(_rabbit);
        }

        //Find a target if you don't have one
        if(_currentTarget == null)
        {
            _currentTarget = FindTarget(_targetTag);
            if (_currentTarget != null)
            {
                wandering = false;
                _currentTargetPosition = _currentTarget.transform.position;
                _agent.SetDestination(_currentTargetPosition);
            }
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
           _currentTarget = null;
           wandering = false;
        }

            
        

    }

    private void wander()
    {
        wandering = true;
        bool outOfBounds = true;
        // find random position within bounds of ground if out of bounds find new position
        // while (outOfBounds)
        // {
        //     _currentTargetPosition = new Vector3(0.5f + transform.position.x + Random.Range(-1f, 1f) * _senseRadius, transform.position.y, 0.5f + transform.position.z + Random.Range(-1f, 1f) * _senseRadius);
        //     if (_currentTargetPosition.x < ground.transform.localScale.x && _currentTargetPosition.x > 0 && _currentTargetPosition.z < ground.transform.localScale.z && _currentTargetPosition.z > 0)
        //     {
        //         outOfBounds = false;
        //     }
        // }
        _currentTargetPosition = new Vector3(0.5f + transform.position.x + Random.Range(-1f, 1f) * _senseRadius, transform.position.y, 0.5f + transform.position.z + Random.Range(-1f, 1f) * _senseRadius);
        _agent.SetDestination(_currentTargetPosition);
    }

    private GameObject OnRabbitWithinEatDistance()
    {
        //Finds all game objects within eating distance
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _eatingDistance);
        float closestDistance = float.MaxValue;
        Collider closest = null;
        foreach (Collider collider in hitColliders)
        {
            if (this.gameObject == collider.gameObject || string.IsNullOrEmpty(collider.tag)) continue;
            if (!collider.CompareTag("Rabbit")) continue;
            if(_currentTarget == collider.gameObject){
                return collider.gameObject;
            }

            float dist = (transform.position - collider.transform.position).magnitude;
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = collider;
            }
        }
        if (closest == null) return null;
        return closest.gameObject;
    }

    private void eatRabbit(GameObject rabbit)
    {
        belly += 10f;
        if (belly > _maxBelly) belly = _maxBelly;
        Destroy(rabbit);

        //grow in size by 11%, but not over double
        transform.localScale = new Vector3(Mathf.Max(transform.localScale.x * 1.11f, 1), Mathf.Max(transform.localScale.y * 1.11f, 1), Mathf.Max(transform.localScale.z * 1.11f, 2));
        if (transform.localScale.z > 2)
        {
            transform.localScale = new Vector3(1f, 1f, 2f);
        }
        _currentTarget = null;
        
    }
}
