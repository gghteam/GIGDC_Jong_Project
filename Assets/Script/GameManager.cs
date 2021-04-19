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

    public TpBar tpBar = null;

    [SerializeField]
    private float changeTP = 1f;

    public float slowTP = 1f; //나중에 0.3으로 바꿔줄 변수

    private void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            tpBar.UpdateTPBar(-changeTP * Time.deltaTime);

            slowTP = 0.3f;
        }
        else
        {
            slowTP = 1f;
        }
    }
}
