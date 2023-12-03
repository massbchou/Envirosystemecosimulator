//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitMatingState : RabbitAbstractState
{
    public override void EnterState(RabbitStateContext animal)
    {
        //animal._currentTarget = animal.FindTarget("Rabbit");
    }

    public override void UpdateState(RabbitStateContext animal)
    {

    }

    public override void OnCollisionEnter(RabbitStateContext animal)
    {

    }
}
