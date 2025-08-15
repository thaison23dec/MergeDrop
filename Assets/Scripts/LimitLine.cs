using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLine : MonoBehaviour
{

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void TurnRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
