using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{
    GameObject _currentTarget = null;
    Vector3 _currentTargetPosition;
    [SerializeField] float _eatingDistance = 2f;
    [SerializeField] float _maxBelly = 10f;
    private float belly;
    bool wandering = false;

    [SerializeField] GameObject _rabbitPrefab; //baby to spawn

    [SerializeField] float _matingTime = 1f; //time it takes to mate

    public bool isMating = false; //variable that is true through the duration of the mate coroutine

    public GameObject CurrentTarget{ get { return _currentTarget; } set { _currentTarget = value; }} //getter and setter for current target 

    // Start is called before the first frame update
    void Start()
    {
        _targetTag = "Plant";
        wandering = false;
        _currentTarget = null;
        isMating = false;
        base.Start();
        belly = _maxBelly;
        _agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //decrease belly by time
        belly -= Time.deltaTime;

        if (belly < 0)
        {
            Destroy(gameObject);
        }

        //if scale reaches 2 and not very hungry, start searching for mate. Else search for food
        if (transform.localScale.z > 2f && belly > _maxBelly / 2)
        {
            _targetTag = "Rabbit";
        }
        else
        {
            _targetTag = "Plant";
        }

        //Don't update target or anything esle if you are mating
        if (isMating) return;

        //look for target if you don't have one, or if your target is moving
        if(_currentTarget == null || this.CompareTag(_targetTag))
        {
            _currentTarget = FindTarget(_targetTag);
            if(_currentTarget != null)
            {
                wandering = false;
                _currentTargetPosition = _currentTarget.transform.position;
                _agent.SetDestination(_currentTargetPosition);
            }
        }

        //no target found, so find a random position
        if (_currentTarget == null && !wandering)
        {

            wandering = true;
            _currentTargetPosition = new Vector3(0.5f + transform.position.x + Random.Range(-1f, 1f) * _senseRadius, transform.position.y, 0.5f + transform.position.z + Random.Range(-1f, 1f) * _senseRadius);
            _agent.SetDestination(_currentTargetPosition);
        }


        float dist = (transform.position - _currentTargetPosition).magnitude;
        if (dist < _eatingDistance)
        {
            //if searching for plant
            if (_currentTarget != null && _currentTarget.CompareTag("Plant"))
            {
                //Grow in size by 11%, but not over double
                transform.localScale = new Vector3(Mathf.Max(transform.localScale.x * 1.11f, 1), Mathf.Max(transform.localScale.y * 1.11f, 1), Mathf.Max(transform.localScale.z * 1.11f, 2));
                
                //feed 
                belly = _maxBelly;
                if (_currentTarget != null) Destroy(_currentTarget);
            }
            //if searching for mate
            else if(_currentTarget != null && _currentTarget.CompareTag("Rabbit")){
 
                //Get the other rabbit and check if it is also searching for a this rabbit
                Rabbit other = _currentTarget.GetComponent<Rabbit>();
                if(GameObject.ReferenceEquals(other.CurrentTarget, gameObject))
                {
                    StartCoroutine(Mate(other));
                    return;
                }
            }


            wandering = false;
            _currentTarget = null;

        }
        
    }

    //Spawn 0-3 new rabbits
    public IEnumerator Mate(Rabbit other)
    {
        isMating = true;

        _agent.enabled = false;
        _animator.SetBool("isMating", true);

        yield return new WaitForSeconds(_matingTime);

        for (int i = 0; i < Random.Range(1, 3); ++i)
        {
            GameObject newRabbit = Instantiate(_rabbitPrefab, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            newRabbit.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }

        belly /= 2;
        isMating = false;


        wandering = false;
        _currentTarget = null;

        _animator.SetBool("isMating", false);
        _agent.enabled = true;

    }
}
