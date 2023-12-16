//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//abstract needed for state machine
public abstract class SnakeAbstractState
{
    public abstract void EnterState(Snake animal);

    public abstract void UpdateState(Snake animal);

    public abstract void OnCollisionEnter(Snake animal);
}
