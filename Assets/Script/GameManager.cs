using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject temp = new GameObject("GameManager");
                    instance = temp.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    [SerializeField][Header("다른 게임오브젝트들")]
    private TpBar tpBar;
    [SerializeField]
    private GameObject charger;

    [SerializeField][Header("변수들")]
    public float curTP;
    public float maxTP = 100;

    [SerializeField]
    private float minusTP = 1f; //초당 -하는 TP 양
    public float slowTP = 1f; //나중에 0.3으로 바꿔줄 변수 (슬로우)

    [SerializeField]
    private bool isTPon = false;
    private bool charge = false;

    private void Start()
    {
        slowTP = 0;
        curTP = maxTP;
        charger.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            isTPon = !isTPon;
        }

        if (isTPon)
        {
            ChangeTP(-minusTP * Time.deltaTime);
        }
        else
        {
            charge = false;
            charger.SetActive(false);
            slowTP = 1f;
        }
    }

    public void ChangeTP(float tp)
    {
        if(!charge)
        {
            if (curTP < 0)
            {
                curTP = 0;
                Debug.Log("TP가 없씀!");

                slowTP = 0.3f;

                charger.SetActive(true);
                charge = true;
            }
            else
            {
                slowTP = 0f;

                curTP += tp;

                if (curTP > maxTP) curTP = maxTP;
                tpBar.UpdateTPBar();
            }
        }
    }

    public void Charge(float tp)
    {
        if (curTP + tp > maxTP)
        {
            curTP = maxTP;
            return;
        }

        curTP += tp;
        tpBar.UpdateTPBar();
    }
}
