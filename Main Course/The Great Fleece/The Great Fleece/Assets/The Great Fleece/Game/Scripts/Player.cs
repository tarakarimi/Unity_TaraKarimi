using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _target;
    public GameObject coinPrefab;
    private bool _coinTossed;
    void Start()
    {
    
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin,out hitInfo))
            {
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.transform.position = hitInfo.point;
                //Debug.Log("Hit point: " + hitInfo.point);
                _agent.SetDestination(hitInfo.point);
                _animator.SetBool("walk",true);
                _target = hitInfo.point;
                
            }
        }

        _target.y = transform.position.y;
        float distance = Vector3.Distance(transform.position, _target);
        if (distance < 1.5f)
        {
            _animator.SetBool("walk",false);
        }

        if (Input.GetMouseButtonDown(1) && _coinTossed == false)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin,out hitInfo))
            {
                _animator.SetTrigger("Throw");
                _coinTossed = true;
                Vector3 point = hitInfo.point;
                point.y = -1.86f;
                Instantiate(coinPrefab, point, Quaternion.identity);
                SendAIToCoinSpot(hitInfo.point);
                transform.LookAt(hitInfo.point);
            }
            
        }
    }

    void SendAIToCoinSpot(Vector3 coinPos)
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard1");
        foreach (var guard in guards)
        {
            GuardAI guardAi = guard.GetComponent<GuardAI>();
            guardAi.SetCoinDestination(coinPos);
        }
    }
    
}