using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NewSheepScript : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;
    private bool onPursuit;
    private bool isFree;
    private float timer;
    public float wanderTime;
    public float wanderRad;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CameraController.instance.sheep.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(onPursuit)
            agent.SetDestination(target.position);
        else
        {
            timer += Time.deltaTime;
            Wander();
        }
        if (transform.position.z <= -60)
        {
            isFree = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dog") && !isFree)
        {
            target = CameraController.instance.gate.transform;
            onPursuit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("dog"))
        {
            onPursuit = false;
        }
    }

    void Wander()
    {
        wanderTime = Random.Range(5f, 15f);
        if (timer >= wanderTime)
        {
            Vector2 wanderTarget = Random.insideUnitCircle * wanderRad;
            Vector3 wanderPos3D = new Vector3(transform.position.x + wanderTarget.x, transform.position.y, transform.position.z + wanderTarget.y);
            if (isFree)
            {
                if (transform.position.z + wanderTarget.y > -50)
                {
                    wanderTarget = Random.insideUnitCircle * wanderRad;
                    wanderPos3D = new Vector3(transform.position.x + wanderTarget.x, transform.position.y, transform.position.z + wanderTarget.y);
                }
            }
            agent.SetDestination(wanderPos3D);
            timer = 0;
        }
    }
}
