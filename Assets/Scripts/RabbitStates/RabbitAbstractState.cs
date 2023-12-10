//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//abstract needed for state machine
public abstract class RabbitAbstractState
{
    public abstract void EnterState(Rabbit animal);

    public abstract void UpdateState(Rabbit animal);

    public abstract void OnCollisionEnter(Rabbit animal);
}
