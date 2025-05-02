using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Steering 
{
    public float angular;
    public Vector2 linear;
     
    public Steering() 
    {
        angular = 0;
        linear = Vector2.zero;
    }
}
