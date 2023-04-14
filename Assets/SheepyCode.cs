using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class SheepyCode : MonoBehaviour
{

    private NavMeshAgent _navMeshAgent;
    //public Transform otherPlayer;
    private bool Run;
    public float timer, wanderTime;
    public GameObject gate;
    
    public enum State
    {
        FindCharacter,
        Wander,
        Idle,
    }

    private State currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        CameraController.instance.sheep.Add(gameObject);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = State.FindCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.FindCharacter:
                    
                //SetAITargetLocation(otherPlayer.position);
                
                if (_navMeshAgent.remainingDistance < 1f && _navMeshAgent.remainingDistance > 0.5f)
                {
                    currentState = State.Wander;
                 }
                
                break;
            
            case State.Wander:
                timer += Time.deltaTime;
                break;
            
            case State.Idle:

                break;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SetAITargetLocation(hit.point);    
            }
            
        }
        
        Wander();
    }

    private void SetAITargetLocation(Vector3 targetLocation)
    {
        _navMeshAgent.SetDestination(targetLocation);
    }

    private void Wander()
    {
        if (timer >= wanderTime)
        {
            Vector2 wanderTarget = Random.insideUnitCircle * wanderTime;
            Vector3 wanderPos3D = new Vector3(transform.position.x + wanderTarget.x, transform.position.y,
                z: transform.position.z + wanderTarget.y);
            SetAITargetLocation(wanderPos3D);

            timer += Time.deltaTime;
        }
    }

    public void SetState(string newState)
    {
        currentState = (State) Enum.Parse(typeof(State), newState) ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dog"))
        {
            _navMeshAgent.SetDestination(gate.transform.position);
        }
    }
}
