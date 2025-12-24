using UnityEngine;

public class ChaseState : IState
{
    private Enemy npc;
    private NavMeshMoveController moveController;

    public ChaseState(Enemy enemy, NavMeshMoveController moveController)
    {
        this.npc = enemy;
        this.moveController = moveController;
    }

    public void Enter()
    {
        Debug.Log(npc.gameObject.name + "Persiguiendo...");
        moveController.StartMovement();
    }

    public void Update(float deltaTime)
    {
        if (moveController.HasArrived())
        {
            npc.stateMachine.ChangeState(npc.stateMachine.SeekState);
        }
    }

    public void Exit()
    {
        moveController.StopMovement();
    }
}
