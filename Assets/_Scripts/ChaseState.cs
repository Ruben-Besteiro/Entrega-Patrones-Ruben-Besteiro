using UnityEngine;

public class ChaseState : IState
{
    private Enemy enemy;
    private NavMeshMoveController moveController;

    public ChaseState(Enemy enemy, NavMeshMoveController moveController)
    {
        this.enemy = enemy;
        this.moveController = moveController;
    }

    public void Enter()
    {
        Debug.Log(enemy.gameObject.name + "Persiguiendo...");
        moveController.StartMovement();
    }

    public void Update(float deltaTime)
    {
        if (moveController.HasArrived())
        {
            enemy.stateMachine.ChangeState(enemy.stateMachine.seekState);
        }
    }

    public void Exit()
    {
        moveController.StopMovement();
    }
}
