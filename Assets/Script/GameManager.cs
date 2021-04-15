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

    public float slowTP = 0.3f;

    private void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            tpBar.UpdateTPBar(-changeTP * Time.deltaTime);
        }
    }
}
