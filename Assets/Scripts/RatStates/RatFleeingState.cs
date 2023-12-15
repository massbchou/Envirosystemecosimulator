//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RatFleeingState : RatAbstractState
{
    public override void EnterState(Rat animal)
    {
        Debug.Log("Rat entered fleeing state");
        animal._currentTarget = animal.FindRatPredator();
    }

    public override void UpdateState(Rat animal)
    {
        //if no longer need to flee, change state
        if (!animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Idle);
            return;
        }
        else if (animal.FindTarget("Burrow") && animal.DistanceTo(animal.FindTarget("Burrow").transform.position) < 5f)
        {
            animal.SwitchState(animal.Burrowing);
            return;
        }
        else
        {
            animal._currentTarget = animal.FindRatPredator();
            animal.GoAwayFromTarget();
        }
    }

    public override void OnCollisionEnter(Rat animal)
    {

    }
}
