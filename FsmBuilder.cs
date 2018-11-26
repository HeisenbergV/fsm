using System;
using System.Collections.Generic;

public class FsmBuilder<Tstate>
{
    private Dictionary<Tstate, FsmBehaviour<Tstate>> stateDic;
    private Tstate currentState;
    public FsmBuilder()
    {
        stateDic = new Dictionary<Tstate, FsmBehaviour<Tstate>>();
        currentState = default(Tstate);
    }
    
    public void Startup(Tstate state)
    {
        currentState = state;
        this.stateDic[currentState].TriggerEnter();
    }

    public FsmBehaviour<Tstate> Add(Tstate state)
    {
        if(stateDic.ContainsKey(state))
        {
            throw new Exception("repeat state key");
        }
        FsmBehaviour<Tstate> behaviour = new FsmBehaviour<Tstate>(state);
        stateDic.Add(state, behaviour);
        return behaviour;
    }

    public void ChangeState(Tstate state)
    {
        var behaviour = stateDic[currentState];
        if(behaviour == stateDic[state]) return;
        
        behaviour.TriggerLeave();
        currentState = state;
        stateDic[currentState].TriggerEnter();
    }

    public void TriggerCondition(int conditionId)
    {
        stateDic[currentState].TriggerCondition(conditionId);
    }

    public void Update()
    {
        stateDic[currentState].TriggerUpdate();
    }
}