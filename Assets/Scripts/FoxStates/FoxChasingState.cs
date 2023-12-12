//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FoxChasingState : FoxAbstractState
{
    public override void EnterState(Fox animal)
    {
        Debug.Log("Fox entered chasing state");
    }

    public override void UpdateState(Fox animal)
    {
        //if no longer needs to eat, switch state
        if (!animal.NeedsToEat() || !animal.SeesFood())
        {
            animal.SwitchState(animal.Idle);
            return;
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindTarget("Rabbit");
            if (animal._currentTarget == null)
            {
                animal.SwitchState(animal.Idle);
            }
        }

        //eat if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance)
        {
            animal.eatRabbit(animal._currentTarget);
        }
    }

    public override void OnCollisionEnter(Fox animal)
    {

    }
}
