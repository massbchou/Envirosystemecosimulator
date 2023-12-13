//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FoxChasingState : FoxAbstractState
{
    GameObject newPossibleTarget = null;

    public override void EnterState(Fox animal)
    {
        Debug.Log("Fox entered chasing state");
        animal._currentTarget = animal.FindTarget("Rabbit");
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
        else //retarget if possible
        {
            newPossibleTarget = animal.FindTarget("Rabbit");
            if (newPossibleTarget != null && animal.DistanceTo(newPossibleTarget.transform.position) < animal.DistanceTo(animal._currentTarget.transform.position))
            {
                animal._currentTarget = newPossibleTarget;
            }
        }

        //eat if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance)
        {
            animal.eatRabbit(animal._currentTarget);
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Fox animal)
    {

    }
}
