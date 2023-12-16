//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FoxDrinkingState : FoxAbstractState
{
    public override void EnterState(Fox animal)
    {
        animal._currentTarget = animal.FindTarget("Water");
    }

    public override void UpdateState(Fox animal)
    {
        if (!animal.NeedsToDrink() || !animal.SeesWater())
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

    public override void OnCollisionEnter(Fox animal)
    {

    }
}
