//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RabbitIdleState : RabbitAbstractState
{
    public override void EnterState(Rabbit animal)
    {
        Debug.Log("Entered idle state");
        animal._currentTarget = animal.gameObject; //when the target isn't another game object, set it to the animal itself to fix HasNoGoodTargets()
        animal._currentTargetPosition = new Vector3(animal.transform.position.x + Random.Range(-1f, 1f) * animal._senseRadius, animal.transform.position.y, animal.transform.position.z + Random.Range(-1f, 1f) * animal._senseRadius);
        animal._agent.SetDestination(animal._currentTargetPosition);
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
            animal._currentTarget = animal.gameObject;
            animal._currentTargetPosition = new Vector3(animal.transform.position.x + Random.Range(-1f, 1f) * animal._senseRadius, animal.transform.position.y, animal.transform.position.z + Random.Range(-1f, 1f) * animal._senseRadius);
            animal._currentTargetPosition.y = Terrain.activeTerrain.SampleHeight(animal._currentTargetPosition);
            animal._agent.SetDestination(animal._currentTargetPosition);
        }
    }

    public override void OnCollisionEnter(Rabbit animal)
    {
        
    }
}
