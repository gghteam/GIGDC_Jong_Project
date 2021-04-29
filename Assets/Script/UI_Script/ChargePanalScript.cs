using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargePanalScript : MonoBehaviour
{
    private int currentIdx = 0;

    private RectTransform rTransform;

    private KeyState[] keys = { KeyState.RIGHT, KeyState.DOWN, KeyState.LEFT, KeyState.UP };

    private Image chargeImage;

    [SerializeField]
    private Sprite[] sprites;

    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        chargeImage = GetComponent<Image>();
        currentIdx = 0;
    }

    //누른키가 맞는지 체크해주는 함수
    public bool Check(KeyState key)
    {
        if (keys[currentIdx] == key)
        {
            chargeImage.sprite = sprites[currentIdx];
            currentIdx++;
        }
        if (currentIdx >= 4)
        { //끝까지 다 맞췄다면 
            currentIdx = 0;
            return true; //다 맞췄다면 return true;
        }
        return false;
    }
}
