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

    [SerializeField]
    private float curTP;
    private float maxTP = 100;

    private void Start()
    {
        curTP = maxTP;
    }

    public void UpdateTPBar(float tp)
    {
        curTP += tp;
        
        if (curTP <= 0)
        {
            curTP = 0;
            Debug.Log("TP가 없씀!");
        }
        else if (!(curTP >= maxTP))
        {
            tpText.text = string.Concat("TP:" + Mathf.Round(curTP));
            tpBar.fillAmount = curTP / 100;
        }

    }
}
