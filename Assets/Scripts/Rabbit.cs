using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{
    [SerializeField] LayerMask mask;
    GameObject _currentTarget = null;
    Vector3 _currentTargetPosition;
    [SerializeField] float _eatingDistance = 1f;
    [SerializeField] float _maxBelly = 10f;
    private float belly;
    bool wandering = false;

    // Start is called before the first frame update
    void Start()
    {
        wandering = false;
        _currentTarget = null;
        base.Start();
        belly = _maxBelly;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentTarget == null)
        {
            _currentTarget = FindTarget("Plant");
            if(_currentTarget != null)
            {
                wandering = false;
                _currentTargetPosition = _currentTarget.transform.position;
                _agent.SetDestination(_currentTargetPosition);
            }
        }

        if (_currentTarget == null && !wandering)
        {
            wandering = true;
            _currentTargetPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f) * _senseRadius, transform.position.y, transform.position.z + Random.Range(-1f, 1f) * _senseRadius);
            _agent.SetDestination(_currentTargetPosition);
        }


        float dist = (transform.position - _currentTargetPosition).magnitude;
        if (dist < _eatingDistance)
        {
            if (!wandering)
            {
                //Grow in size by 11%
                transform.localScale *= 1.11f;
                //feed 
                belly = _maxBelly;
            }
            wandering = false;
            if(_currentTarget != null) Destroy(_currentTarget);
            _currentTarget = null;
            
        }
        
        //if scale reaches 2, split into two new rabbits
        if(transform.localScale.z > 2f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            GameObject newRabbit = Instantiate(gameObject, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            newRabbit.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            belly /= 2;
        }
        
        //decrease belly by time
        belly -= Time.deltaTime;
        
        if (belly < 0)
        {
            Destroy(gameObject);
        }
    }
}
