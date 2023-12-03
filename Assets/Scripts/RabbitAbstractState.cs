//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//abstract needed for state machine
public abstract class RabbitAbstractState
{
    public abstract void EnterState(RabbitStateContext animal);

    public abstract void UpdateState(RabbitStateContext animal);

    public abstract void OnCollisionEnter(RabbitStateContext animal);
}
