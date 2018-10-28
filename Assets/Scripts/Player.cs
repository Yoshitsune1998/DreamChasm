using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player _player;
    public bool jumping = false;
    private bool falling = false;
    private bool checkOnce = true;
    public float jumpForce = .5f;
    private float targetHeight = 0;
    public GameObject cubesOn = null;
    public GameObject nextTarget = null;
    public int currentLine = 0;
    public Animator anim;

    private float heightOrigin = 0;

    private void Awake()
    {
        if (_player == null)
        {
            _player = this;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AutoSizing();
        if (falling)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y-.1f);
            cubesOn.transform.position = new Vector2(transform.position.x, transform.position.y - .75f);
        }
        if (!GameVariables.gameNotStart && !jumping && !falling)
        {
            transform.position = new Vector2(cubesOn.transform.position.x, cubesOn.transform.position.y + 1.25f);
        }
        if (jumping)
        {
            if (checkOnce)
            {
                if (nextTarget.transform.position.x > transform.position.x)
                {
                    Debug.Log("kanan");
                    anim.SetBool("GoingRight", true);
                }
                else if (nextTarget.transform.position.x < transform.position.x)
                {
                    Debug.Log("kiri");
                    anim.SetBool("GoingLeft", true);
                }
                else
                {
                    anim.SetBool("IsJumping", true);
                    Debug.Log("lurus");
                }
                checkOnce = false;
            }
            if (transform.position.y >= nextTarget.transform.position.y)
            {
                cubesOn = nextTarget;
                if (!nextTarget.GetComponent<Cubes>().IsReal)
                {
                    GameManager.instance.gameOver = true;
                    Invoke("Falling",.2f);
                }
                jumping = !jumping;
                checkOnce = true;
                StopJump();
                if (GameManager.instance.gameOver) return;
                GameManager.instance.GetScore();
                return;
            }
            Jumping(nextTarget);
        }
    }

    public void Falling()
    {
        heightOrigin = transform.position.y;
        falling = true;
        anim.SetTrigger("Die");
        GameManager.instance.GameOver();
    }

    public void StopJump()
    {
        anim.SetBool("IsJumping", false);
        anim.SetBool("GoingRight", false);
        anim.SetBool("GoingLeft", false);
    }

    private void AutoSizing()
    {
        Vector3 scale = new Vector3(cubesOn.transform.localScale.x - .1f, cubesOn.transform.localScale.y - .1f, cubesOn.transform.localScale.z - .1f);
        transform.localScale = scale;
    }
    
    public void Jumping(GameObject target)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, target.transform.position.y + .5f), jumpForce * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        GetComponent<SpriteRenderer>().flipX = true;
    }

    public void ChangeDirectionAlt()
    {
        GetComponent<SpriteRenderer>().flipX = false;
    }

}
