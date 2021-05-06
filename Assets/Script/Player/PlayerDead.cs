using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public bool isDead;
    public void DeadScene()
    {
        //데드씬을 여기다 작업해야댐;
    }

    private void Awake()
    {
        isDead = false;
    }
}
