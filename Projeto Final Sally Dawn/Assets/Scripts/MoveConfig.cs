using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveConfig 
{
    public string name;

    public float MaxOcioso = 10;
    [HideInInspector]
    [Range(0, 6)] public float QlAnimaOcioso;
    public float AnimaOcioso = 5;

    [Range(5, 15)] public float jumpforce = 12;

    [Range(5, 150)] public float walkSpeed = 10;
    [Range(5, 100)] public float crawlSpeed = 7;
    [Range(5, 100)] public float climbSpeed = 8;
    [Range(10, 30)] public float runSpeed;
    [Range(2, 10)] public float PullshSpeed = 5;
    [Range(0, 2)] public float currentSpeed = .5f;

    [Range(1, 7)] public float fallMultiplier = 4f;
    [Range(1, 7)] public float lowJumpMultiplier = 1.5f;
    
    [HideInInspector]
    public GameObject ModeloName;
}
