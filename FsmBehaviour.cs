using System;
using System.Collections.Generic;

public class FsmBehaviour<T>
{
    //进入事件
    private Action<T> enterCallback;
    //离开事件
    private Action<T> leaveCallback;
    //更新事件
    private Action<T> updateCallback;
    private T state;
    private Dictionary<int, Action<T>> conditionCallback;
    public FsmBehaviour(T s)
    {
        state = s;
        conditionCallback = new Dictionary<int, Action<T>>();
    }

    public FsmBehaviour<T> OnEnter(Action<T> callback)
    {
        enterCallback = callback;
        return this;
    }

    public FsmBehaviour<T> OnLeave(Action<T> callback)
    {
        leaveCallback = callback;
        return this;
    }

    public FsmBehaviour<T> OnUpdate(Action<T> callback)
    {
        updateCallback = callback;
        return this;
    }

    public FsmBehaviour<T> OnCondition(int conditionId, Action<T> callback)
    {
        conditionCallback.Add(conditionId, callback);
        return this;
    }

    public void TriggerEnter()
    {
        enterCallback?.Invoke(state);
    }

    public void TriggerLeave()
    {
        leaveCallback?.Invoke(state);
    }

    public void TriggerUpdate()
    {
        updateCallback?.Invoke(state);
    }

    public void TriggerCondition(int conditionId)
    {
        Action<T> action = conditionCallback.GetValueOrDefault(conditionId);
        action?.Invoke(state);
    }
}