using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitStateContext : Rabbit
{
    RabbitAbstractState currentState;

    //all states a rabbit can be in
    public RabbitIdleState RabbitIdle = new RabbitIdleState();
    public RabbitMatingState RabbitMating = new RabbitMatingState();
    public RabbitFleeingState RabbitFleeing = new RabbitFleeingState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = RabbitIdle;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(RabbitAbstractState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
