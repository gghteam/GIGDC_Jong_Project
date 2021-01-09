using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject _MainUI = null;
    public GameObject background = null;
    public GameObject[] mainUI = null;
    public GameObject[] lordUI = null;
    public GameObject[] settingUI = null;
    public GameObject[] quitUI = null;

    public enum UIList { MAIN, LORD, SETTING, QUIT, PLAY};

    public UIList nowViewUI = UIList.MAIN;

    void Start()
    {
        Time.timeScale = 0f;

        nowViewUI = UIList.MAIN;

        _MainUI.SetActive(true);
        background.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(true);
        }
        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            settingUI[i].SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            quitUI[i].SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch (nowViewUI)
            {
                case UIList.MAIN:
                    {
                        break;
                    }
                case UIList.LORD:
                    {
                        Lord_Back();
                        break;
                    }
                case UIList.SETTING:
                    {
                        Setting_Back();
                        break;
                    }
                case UIList.QUIT:
                    {
                        break;
                    }
                case UIList.PLAY:
                    {
                        //TODO 스텟, 미니맵, 등등 띄우기
                        break;
                    }
            }
        }
    }

    public void Main_New()
    {
        nowViewUI = UIList.PLAY;

        background.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(false);
        }
        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            settingUI[i].SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            quitUI[i].SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void Main_Lord()
    {
        nowViewUI = UIList.LORD;

        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(true);
        }
    }

    public void Main_Setting()
    {
        nowViewUI = UIList.SETTING;

        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(false);
        }
        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            settingUI[i].SetActive(true);
        }
    }

    public void Main_Quit()
    {
        nowViewUI = UIList.QUIT;

        for (int i = 0; i < 4; i++)
        {
            quitUI[i].SetActive(true);
        }
    }

    public void Lord_First()
    {
        //TODO 1번 파일 로드

        Debug.Log("1번 파일 로드");

        nowViewUI = UIList.PLAY;

        background.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(false);
        }
        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            settingUI[i].SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            quitUI[i].SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void Lord_Second()
    {
        //TODO 2번 파일 로드

        Debug.Log("2번 파일 로드");

        nowViewUI = UIList.PLAY;

        background.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(false);
        }
        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            settingUI[i].SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            quitUI[i].SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void Lord_Third()
    {
        //TODO 3번 파일 로드

        Debug.Log("3번 파일 로드");

        nowViewUI = UIList.PLAY;

        background.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(false);
        }
        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            settingUI[i].SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            quitUI[i].SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void Lord_Back()
    {
        nowViewUI = UIList.MAIN;

        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
    }

    public void Setting_Back()
    {
        nowViewUI = UIList.MAIN;

        for (int i = 0; i < 3; i++)
        {
            settingUI[i].SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(true);
        }
    }

    public void Quit_Yes()
    {
        Debug.Log("종료");
        Application.Quit();
    }

    public void Quit_No()
    {
        nowViewUI = UIList.MAIN;

        for (int i = 0; i < 4; i++)
        {
            quitUI[i].SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(true);
        }
    }
}
