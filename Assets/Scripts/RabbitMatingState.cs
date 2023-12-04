//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitMatingState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {
        animal._currentTarget = animal.FindTarget("Rabbit");
    }

    public override void UpdateState(Rabbit animal)
    {
        //if rabbit has mated, or urgent need to eat or flee, change state
    }

    public override void OnCollisionEnter(Rabbit animal)
    {

    }
}
