//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//abstract needed for state machine
public abstract class FoxAbstractState
{
    public abstract void EnterState(Fox animal);

    public abstract void UpdateState(Fox animal);

    public abstract void OnCollisionEnter(Fox animal);
}
