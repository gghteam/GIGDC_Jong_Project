using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Set_False : MonoBehaviour
{
    public void Pause_False()
    {
        Pause.Instance.pause_Check = false;
        Time.timeScale = 1f;
    }
}
