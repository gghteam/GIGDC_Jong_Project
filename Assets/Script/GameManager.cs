using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KeyState
{
    RIGHT = 0,
    DOWN = 1,
    LEFT = 2,
    UP = 3
}

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public Text tpAmountText;
    public Image tpAmountImage;

    public Image charginFilterImage;
    public ChargePanalScript chargePanel;

    public GameObject charger;

    public Transform player;

    [Range(0, 1)]
    public float timeScale = 1f; //시간 스케일링 값, 실제로 타임스케일을 건드리지는 않는다.

    public Dictionary<KeyCode, KeyState> keyDic = new Dictionary<KeyCode, KeyState>();

    //스태틱 겟터 셋터 셋팅
    public static Dictionary<KeyCode, KeyState> KeyInfo
    {
        get { return instance.keyDic; }
    }

    public static ChargePanalScript CPanel
    {
        get { return instance.chargePanel; }
    }

    public static GameObject Charger
    {
        get { return instance.charger; }
    }

    public static Transform Player
    {
        get { return instance.player; }
    }

    void Awake()
    {
        //키코드에 따른 번호로 치환하기
        keyDic.Add(KeyCode.RightArrow, KeyState.RIGHT);
        keyDic.Add(KeyCode.DownArrow, KeyState.DOWN);
        keyDic.Add(KeyCode.LeftArrow, KeyState.LEFT);
        keyDic.Add(KeyCode.UpArrow, KeyState.UP);

        if (instance != null)
        {
            Debug.Log("다수의 게임오브젝트 인스턴스가 실행되고 있습니다. 게임매니저는 하나여야 합니다.");
        }
        instance = this;
    }

    //스태틱 헬퍼 매서드들
    public static void SetTpAmount(float amount, bool isCoroutine = false)
    {
        instance.SetTpUI(amount, isCoroutine); //실제 인스턴스에 명령
    }

    public static float GetScale()
    {
        return instance.timeScale;
    }

    public static void SetCharginState(bool value)
    {
        instance.SetCharging(value);
    }

    public static void SetScale(float scale)
    {
        scale = Mathf.Clamp(scale, 0, 1); //0, 1의 값만 받아들일 수 있게 한다.
        instance.timeScale = scale;
    }

    //스태틱 헬퍼 종료

    //차징 상태 만들어주는 함수와 코루틴
    public void SetCharging(bool value)
    {
        StartCoroutine(FadeCo(value));
        if (value)
        {
            timeScale = 0.3f;
            Vector3 position = Camera.main.WorldToScreenPoint(player.position + new Vector3(0, 2, 0));
        }
        else
        {
            timeScale = 1f;
        }
    }

    IEnumerator FadeCo(bool value)
    {
        float target = value ? 0.5f : 0f;

        while (Mathf.Abs(charginFilterImage.color.a - target) >= 0.05f)
        {
            float a = Mathf.Lerp(charginFilterImage.color.a, target, Time.deltaTime * 5f);
            Color c = charginFilterImage.color;
            c.a = a;
            charginFilterImage.color = c;
            yield return null;
        }
        Color color = charginFilterImage.color;
        color.a = target;
        charginFilterImage.color = color;
    }

    //TP UI 갱신해주는 함수와 코루틴
    public void SetTpUI(float amount, bool isCoroutine = false)
    {
        tpAmountText.text = string.Concat("TP:",Mathf.Round(amount).ToString());
        if (!isCoroutine)
        {
            tpAmountImage.fillAmount = amount / 100f;
        }
        else
        {
            StopCoroutine("TPBarRefresh");
            StartCoroutine(TPBarRefresh(amount));
        }
    }

    IEnumerator TPBarRefresh(float amount)
    {
        float transitionSpeed = 5f; //애니메이션 좀더 빨리하고 싶으면 이거 건드리면 됨.
        float targetAmount = amount / 100f;
        while (Mathf.Abs(tpAmountImage.fillAmount - targetAmount) >= 0.005f)
        {
            tpAmountImage.fillAmount = Mathf.Lerp(tpAmountImage.fillAmount, targetAmount, Time.deltaTime * transitionSpeed);
            yield return null;
        }
        tpAmountImage.fillAmount = targetAmount;
    }

}
