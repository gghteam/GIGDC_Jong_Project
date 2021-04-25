using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField]
    private Sprite leftSprite = null;
    [SerializeField]
    private Sprite rightprite = null;
    [SerializeField]
    private Sprite upSprite = null;
    [SerializeField]
    private Sprite downSprite = null;

    private enum State {Left, Right, Up, Down }

    void Update()
    {
        
    }
}
