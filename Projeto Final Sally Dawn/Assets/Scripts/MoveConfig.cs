using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveConfig 
{
    public float MaxOcioso = 10;
    public float QlAnimaOcioso;
    public float AnimaOcioso = 5;

    public GameObject ModeloName;
    
    public float jumpforce = 10;

    public float walkSpeed = 10;
    public float crawlSpeed = 7;
    public float climbSpeed = 8;
    public float runSpeed=20;
    public float PullshSpeed=5;
    public float currentSpeed = .5f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


}
