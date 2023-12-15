//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RatMatingState : RatAbstractState
{
    public override void EnterState(Rat animal)
    {
        Debug.Log("Rat entered mating state");
        animal._currentTarget = animal.FindTarget("Rat");
    }

    public override void UpdateState(Rat animal)
    {
        if (animal.isMating)
        {
            return;
        }

        //if Rat has mated, or urgent need to eat or flee, change state
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
            animal._currentTarget = animal.FindTarget("Rat");
        }

        //mate if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTarget.transform.position) < animal._eatingDistance)
        {
            //Get the other Rat and check if it is also searching for a Rat
            Rat other = animal._currentTarget.GetComponent<Rat>();
            if (other != null && GameObject.ReferenceEquals(other._currentTarget.gameObject, animal.gameObject) && other._readyToMate && !animal.isMating)
            {
                animal.StartCoroutine(animal.Mate(other));
                return;
            }
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Rat animal)
    {

    }
}
