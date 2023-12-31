//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitMatingState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {
        animal._currentTarget = animal.FindTarget("Rabbit");
    }

    public override void UpdateState(Rabbit animal)
    {
        if (animal.isMating)
        {
            return;
        }

        //if rabbit has mated, or urgent need to eat or flee, change state
        if (animal.NeedsToFlee())
        {
            animal._readyToMate = false;
            animal.SwitchState(animal.Fleeing);
            return;
        }
        else if (animal.BadlyNeedsToEat())
        {
            animal._readyToMate = false;
            animal.SwitchState(animal.Foraging);
            return;
        }
        else if (!animal.WantsToMate() || !animal.SeesMate())
        {
            animal.SwitchState(animal.Idle);
            return;
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindTarget("Rabbit");
        }

        //mate if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTarget.transform.position) < animal._eatingDistance)
        {
            //Get the other rabbit and check if it is also searching for a rabbit
            Rabbit other = animal._currentTarget.GetComponent<Rabbit>();
            if (other != null && GameObject.ReferenceEquals(other._currentTarget.gameObject, animal.gameObject) && other._readyToMate && !animal.isMating)
            {
                animal.StartCoroutine(animal.Mate(other));
                return;
            }
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Rabbit animal)
    {

    }
}
