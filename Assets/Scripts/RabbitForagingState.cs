//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitForagingState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {
        Debug.Log("Entered foraging state");
        animal._currentTarget = animal.FindTarget("Plant");
    }

    public override void UpdateState(Rabbit animal)
    {
        //if urgent need to flee, or rabbit full, change state
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
        }
        else if (!animal.NeedsToEat())
        {
            animal.SwitchState(animal.Idle);
        }
        else if (animal.HasNoGoodTarget())
        {
            animal._currentTarget = animal.FindTarget("Plant");
            //add: if it can't, switch to Idle
        }

        //eat if able
        if (!animal.HasNoGoodTarget() && animal.DistanceTo(animal._currentTargetPosition) < animal._eatingDistance)
        {
            //Grow in size by 11%, but not over double
            animal.transform.localScale = new Vector3(Mathf.Max(animal.transform.localScale.x * 1.11f, 1), Mathf.Max(animal.transform.localScale.y * 1.11f, 1), Mathf.Max(animal.transform.localScale.z * 1.11f, 2));

            //feed 
            animal.belly = animal._maxBelly;
            if (animal._currentTarget != null) Animal.Destroy(animal._currentTarget);//.transform.parent.gameObject);
        }

        //go to target
        animal.GoToTarget();
    }

    public override void OnCollisionEnter(Rabbit animal)
    {

    }
}
