using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string yAxisName = "Vertical";
    public string xAxisName = "Horizontal";
    public string timeSleepKeyName = "TimeSleep";
    public string chargeKeyName = "Charge";
    public string jumpKeyName = "Jump";

    public float yMove { get; private set; }
    public float xMove { get; private set; }
    public float hp { get; private set; }
    public bool xMoving { get; private set; }
    public bool timeSleepKey { get; private set; }
    public bool chargeKey { get; private set; }
    public bool jump { get; private set; }
    public bool jumpHolding { get; private set; }
    void Update()
    {
        yMove = Input.GetAxisRaw(yAxisName);
        xMove = Input.GetAxisRaw(xAxisName);
        xMoving = Input.GetButton(xAxisName);
        timeSleepKey = Input.GetButtonDown(timeSleepKeyName);
        chargeKey = Input.GetButtonDown(chargeKeyName);
        jump = Input.GetButtonDown(jumpKeyName);
        jumpHolding = Input.GetButton(jumpKeyName);
    }
}
