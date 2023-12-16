//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SnakeIdleState : SnakeAbstractState
{
    public override void EnterState(Snake animal)
    {
        Debug.Log("Snake entered idle state");
        animal.GetRandomTarget();
    }

    public override void UpdateState(Snake animal)
    {
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
            return;
        }
        else if (animal.NeedsToEat() && animal.SeesFood())
        {
            animal.SwitchState(animal.Chasing);
            return;
        }
        else if (animal.WantsToMate() && animal.SeesMate())
        {
            animal.SwitchState(animal.Mating);
            return;
        }
        else if (animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance || animal.HasNoGoodTarget())
        {
            animal.GetRandomTarget();
        }
    }

    public override void OnCollisionEnter(Snake animal)
    {

    }
}
