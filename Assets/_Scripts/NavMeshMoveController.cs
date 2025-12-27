using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerPos;
    [SerializeField] public NavMeshAgent agent;

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
            try
            {
                // Hacemos una "destrucción falsa" del jugador
                MeshRenderer[] models = player.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer m in models)
                {
                    m.enabled = false;
                }
                player.GetComponent<Player>().DisableAllActions();
            } catch (Exception)
            {
                print("Excepción");
            }
            return true;
        }
        return false;
    }

    public void StopMovement()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
