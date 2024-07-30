using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EmployeeMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform currentDestination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Transform destination)
    {
        currentDestination = destination;
        agent.SetDestination(destination.position);// + new Vector3(Random.Range(-2, +2),
                                                   //             0,
                                                   //             Random.Range(-2, +2)));
    }

    void Update()
    {
        if (currentDestination != null && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Arrived at the destination
            CompleteTask();
        }
    }

    private void CompleteTask()
    {
        // Task completion logic here
        currentDestination = null;
    }
}
