using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TpBar : MonoBehaviour
{
    [SerializeField]
    private Text tpText = null;
    [SerializeField]
    private Image tpBar = null;

    private float curTP;
    private float maxTP = 100;

    private void Start()
    {
        curTP = maxTP;
    }

    public void UpdateTPBar(float tp)
    {
        curTP += tp;

        if (curTP < maxTP)
        {
            tpText.text = string.Concat("TP:" + Mathf.Round(curTP));
            tpBar.fillAmount = curTP / 100;
        }

    }
}
