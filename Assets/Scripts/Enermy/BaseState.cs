public abstract class BaseState
{
    protected Enermy currentEnemy;
    public abstract void OnEnter(Enermy enermy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
