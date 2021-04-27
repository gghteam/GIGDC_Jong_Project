using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTPScript : MonoBehaviour
{
    [SerializeField]
    private float maxTP;
    public float curTP;

    public float slowScale = 0.3f;
    public float minusTP = 5f;

    private enum State
    {
        DEFAULT,
        STOP,
        CHARGING
    }

    private State state = State.DEFAULT;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && state == State.DEFAULT)
        {
            state = State.STOP;
            //curTP에서 minusTP * Time.unscaledDeltaTime 만큼을 빼준다
            //그리고 TimeScale 을 0으로

            //만약 curTP가 0보다 작아진다면 curTP를 0으로 바꿔주고
            //State 를 CHARGING 으로
            //TimeScale 을 slowScale로
            //Charger를 켜준다
        }
        else if (Input.GetKeyDown(KeyCode.S) && state == State.STOP)
        {
            state = State.DEFAULT;
            //TimeScale을 다시 1로 돌려준다
        }
        else if (Input.GetKeyDown(KeyCode.S) && state == State.CHARGING)
        {
            state = State.DEFAULT;
            //Charger 를 꺼준다
            //TimeScale을 다시 1로 돌려준다
        }

        if (Input.GetKeyDown(KeyCode.Q) && state == State.DEFAULT)
        {
            state = State.CHARGING;
            //Charger를 켜준다
            //TimeScale 을 slowScale로
        }
        else if (Input.GetKeyDown(KeyCode.Q) && state == State.CHARGING)
        {
            state = State.DEFAULT;
            //Charger를 꺼준다
            //TimeScale 을 0으로
        }
    }
}
