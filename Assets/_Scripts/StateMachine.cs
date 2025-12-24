using UnityEngine;

public class StateMachine
{
    private IState currentState;

    private SeekState waitState;
    private ChaseState moveToPointState;

    public IState CurrentState { get { return currentState; } }

    public SeekState SeekState { get { return waitState; } }

    public ChaseState ChaseState { get { return moveToPointState; } }

    public StateMachine(Enemy enemy)
    {
        // Creamos una variable por cada estado
        waitState = new SeekState(enemy);
        moveToPointState = new ChaseState(enemy, enemy.navMeshMoveController);
    }

    public void Start(IState state)
    {
        currentState = state;
        state.Enter();
    }

    public void Stop()
    {
        currentState.Exit();
        currentState = null;
    }

    public void Update(float deltaTime)
    {
        if (currentState != null)
        {
            currentState.Update(deltaTime);
        }
    }

    public void ChangeState(IState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
