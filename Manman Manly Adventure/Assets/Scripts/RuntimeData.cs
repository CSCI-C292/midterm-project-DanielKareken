using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New RuntimeData")]

public class RuntimeData : ScriptableObject
{
    //Player class
    public bool attacking;
    public bool animationLock;
    public bool facingRight;
    public bool hurt;
    public bool fireballOnCD;

    //Power up
    public bool strengthUp;
    public bool powerUpActive;

    //Win cond
    public bool pacifist;
    public string winCond;
    public bool gameOver;
}
