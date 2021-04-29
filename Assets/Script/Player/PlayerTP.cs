using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TPState
{
    DEFAULT,
    CHARGING,
    STOP
}

public class PlayerTP : MonoBehaviour
{
    [Header("TP값 관련")]
    public float maxTP = 100f;
    public float decreaseTP = 10f; //초당 10씩 소모
    public float increaseTP = 10f; // 한번 성공시마다 10씩 충전
    private float _tp;
    private Rigidbody2D rigid;

    private PlayerInput playerInput;

    public TPState state = TPState.DEFAULT;

    void Awake()
    {
        _tp = maxTP;
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerInput.timeSleepKey && state == TPState.DEFAULT)
        {
            EnterStopState();
        }
        else if (playerInput.timeSleepKey && state == TPState.STOP)
        {
            state = TPState.DEFAULT;
        }
        else if (playerInput.chargeKey && state == TPState.DEFAULT && !(_tp >= maxTP))
        {
            GameManager.Charger.SetActive(true);
            EnterCharingState(true);//차징시작
        }
        else if (playerInput.chargeKey && state == TPState.CHARGING)
        {
            GameManager.Charger.SetActive(false);
            EnterCharingState(false);
            //차징종료
        }

        if (state == TPState.CHARGING)
        {
            CheckArrowKey(); //차징 상태일때는 키를 체크한다.
        }
    }

    private void CheckArrowKey()
    {
        bool result = false; //전부 클리어 했는가?
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            result = GameManager.CPanel.Check(GameManager.KeyInfo[KeyCode.LeftArrow]);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            result = GameManager.CPanel.Check(GameManager.KeyInfo[KeyCode.RightArrow]);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            result = GameManager.CPanel.Check(GameManager.KeyInfo[KeyCode.DownArrow]);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            result = GameManager.CPanel.Check(GameManager.KeyInfo[KeyCode.UpArrow]);
        }
        if (result)
        {
            _tp = Mathf.Clamp(_tp + increaseTP, 0, maxTP);
            GameManager.SetTpAmount(_tp, true);
        }
    }

    private void EnterStopState()
    {
        state = TPState.STOP;
        GameManager.SetScale(0);
        StartCoroutine(StopRoutine());
    }

    IEnumerator StopRoutine()
    {
        while (state == TPState.STOP && _tp > 0)
        {
            _tp -= decreaseTP * Time.deltaTime;
            GameManager.SetTpAmount(_tp);
            yield return null;
        }
        GameManager.SetScale(1);
        if (_tp <= 0)
        {
            ////TP가 다 떨어졌다면 자동으로 차징상태로 전환된다.
            //EnterCharingState(true);
        }
    }

    private void EnterCharingState(bool value)
    {
        if (value)
        {
            state = TPState.CHARGING;
            rigid.velocity = Vector2.zero;
        }
        else
        {
            state = TPState.DEFAULT;
        }
        GameManager.SetCharginState(value);
    }
}
