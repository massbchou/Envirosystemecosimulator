using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animal : MonoBehaviour
{

    public NavMeshAgent _agent; //was protected
    [SerializeField] public float _senseRadius = 10f; //was protected

    public string _targetTag; //was protected

    protected Animator _animator;

    public Ground _ground;

    public GameObject _currentTarget = null;
    public Vector3 _currentTargetPosition;

    public bool _readyToMate;
    public bool isMating = false; //variable that is true through the duration of the mate coroutine

    //getters and setters
    public string TargetTag { get { return _targetTag; } set { _targetTag = value; } }

    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();  
/*        _ground = GameObject.Find("Ground").GetComponent<Ground>();
*/
        _currentTarget = null;

        _readyToMate = false;
        isMating = false;
    }

    public GameObject FindTarget(string desiredTag) //was protected
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
                if (other == null) continue;
                if (!other._readyToMate) continue;
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

    public bool HasNoGoodTarget()
    {
        return _currentTarget == null; /* ||
            (_ground._corner1.x >= _currentTargetPosition.x) || (_ground._corner2.x <= _currentTargetPosition.x) ||
            (_ground._corner4.z >= _currentTargetPosition.z) || (_ground._corner1.z <= _currentTargetPosition.z);*/
    }

    public void GoToTarget()
    {
        if (!HasNoGoodTarget())
        {
            _currentTargetPosition = _currentTarget.transform.position;
            _agent.SetDestination(_currentTargetPosition);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
