//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SnakeChasingState : SnakeAbstractState
{
    GameObject newPossibleTarget = null;

    public override void EnterState(Snake animal)
    {
        Debug.Log("Snake entered chasing state");
        animal._currentTarget = animal.FindSnakeFood();
    }

    public override void UpdateState(Snake animal)
    {
        //if no longer needs to eat, switch state
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
            return;
        }
        else if (!animal.NeedsToEat() || !animal.SeesFood())
        {
            animal.SwitchState(animal.Idle);
            return;
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindSnakeFood();
            if (animal._currentTarget == null)
            {
                animal.SwitchState(animal.Idle);
            }
        }
        else //retarget if possible
        {
            newPossibleTarget = animal.FindSnakeFood();
            if (newPossibleTarget != null && animal.DistanceTo(newPossibleTarget.transform.position) < animal.DistanceTo(animal._currentTarget.transform.position))
            {
                animal._currentTarget = newPossibleTarget;
            }
        }

        //eat if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance)
        {
            if (animal._currentTarget.CompareTag("Rabbit") || animal._currentTarget.CompareTag("Rat"))
            {
                animal.eatRabbit(animal._currentTarget);
            }

            //add code to act differently if eating a plant instead
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Snake animal)
    {

    }
}
