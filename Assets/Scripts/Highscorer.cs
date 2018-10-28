using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscorer : MonoBehaviour {

    public Text highscore_text;
    public int highscore;

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscore_text.text = "" + highscore;
    }

}
