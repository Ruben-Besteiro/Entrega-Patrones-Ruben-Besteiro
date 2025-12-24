using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveController : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private NavMeshAgent agent;
    private Vector3 destinationPoint;

    public void StartMovement()
    {
        agent.isStopped = false;
        agent.SetDestination(playerPos.position);
    }

    void Update()
    {
        // Actualiza el destino cada frame si el agente está en movimiento
        if (!agent.isStopped && playerPos != null)
        {
            agent.SetDestination(playerPos.position);
        }
    }

    public bool HasArrived()
    {
        if (agent == null) return false;

        // Usa la distancia restante del NavMeshAgent
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Destroy(playerPos.gameObject);
            return true;
        }
        return false;

        //return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    public void StopMovement()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
