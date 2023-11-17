using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animal : MonoBehaviour
{

    protected NavMeshAgent _agent;
    [SerializeField] protected float _senseRadius = 10f;

    // Start is called before the first frame update
    protected void Start()
    {
        _agent = GetComponent<NavMeshAgent>();  
    }

    protected GameObject FindTarget(string desiredTag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _senseRadius);
        Collider closest = null;
        float closestDistance = float.MaxValue;
        foreach (Collider collider in hitColliders)
        {
            if (!collider.CompareTag(desiredTag)) continue;

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
