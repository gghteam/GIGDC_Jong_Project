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

    private void Start()
    {
        healthBarImg = GetComponent<Image>();
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBarImg.fillAmount = health / maxHealth;
    }
}
