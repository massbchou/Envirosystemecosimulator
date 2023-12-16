//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RatDrinkingState : RatAbstractState
{
    public override void EnterState(Rat animal)
    {
        animal._currentTarget = animal.FindTarget("Water");
    }

    public override void UpdateState(Rat animal)
    {
        //if urgent need to flee, or Rat full, change state
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
            return;
        }
        else if (!animal.NeedsToDrink() || !animal.SeesWater())
        {
            animal.SwitchState(animal.Idle);
            return;
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindTarget("Water");
            if (animal._currentTarget == null)
            {
                animal.SwitchState(animal.Idle);
                return;
            }
        }

        //eat if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance)
        {
            //animal.eatPlant(animal._currentTarget); add drinking
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Rat animal)
    {

    }
}
