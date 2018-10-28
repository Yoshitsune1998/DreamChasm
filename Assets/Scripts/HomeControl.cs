using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeControl : MonoBehaviour {

    public static HomeControl home;
    public GameObject highscoreUI;
    public GameObject optionUI;
    public Text highscore;
    public GameObject back;

    private void Awake()
    {
        if (home==null)
        {
            home = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void OpenOption()
    {
        optionUI.SetActive(true);
    }

    public void CloseOption()
    {
        optionUI.SetActive(false);
        Option.option.SetToNormal();
    }

    public void OpenHighscore()
    {
        highscoreUI.SetActive(true);
        back.SetActive(true);
        int score;
        if (PlayerPrefs.GetInt("Highscore") > 0)
        {
            score = PlayerPrefs.GetInt("Highscore");
        }
        else
        {
            score = 0;
        }
        highscore.text = "" + score;
    }

    public void CloseHighscore()
    {
        highscoreUI.SetActive(false);
        back.SetActive(false);
    }

}
