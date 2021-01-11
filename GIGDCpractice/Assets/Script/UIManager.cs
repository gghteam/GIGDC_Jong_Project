using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject background = null;
    [SerializeField]
    private GameObject[] mainUI = null;
    [SerializeField]
    private GameObject[] lordUI = null;
    [SerializeField]
    private GameObject[] settingUI = null;
    [SerializeField]
    private GameObject[] quitUI = null;
    [SerializeField]
    private GameObject[] escUI = null;
    [SerializeField]
    private GameObject[] realMainUI = null;
    [SerializeField]
    private GameObject[] realQuitUI = null;

    private enum UIList { MAIN, LORD, SETTING, QUIT, PLAY, ESC, RM, RQ};

    [SerializeField]
    private UIList nowViewUI = UIList.MAIN;

    [SerializeField]
    private bool play = false;

    void Start()
    {
        play = false;

        Time.timeScale = 0f;

        nowViewUI = UIList.MAIN;

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
        for (int i = 0; i < 6; i++)
        {
            escUI[i].SetActive(false);
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
                        if (play == true) Lord_Back_ESC();
                        else Lord_Back_Main();

                        break;
                    }
                case UIList.SETTING:
                    {
                        if (play == true) Setting_Back_ESC();
                        else Setting_Back_Main();

                        break;
                    }
                case UIList.QUIT:
                    {
                        Quit_No();
                        break;
                    }
                case UIList.PLAY:
                    {
                        //TODO 스텟, 미니맵, 등등 띄우기
                        ESC();
                        break;
                    }
                case UIList.ESC:
                    {
                        ESC_Back();
                        break;
                    }
                case UIList.RM:
                    {
                        RealMain_No();
                        break;
                    }
                case UIList.RQ:
                    {
                        RealQuit_No();
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

        play = true;

        Time.timeScale = 1f;
    }

    public void Lord()
    {
        nowViewUI = UIList.LORD;

        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(true);
        }
    }

    public void Setting()
    {
        nowViewUI = UIList.SETTING;

        if (play == true)
        {
            for (int i = 0; i < 6; i++)
            {
                escUI[i].SetActive(false);
            }
            background.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                settingUI[i].SetActive(true);
            }
        }
        else
        {
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

        play = true;

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

        play = true;

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

        play = true;

        Time.timeScale = 1f;
    }

    public void Lord_Back()
    {
        if (play == true) Lord_Back_ESC();
        else Lord_Back_Main();
    }

    public void Setting_Back()
    {
        if (play == true) Setting_Back_ESC();
        else Setting_Back_Main();
    }

    public void Lord_Back_Main()
    {
        nowViewUI = UIList.MAIN;

        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
    }

    public void Setting_Back_Main()
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

    public void ESC()
    {
        nowViewUI = UIList.ESC;

        for (int i = 0; i < 6; i++)
        {
            escUI[i].SetActive(true);
        }
    }

    public void ESC_Back()
    {
        nowViewUI = UIList.PLAY;

        for (int i = 0; i < 6; i++)
        {
            escUI[i].SetActive(false);
        }
    }

    public void Lord_Back_ESC()
    {
        nowViewUI = UIList.ESC;

        for (int i = 0; i < 6; i++)
        {
            lordUI[i].SetActive(false);
        }
    }

    public void Setting_Back_ESC()
    {
        nowViewUI = UIList.ESC;

        for (int i = 0; i < 3; i++)
        {
            settingUI[i].SetActive(false);
        }
        background.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            escUI[i].SetActive(true);
        }
    }

    public void RealMain()
    {
        nowViewUI = UIList.RM;

        for (int i = 0; i < 4; i++)
        {
            realMainUI[i].SetActive(true);
        }
    }

    public void RealMain_Yes()
    {
        nowViewUI = UIList.MAIN;

        play = false;

        for (int i = 0; i < 4; i++)
        {
            realMainUI[i].SetActive(false);
        }
        for (int i = 0; i < 6; i++)
        {
            escUI[i].SetActive(false);
        }
        background.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            mainUI[i].SetActive(true);
        }
    }

    public void RealMain_No()
    {
        nowViewUI = UIList.ESC;

        for (int i = 0; i < 4; i++)
        {
            realMainUI[i].SetActive(false);
        }
    }

    public void RealQuit()
    {
        nowViewUI = UIList.RQ;

        for (int i = 0; i < 4; i++)
        {
            realQuitUI[i].SetActive(true);
        }
    }

    public void RealQuit_No()
    {
        nowViewUI = UIList.RQ;

        for (int i = 0; i < 4; i++)
        {
            realQuitUI[i].SetActive(false);
        }
    }
}
