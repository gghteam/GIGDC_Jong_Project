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

    public void UpdateTPBar()
    {
        tpText.text = string.Concat("TP:" + Mathf.Round(GameManager.Instance.curTP));
        tpBar.fillAmount = GameManager.Instance.curTP / 100;
    }
}
