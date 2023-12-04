//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitMatingState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {
        Debug.Log("Entered mating state");
        animal._currentTarget = animal.FindTarget("Rabbit");
    }

    public override void UpdateState(Rabbit animal)
    {
        //if rabbit has mated, or urgent need to eat or flee, change state
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
        }
        else if (animal.NeedsToEat())
        {
            animal.SwitchState(animal.Foraging);
        }
        else if (!animal.WantsToMate())
        {
            animal.SwitchState(animal.Idle);
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindTarget("Rabbit");
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Rabbit animal)
    {

    }
}
