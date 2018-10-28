using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> _line = new List<GameObject>();

    public Text ScoreText;
    public Text LastScore;
    public Text HighScore;

    public GameObject GameOverPanel;
    public GameObject Controller;
    public GameObject PlayUI;
    public GameObject PausePanel;
    public GameObject OptionPanel;

    public int score = 0;
    private int state = 0;

    public bool gameOver = false;
    public bool paused = false;

    public Animator bg_anim;
    public Animator[] cloud_anim;
    public GameObject warningSign;
    public GameObject[] ornamentNormal;
    public GameObject[] ornamentDark;
    public GameObject actOrnament;
    private GameObject tempOrnament;
    private int activeOrnament = 0;
    private bool minus = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void OrangeButton()
    {
        CubesChecker("Orange");
    }

    public void PinkButton()
    {
        CubesChecker("Pink");
    }

    public void GreenButton()
    {
        CubesChecker("Green");
    }

    public void BlueButton()
    {
        CubesChecker("Blue");
    }

    public void CubesChecker(string button)
    {
        if (gameOver) return;
        var cubesNow = Player._player.cubesOn.GetComponent<Cubes>().Pos;
        bool checkBeside = false;
        if (cubesNow.Equals("Right") || cubesNow.Equals("Left"))
        {
            checkBeside = true;
        }
        var curLine = Player._player.cubesOn.transform.parent.gameObject;
        int index = _line.IndexOf(curLine); //current index line
        if (index + 1 >= _line.Count - 1)
        {
            return;
        }
        var nextLine = _line[index + 1];
        Debug.Log(nextLine);
        foreach (Transform child in nextLine.transform)
        {
            var cube = child.GetComponentInChildren<Cubes>();
            if (cube == null) continue;
            Player._player.StopJump();  
            if (cube.Name.Equals(button))
            {
                if (checkBeside)
                {
                    if (!(cube.Pos.Equals(cubesNow) || cube.Pos.Equals("Middle"))) continue;
                }
                Player._player.nextTarget = child.gameObject;
                Player._player.jumping = true;
                return;
            }
            else
            {
                continue;
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        Invoke("EndGame", .75f);
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        PlayUI.SetActive(true);
        Controller.SetActive(true);
        if (Option.option!=null)
        {
            Option.option.SetToNormal();
        }
        OptionPanel.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void PauseOrResume()
    {
        if (paused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        paused = true;
        PausePanel.SetActive(true);
        Controller.SetActive(false);
        OptionPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void OpenOption()
    {
        PausePanel.SetActive(false);
        OptionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        PausePanel.SetActive(true);
        OptionPanel.SetActive(false);
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        var player = FindObjectOfType<Player>();
        Destroy(player, .5f);
        PlayUI.SetActive(false);
        Controller.SetActive(false);
        GameOverPanel.SetActive(true);
        if(PlayerPrefs.GetInt("Highscore") <= 0 || score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore",score);
        }
        LastScore.text = ""+score.ToString();
        HighScore.text = "" + PlayerPrefs.GetInt("Highscore").ToString();
    }

    public void GetScore()
    {
        score += 10;
        if (score > 0 &&score%200 == 0)
        {
            ChangeBackground();
        }
        if (score>0 && score%20==0)
        {
            ChangeOrnament();
        }
        else
        {
            if(actOrnament != null)
            {
                StartCoroutine("ClosePop");
            }
        }
        ScoreText.text = ""+score.ToString();
    }

    IEnumerator ClosePop()
    {
        tempOrnament = actOrnament;
        actOrnament = null;
        Animator orn_anim = tempOrnament.GetComponent<Animator>();
        orn_anim.SetBool("Pop", false);
        yield return new WaitForSeconds(1f);
        tempOrnament.SetActive(false);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeBackground()
    {
        state++;
        bool changer;
        if (state%2!=0)
        {
            changer = true;
        }
        else
        {
            changer = false;
        }
        bg_anim.SetBool("ChangeBg", changer);
        AnimatingClouds(changer);
    }

    public void AnimatingClouds(bool change)
    {
        foreach (var anim in cloud_anim)
        {
            anim.SetBool("ChangeCloud",change);
        }
    }

    public void ChangeOrnament()
    {
        
        if (activeOrnament + 1 >= 5)
        {
            minus = true;
        }
        else if(activeOrnament - 1 < 5)
        {
            minus = false;
        }
        if (minus)
        {
            activeOrnament--;
        }
        else
        {
            activeOrnament++;
        }
        if (state % 2 != 0)
        {
            actOrnament = ornamentDark[activeOrnament];
        }
        else
        {
            actOrnament = ornamentNormal[activeOrnament];
            
        }
        actOrnament.SetActive(true);
        Animator orn_anim = actOrnament.GetComponent<Animator>();
        orn_anim.SetBool("Pop",true);
    }

}
