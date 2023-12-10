//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitFleeingState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {

    }

    public override void UpdateState(Rabbit animal)
    {
        //if no longer need to flee, change state
        if (!animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Idle);
        }
    }

    public override void OnCollisionEnter(Rabbit animal)
    {

    }
}
