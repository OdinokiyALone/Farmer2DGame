using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class levelPoint : BaseDevice
{
    public string Level;
    public override void OnActivate()
    {
        Managers.Mission.GotoNext(ConstCollection.FOREST);
    }   
}
