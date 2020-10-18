using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] RuntimeData runtimeData;
    void stopAttack()
    {
        runtimeData.attacking = false;
        print("Stopped attack");
    }
}
