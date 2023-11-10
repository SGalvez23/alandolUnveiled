using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State" )]
public class D_Idle : ScriptableObject
{
    public float minIdleTime = 1.0f;
    
    public float maxIdleTime = 3.0f;
}
