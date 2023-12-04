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

        //mate if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance)
        {
            //Get the other rabbit and check if it is also searching for a this rabbit
            Rabbit other = animal._currentTarget.GetComponent<Rabbit>();
            if (GameObject.ReferenceEquals(other.CurrentTarget, animal))
            {
                animal.StartCoroutine(animal.Mate(other));
                //return;
            }
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Rabbit animal)
    {

    }
}
