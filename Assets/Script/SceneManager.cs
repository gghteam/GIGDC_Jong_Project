using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject player;
    public void DeadScene()
    {
        player.SetActive(false);
        //데드씬을 여기다 작업해야댐;
    }
}
