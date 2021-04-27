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

    public void UpdateTPBar(bool isCharge,float amount)
    {
        gameObject.SetActive(isCharge);

        tpText.text = string.Concat("TP:" + Mathf.Round(amount));
        tpBar.fillAmount = amount / 100;
    }
}
