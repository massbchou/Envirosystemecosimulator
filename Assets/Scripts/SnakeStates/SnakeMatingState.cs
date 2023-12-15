//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SnakeMatingState : SnakeAbstractState
{
    public override void EnterState(Snake animal)
    {
        Debug.Log("Snake entered mating state");
        animal._currentTarget = animal.FindTarget("Snake");
    }

    public override void UpdateState(Snake animal)
    {
        if (animal.isMating)
        {
            return;
        }

        //if rabbit has mated, or urgent need to eat or flee, change state
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
            return;
        }
        else if (animal.BadlyNeedsToEat())
        {
            animal._readyToMate = false;
            animal.SwitchState(animal.Chasing);
            return;
        }
        else if (!animal.WantsToMate() || !animal.SeesMate())
        {
            animal.SwitchState(animal.Idle);
            return;
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindTarget("Snake");
        }

        //mate if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTarget.transform.position) < animal._eatingDistance)
        {
            //Get the other rabbit and check if it is also searching for a rabbit
            Snake other = animal._currentTarget.GetComponent<Snake>();
            if (other != null && GameObject.ReferenceEquals(other._currentTarget.gameObject, animal.gameObject) && other._readyToMate && !animal.isMating)
            {
                animal.StartCoroutine(animal.Mate(other));
                return;
            }
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Snake animal)
    {

    }
}
