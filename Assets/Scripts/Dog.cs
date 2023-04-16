using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;
    private bool onPursuit;
    private float timer;
    public float wanderTime;
    public float wanderRad;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CameraController.instance.dogs.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (onPursuit)
        {
            agent.SetDestination(target.position);
            if (target.position.z < -50)
            {
                onPursuit = false;
            }
        }
        else
        {
            timer += Time.deltaTime;
            Wander();
        }
        if(transform.position.z <= -40)
        {
            agent.SetDestination(Vector3.zero);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sheep") && other.transform.position.z > -50)
        {
            target = other.transform;
            onPursuit = true;
        }
    }

    void Wander()
    {
        if (timer >= wanderTime)
        {
            
            Vector2 wanderTarget = Random.insideUnitCircle * wanderRad;
            Vector3 wanderPos3D = new Vector3(transform.position.x + wanderTarget.x, transform.position.y, transform.position.z + wanderTarget.y);
            if(transform.position.z + wanderTarget.y <= -50)
            {
                wanderTarget = Random.insideUnitCircle * wanderRad;
                wanderPos3D = new Vector3(transform.position.x + wanderTarget.x, transform.position.y, transform.position.z + wanderTarget.y);
            }
            else
                agent.SetDestination(wanderPos3D);
            timer = 0;
        }
    }

}
