using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Animator _animator;

    private Vector3 _target;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin,out hitInfo))
            {
                //print("hit: "+hitInfo.point);
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.transform.position = hitInfo.point;
                _agent.SetDestination(hitInfo.point);
                _animator.SetBool("walk",true);
                _target = hitInfo.point;
            }
        }

        float distance = Vector3.Distance(transform.position, _target);
        if (distance < 1)
        {
            _animator.SetBool("walk",false);
        }
    }
}
