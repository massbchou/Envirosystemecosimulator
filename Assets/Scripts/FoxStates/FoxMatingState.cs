//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FoxMatingState : FoxAbstractState
{
    public override void EnterState(Fox animal)
    {
        Debug.Log("Fox entered mating state");
    }

    public override void UpdateState(Fox animal)
    {

    }

    public override void OnCollisionEnter(Fox animal)
    {

    }
}
