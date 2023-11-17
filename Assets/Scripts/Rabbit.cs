using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{
    [SerializeField] LayerMask mask;
    GameObject _currentTarget = null;



    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _currentTarget = FindTarget("Plant");
        Debug.Log(_currentTarget.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentTarget == null)
        {
            _currentTarget = FindTarget("Plant");
        }
        if(_currentTarget != null)
        {
            _agent.SetDestination(_currentTarget.transform.position);
        }
    }
}
