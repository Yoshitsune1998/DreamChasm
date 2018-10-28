using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public float speed = 1.5f;
    public bool changeDir = false;
    private float timer = 2f;
    private float timeChange = 2f;

	void Update () {
        if (timer <= 0f)
        {
            changeDir = !changeDir;
            timer = timeChange;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if (!changeDir)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
