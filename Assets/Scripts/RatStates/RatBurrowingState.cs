//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RatBurrowingState : RatAbstractState
{
    public override void EnterState(Rat animal)
    {
        animal._currentTarget = animal.FindTarget("Burrow");
    }

    public override void UpdateState(Rat animal)
    {
        //if no longer need to flee, change state
        if (!animal.NeedsToFlee())
        {
            //leave burrow
            Vector3 pos = animal.transform.position;
            pos.y += 10f;
            animal.transform.position = pos;
            animal.isBurrowed = false;

            animal.SwitchState(animal.Idle);
            return;
        }
        else if (animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance)
        {
            //go into burrow
            Vector3 pos = animal.transform.position;
            pos.y -= 10f;
            animal.transform.position = pos;
            animal.isBurrowed = true;
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindTarget("Burrow");
        }
        else
        {
            animal.GoToTarget();
        }
    }

    public override void OnCollisionEnter(Rat animal)
    {

    }
}
