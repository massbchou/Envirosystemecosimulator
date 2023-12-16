//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FoxIdleState : FoxAbstractState
{
    public override void EnterState(Fox animal)
    {
        Debug.Log("Fox entered idle state");

        animal.GetRandomTarget();
    }

    public override void UpdateState(Fox animal)
    {
        if (animal.NeedsToEat() && animal.SeesFood())
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

    public override void OnCollisionEnter(Fox animal)
    {

    }
}
