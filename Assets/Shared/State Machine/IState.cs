public interface IState
{
    void Enter();
    void Exit();
    void Update();
}

public interface IStateExtend
{
    void Enter(object data = null);
    void Exit();
    void Update();
}