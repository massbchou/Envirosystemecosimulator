//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitForagingState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {
        animal._currentTarget = animal.FindTarget("Rabbit");
    }

    public override void UpdateState(Rabbit animal)
    {

    }

    public override void OnCollisionEnter(Rabbit animal)
    {

    }
}
