using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animal : MonoBehaviour
{

    protected NavMeshAgent _agent;
    [SerializeField] protected float _senseRadius = 10f;

    protected string _targetTag;

    protected Animator _animator;

    //getters and setters
    public string TargetTag { get { return _targetTag; } set { _targetTag = value; } }

    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();  
    }

    protected GameObject FindTarget(string desiredTag)
    {
        if (string.IsNullOrEmpty(this.tag) || string.IsNullOrEmpty(desiredTag)) return null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _senseRadius);
        Collider closest = null;
        float closestDistance = float.MaxValue;
        foreach (Collider collider in hitColliders)
        {
            //ignore ourselves and ignore any tag thats not the desired one
            if (this.gameObject == collider.gameObject || string.IsNullOrEmpty(collider.tag)) continue;
            if (!collider.CompareTag(desiredTag)) continue;

            //if the tag we are searching for is our own tag, we must be finding a mate, so the other animal must also be searching for a mate
            if (this.CompareTag(desiredTag))
            {
                Animal other = collider.GetComponent<Animal>();
                if (other == null || string.IsNullOrEmpty(other.TargetTag)) continue;
                if (!this.CompareTag(other.TargetTag)) continue;
            }

            //get distance to find closest
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
        //change orientation of the plant based on the terrain
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }
}
