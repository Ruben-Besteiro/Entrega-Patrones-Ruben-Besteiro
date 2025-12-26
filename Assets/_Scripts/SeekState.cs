using System;
using Unity.Hierarchy;
using UnityEngine;

public class SeekState : IState
{
    private Enemy enemy;
    private bool lookAtPlayer;
    GameObject player;
    Vector3 playerPos;
    Quaternion playerRot;

    public SeekState(Enemy enemy)
    {
        this.enemy = enemy;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Enter()
    {
        // Nada
    }

    public void Update(float deltaTime)
    {
        if (lookAtPlayer)
        {
            if (player != null)
            {
                // Si nos movemos una cantidad microscópica por bugs de Unity no pasa nada
                if ((playerPos - player.transform.position).magnitude > 0.1f || Quaternion.Angle(playerRot, player.transform.rotation) > 0.1f)
                {
                    enemy.GetComponent<NavMeshMoveController>().enabled = true;
                    if (!GameManager.Instance.enemiesChasing) GameManager.Instance.enemiesChasing = true;         // Con esto paramos el reloj
                    enemy.stateMachine.ChangeState(enemy.stateMachine.chaseState);
                }
            }
        }
    }

    public void OnNotify(bool lookAtPlayer)
    {
        this.lookAtPlayer = lookAtPlayer;

        // Cuando el GM nos dice que toca mirar al jugador, hacemos eso, y viceversa
        if (lookAtPlayer)
        {
            // Almacenamos la posición del jugador en el momento en el que empezaron a mirar
            playerPos = player.transform.position;
            playerRot = player.transform.rotation;
            enemy.transform.LookAt(playerPos);
        } else
        {
            enemy.transform.LookAt(enemy.pared.transform);
        }
    }

    public void Exit()
    {
        // Nothing
    }
}