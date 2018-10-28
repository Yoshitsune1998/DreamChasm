using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cubes : MonoBehaviour {

    public string Name;
    public string Pos;
    public bool IsReal = true;
    public float speed = 2.5f;
    public float incrementSpeed = .01f;
    public static float incrementSize = .00575f;
    public static float incrementTransparency = .025f;
    public static float decrementTransparency = .25f;
    public float TrToDestroy = -2.75f;
    public float DestroyTime = -6.25f;
    public bool active = false;

    private Transform cube;
    private bool destroy = false;
    private Color col;
    private SpriteRenderer sr;

    private void Start()
    {
        cube = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        col = sr.color;
        if (!GameVariables.gameNotStart)
        {
            col.a = 0f;
            sr.color = col;
        }
    }

    void Update () {

        if (GameVariables.gameNotStart==true || GameManager.instance.gameOver || GameManager.instance.paused) return;

        Movement();

        if (cube.transform.position.y <= TrToDestroy)
        {
            destroy = true;
        }   

        if (cube.transform.position.y <= DestroyTime)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        cube.transform.Translate(Vector2.down * speed * Time.deltaTime);
        cube.localScale = new Vector2((cube.localScale.x + incrementSize), (cube.localScale.y + incrementSize));
        if (speed <= 2.75f)
        {
            speed += incrementSpeed;
        }
        if (destroy)
        {
            TransparencyAdjustmentMinus();
            return;
        } 
        TransparencyAdjustmentPlus();
    }

    void TransparencyAdjustmentPlus()
    {
        col.a += incrementTransparency;
        sr.color = col;
    }

    void TransparencyAdjustmentMinus()
    {
        col.a -= decrementTransparency;
        sr.color = col;
    }

}
