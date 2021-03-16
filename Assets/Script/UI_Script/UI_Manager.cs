using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause
{
    private static Pause instance;

    public static Pause Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new Pause();
            }

            return instance;
        }
    }

    public bool pause_Check = false;
}
public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject pause_UI = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause.Instance.pause_Check.Equals(false))
            {
                pause_UI.SetActive(true);
                Time.timeScale = 0f;

                Pause.Instance.pause_Check = true;
            }
            else if (Pause.Instance.pause_Check.Equals(true))
            {
                pause_UI.SetActive(false);
                Time.timeScale = 1f;

                Pause.Instance.pause_Check = false;
            }
        }
    }
}
