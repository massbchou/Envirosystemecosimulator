//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitIdleState : RabbitAbstractState
{


    public override void EnterState(Rabbit animal)
    {
        Debug.Log("Rabbit entered idle state");
        animal.GetRandomTarget();
    }

    public override void UpdateState(Rabbit animal)
    {
        //if rabbit needs to flee or eat, or rabbit has reached destination, pick other activity
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
            return;
        }
        else if (animal.NeedsToEat() && animal.SeesFood())
        {
            animal.SwitchState(animal.Foraging);
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

    public override void OnCollisionEnter(Rabbit animal)
    {
        
    }
}
