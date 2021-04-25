using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private Image healthBarImg;

    //플레이어의 체력에 변화가 생겼을 때 콜해주면 댐
    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBarImg.fillAmount = health / maxHealth;
    }
}
