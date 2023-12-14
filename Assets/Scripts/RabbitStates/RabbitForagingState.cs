//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitForagingState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {
        animal._currentTarget = animal.FindTarget("Plant");
    }

    public override void UpdateState(Rabbit animal)
    {
        //if urgent need to flee, or rabbit full, change state
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
            return;
        }
        else if (!animal.NeedsToEat() || !animal.SeesFood())
        {
            animal.SwitchState(animal.Idle);
            return;
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindTarget("Plant");
            if (animal._currentTarget == null)
            {
                animal.SwitchState(animal.Idle);
                return;
            }
        }

        //eat if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance)
        {
            animal.eatPlant(animal._currentTarget);
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Rabbit animal)
    {

    }
}
