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

    //PowerUps
    public bool speedUp;
    public bool strengthUp;
    public bool healthUp;
    public bool noActive;
}
