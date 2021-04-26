using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    private Image chargerImg = null;

    [SerializeField]
    private Sprite leftSprite = null;
    [SerializeField]
    private Sprite rightprite = null;
    [SerializeField]
    private Sprite upSprite = null;
    [SerializeField]
    private Sprite downSprite = null;

    [SerializeField]
    private float chargeTP = 5f;

    private int rollCount = 0;

    private enum State 
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    State state;

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
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(state == State.DOWN)
            {
                rollCount++;
            }
            chargerImg.sprite = leftSprite;
            state = State.LEFT;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(state == State.UP)
            {
                rollCount++;
            }
            chargerImg.sprite = rightprite;
            state = State.RIGHT;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(state == State.LEFT)
            {
                rollCount++;
            }
            chargerImg.sprite = upSprite;
            state = State.UP;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(state == State.RIGHT)
            {
                rollCount++;
            }
            chargerImg.sprite = downSprite;
            state = State.DOWN;
        }

        if(rollCount == 4)
        {
            GameManager.Instance.Charge(chargeTP);
            rollCount = 0;
        }
    }
}
