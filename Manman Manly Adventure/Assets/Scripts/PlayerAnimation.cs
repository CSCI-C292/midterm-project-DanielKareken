using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject swordHitBox;

    [SerializeField] RuntimeData runtimeData;

    void unlockPlayer()
    {
        runtimeData.animationLock = false;
    }

    void attackHitboxDisable() {
        swordHitBox.SetActive(false);
    }
}
