//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SnakeFleeingState : SnakeAbstractState
{
    public override void EnterState(Snake animal)
    {
        Debug.Log("Snake entered fleeing state");

        animal._currentTarget = animal.FindTarget("Fox");
    }

    public override void UpdateState(Snake animal)
    {
        //if no longer need to flee, change state
        if (!animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Idle);
            return;
        }
        else
        {
            animal._currentTarget = animal.FindTarget("Fox");
        }

        animal.GoAwayFromTarget();
    }

    public override void OnCollisionEnter(Snake animal)
    {

    }
}
