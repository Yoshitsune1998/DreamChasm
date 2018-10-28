using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public bool IsActive = false;

    private void Start()
    {
        GameManager.instance._line.Add(gameObject);
        if (!GameVariables.stopLining)
        {
            name = "Line " + GameVariables.LineCount;
        }
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            GameManager.instance._line.Remove(gameObject);
            Destroy(gameObject);
        }
        
    }

}