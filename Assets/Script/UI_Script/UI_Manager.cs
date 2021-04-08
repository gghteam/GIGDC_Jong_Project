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

    //일시정지 체크를 위한 싱글톤
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

    #region
    //버튼에서 오브젝트 키고 끄는 함수
    public void SetActive_false(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SetActive_true(GameObject obj)
    {
        obj.SetActive(true);
    }
    #endregion

    #region
    //퍼즈 끄는 함수
    public void Pause_False()
    {
        Pause.Instance.pause_Check = false;
        Time.timeScale = 1f;
    }
    #endregion
}

