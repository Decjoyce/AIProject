using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NewSheepScript : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform target;
    private bool onPursuit;
    private float timer;
    public float wanderTime;
    public float wanderRad;
    private bool Ischased;
    public GameObject gate;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CameraController.instance.dogs.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(onPursuit)
            agent.SetDestination(gate.transform.position);
        else
        {
            timer += Time.deltaTime;
            Wander();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dog"))
        {
            
            onPursuit = true;
        }
    }

    void Wander()
    {
        if (timer >= wanderTime)
        {
            
            Vector2 wanderTarget = Random.insideUnitCircle * wanderRad;
            Vector3 wanderPos3D = new Vector3(transform.position.x + wanderTarget.x, transform.position.y, transform.position.z + wanderTarget.y);
            agent.SetDestination(wanderPos3D);
            timer = 0;
        }
    }
}
