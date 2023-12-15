//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//abstract needed for state machine
public abstract class RatAbstractState
{
    public abstract void EnterState(Rat animal);

    public abstract void UpdateState(Rat animal);

    public abstract void OnCollisionEnter(Rat animal);
}