using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomLineChecker : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player._player.Falling();
        }
    }

}
