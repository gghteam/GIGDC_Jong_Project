using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeArrow : MonoBehaviour
{
    public Sprite[] arrowSprites;

    private KeyState correct;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Reset()
    {
        correct = (KeyState)Random.Range(0, 4); //0, 1,2,3 중 하나 
        image.sprite = arrowSprites[(int)correct];
        image.color = new Color(255, 255, 255, 1);
    }

    public bool IsCorrect(KeyState key)
    {
        return correct == key;
    }

    public void SetDisable()
    {
        Color c = image.color;
        c.a = 0.4f;
        image.color = c; //컬러를 반투명하게 만들어
    }
}
