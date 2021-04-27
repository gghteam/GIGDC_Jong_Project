using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private TpBar tpBar;

    [SerializeField]
    private GameObject charger;

    public PlayerTPScript PlayerTPScript;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }
}
