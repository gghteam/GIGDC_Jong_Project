using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    private Image chargerImg = null;

    [SerializeField]
    private Sprite[] sprites;

    private int rollCount = 0;

    private KeyCode[] keys = { KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.RightArrow };

    private void Awake()
    {
        chargerImg = GetComponent<Image>();
    }

    private void OnEnable()
    {
        rollCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && KeyCode.DownArrow == keys[rollCount])
        {
            chargerImg.sprite = sprites[0];
            rollCount++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && KeyCode.LeftArrow == keys[rollCount])
        {
            chargerImg.sprite = sprites[1];
            rollCount++;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && KeyCode.UpArrow == keys[rollCount])
        {
            chargerImg.sprite = sprites[2];
            rollCount++;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && KeyCode.RightArrow == keys[rollCount])
        {
            chargerImg.sprite = sprites[3];
            rollCount++;
        }

        if (rollCount == 4)
        {
            Debug.Log("충전");
            rollCount = 0;
        }
    }
}
