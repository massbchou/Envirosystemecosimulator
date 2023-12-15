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

    public bool isBurrowed = false;

    //getters and setters
    public string TargetTag { get { return _targetTag; } set { _targetTag = value; } }


    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();


        _ground = FindObjectOfType<Ground>();
        _currentTarget = null;

        _readyToMate = false;
        isMating = false;

        isBurrowed = false;
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

            if ( (string.Equals(desiredTag, "Rat") || string.Equals(desiredTag, "Rabbit")) && !this.CompareTag("Snake")) //only snakes can enter burrows to hunt
            {
                Animal other = collider.GetComponent<Animal>();
                if (other.isBurrowed) continue;
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
        return _currentTarget == null ||
            (_ground._corner1.x >= _currentTargetPosition.x) || (_ground._corner2.x <= _currentTargetPosition.x) ||
            (_ground._corner1.z >= _currentTargetPosition.z) || (_ground._corner4.z <= _currentTargetPosition.z);
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
        //change orientation of the plant based on the terrain
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }
    public void ClampTargetPositionToTerrain()
    {
        if (HasNoGoodTarget()) { return; }
        _currentTargetPosition.x = Mathf.Clamp(_currentTargetPosition.x, _ground.groundXMin, _ground.groundXMax);
        _currentTargetPosition.z = Mathf.Clamp(_currentTargetPosition.z, _ground.groundZMin, _ground.groundZMax);
        _currentTargetPosition.y = Terrain.activeTerrain.SampleHeight(_currentTargetPosition);

    }

    public float DistanceTo(Vector3 target)
    {
        return Vector3.Distance(transform.position, target);
    }

    public void GetRandomTarget()
    {
        _currentTarget = gameObject; //when the target isn't another game object, set it to the animal itself to fix HasNoGoodTargets()

        _currentTargetPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f) * _senseRadius, transform.position.y, transform.position.z + Random.Range(-1f, 1f) * _senseRadius);

        //Make sure x, y, and z are all within terrain
        ClampTargetPositionToTerrain();
        _agent.SetDestination(_currentTargetPosition);

    }
}
