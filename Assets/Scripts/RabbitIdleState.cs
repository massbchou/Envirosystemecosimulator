//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitIdleState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {
        Debug.Log("Entered idle state");
        animal._currentTargetPosition = new Vector3(animal.transform.position.x + Random.Range(-1f, 1f) * animal._senseRadius, animal.transform.position.y, animal.transform.position.z + Random.Range(-1f, 1f) * animal._senseRadius);
        animal._agent.SetDestination(animal._currentTargetPosition);
    }

    public override void UpdateState(Rabbit animal)
    {
        //if rabbit needs to flee or eat, or rabbit has reached destination, pick other activity
        if (animal.NeedsToFlee())
        {
            animal.SwitchState(animal.Fleeing);
        }
        else if (animal.NeedsToEat())
        {
            animal.SwitchState(animal.Foraging);
        }
        else if (animal.WantsToMate())
        {
            animal.SwitchState(animal.Mating);
        }
        else if (animal.DistanceTo(animal._currentTargetPosition) < 2f)
        {
            animal._currentTargetPosition = new Vector3(animal.transform.position.x + Random.Range(-1f, 1f) * animal._senseRadius, animal.transform.position.y, animal.transform.position.z + Random.Range(-1f, 1f) * animal._senseRadius);
            animal._agent.SetDestination(animal._currentTargetPosition);
        }
    }

    public override void OnCollisionEnter(Rabbit animal)
    {
    
    }
}
