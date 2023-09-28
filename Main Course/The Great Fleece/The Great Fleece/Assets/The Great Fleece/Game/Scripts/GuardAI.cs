using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;
    private NavMeshAgent _agent;
    private Animator _animator;
    [SerializeField] private int currentTarget;
    private int wayPointCount;
    private bool reverse, _targetReached, coinTossed;
    private Vector3 coinPosition;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        wayPointCount = wayPoints.Count - 1;
    }
    void Update()
    {
        if (wayPoints[currentTarget] != null && !coinTossed)
        {
            _agent.SetDestination(wayPoints[currentTarget].position);

            float distance = Vector3.Distance(transform.position, wayPoints[currentTarget].position);
            if (distance < 2.0f && _targetReached == false && wayPointCount > 0)
            {
                _targetReached = true;
                StartCoroutine(WaitBeforeMoving());
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, coinPosition);
            if (distance < 5f)
            {
                _animator.SetBool("Walk",false);
                transform.LookAt(coinPosition);
            }
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        if (currentTarget == 0 || currentTarget == wayPointCount)
        {
            _animator.SetBool("Walk",false);
            float waitingTime = Random.Range(2, 5);
            yield return new WaitForSeconds(waitingTime);
            _animator.SetBool("Walk",true);
        }
        
        _targetReached = false;
        if (reverse)
        {
            currentTarget--;
            if (currentTarget == 0)
            {
                reverse = false;
            }
        }
        else
        {
            currentTarget++;
            if (currentTarget == wayPointCount)
            {
                reverse = true;
            }
        }
    }

    public void SetCoinDestination(Vector3 coinPos)
    {
        _agent.stoppingDistance = 5.0f;
        coinPosition = coinPos;
        coinTossed = true;
        _agent.SetDestination(coinPosition);
        _animator.SetBool("Walk",true);
        _agent.stoppingDistance = 4.0f;
    }
}
