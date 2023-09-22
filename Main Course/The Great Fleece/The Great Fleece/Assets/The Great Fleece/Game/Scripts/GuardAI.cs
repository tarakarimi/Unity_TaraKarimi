using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;
    private NavMeshAgent _agent;
    private Animator _animator;
    [SerializeField] private int currentTarget;
    private int wayPointCount;
    private bool reverse, _targetReached;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        wayPointCount = wayPoints.Count - 1;
    }
    void Update()
    {
        if (wayPoints[currentTarget] != null)
        {
            _agent.SetDestination(wayPoints[currentTarget].position);

            float distance = Vector3.Distance(transform.position, wayPoints[currentTarget].position);
            if (distance < 1.0f && _targetReached == false && wayPointCount > 0)
            {
                _targetReached = true;
                StartCoroutine(WaitBeforeMoving());
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
}
