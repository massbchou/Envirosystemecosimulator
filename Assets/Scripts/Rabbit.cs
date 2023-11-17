using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{
    [SerializeField] LayerMask mask;
    GameObject _currentTarget = null;
    Vector3 _currentTargetPosition;
    [SerializeField] float _eatingDistance = 1f;

    bool wandering = false;

    // Start is called before the first frame update
    void Start()
    {
        wandering = false;
        _currentTarget = null;
        base.Start();
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
            //TODO: FIX THIS MATH TO BE RELATIVE TO THE RABBIT
            _currentTargetPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f) * _senseRadius, transform.position.y, transform.position.z + Random.Range(-1f, 1f) * _senseRadius);
            _agent.SetDestination(_currentTargetPosition);
        }


        float dist = (transform.position - _currentTargetPosition).magnitude;
        if (dist < _eatingDistance)
        {
            wandering = false;
            if(_currentTarget != null) Destroy(_currentTarget);
            _currentTarget = null;
        }
    }
}
