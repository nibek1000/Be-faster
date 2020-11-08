using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class main : MonoBehaviour {
    [Header("Script by XYZgames")]
    [Space(10)]
    [Header("UI settings")]
    public Text mainText;
    public Button mainButton;
    public Text winSpeedText;
    public Text loseSpeedText;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject tooFastPanel;
    [Space(10)]
    [Header("Game settings")]
    public int level = 0;
    public float speed = 1f;
    public string FToString;
    public int start = 0;
    public bool lose = false;
    public bool GoPressed = false;
    public bool won = false;
    public bool holdOn = false;
    public int Combo = 0;
    private int FirstTime = 0;
    [Header("Tutorial")]
    public GameObject Tutorial;
    public int tut = 0;
    public Text tutText;
    public int leaderboardSpeed;
    private float lbs;
    public GameObject invalidP;
    public Text invalidTxt;
    private bool invalisSp = false;
	void Start () {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
        FirstTime = PlayerPrefs.GetInt("ft");
        tut = PlayerPrefs.GetInt("tut");
        StartCoroutine(tut11());
        if (FirstTime == 1)
        {
            Combo = PlayerPrefs.GetInt("combo");
            speed = PlayerPrefs.GetFloat("speed");
        }
        else
        {
            FirstTime++;
            PlayerPrefs.SetInt("ft", FirstTime);
        }
        if(tut == 0)
        {
            Tutorial.SetActive(true);
            tut = 1;
            PlayerPrefs.SetInt("tut", tut);
        }
        StartCoroutine(Save());
        mainText.text = "waiting for start...";
    }
	

	void Update () {
        if(speed <= -0.001f)
        {
            invalisSp = true;
            invalidP.SetActive(true);
            invalidTxt.text = speed + "error %$3" + "0.00";
            speed += 0.001f;
            
        }
        else
        {
            invalidP.SetActive(false);
            invalisSp = false;
        }
        lbs = speed * 1000;
        leaderboardSpeed = (int)lbs;
        
        //checkAchevements();
        if(start <= -1)
        {
            start++;
        }
        FToString = speed.ToString("N3");
        if (lose == true)
        {
            losePanel.SetActive(true);
            loseSpeedText.text = "Is: " + FToString + "s" + " too fast for you?";
            speed += 0.01f;
            Combo = 0;
            lose = false;
            StartCoroutine(SetFalse());
        }
        if (won == true)
        {
            winPanel.SetActive(true);
            checkAchevements();
            Combo++;
            winSpeedText.text = "You click on button faster then: " + FToString + "s";
            if (speed <= 0.02)
            {
                speed -= 0.002f;
            }
            else
            {
                speed -= 0.02f;
            }
            won = false;
            StartCoroutine(SetFalse());
        }
        if(speed <= 0.9f)
        {
            Debug.Log("Unlocked");
            unlock.Unlocked1 = 1;
            PlayerPrefs.SetInt("steadyUnlock", 1);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (holdOn == true)
            {
                tooFastPanel.SetActive(true);
                StartCoroutine(SetFalse());
            }
            if (start == 0)
            {
                startGame();
            }
            if (start == 1)
            {
                GoPressed = true;
            }
        }
    }


    public void Gstart()
    {
        if(holdOn == true)
        {
            tooFastPanel.SetActive(true);
            StartCoroutine(SetFalse());
        }
        if (start == 0)
        {
            startGame();
        }
        if (start == 1)
        {
            GoPressed = true;
        }
    }


    void startGame()
    {
        StartCoroutine(Ready());
        holdOn = true;
    }


    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(3);
        GoPressed = false;
        start--;
        holdOn = false;
        mainText.text = "waiting for start...";
        losePanel.SetActive(false);
        tooFastPanel.SetActive(false);
        winPanel.SetActive(false);
    }


    IEnumerator Ready()
    {
        //yield return new WaitForSeconds(1);
        mainText.text = "Ready";
        yield return new WaitForSeconds(1);
        mainText.text = "Steady";
        yield return new WaitForSeconds(1);
        mainText.text = "GO!";
        
        Debug.Log("Now press");
        if (tooFastPanel.active != true)
        {
            start++;
            StartCoroutine(Example());
        }
        
    }


    IEnumerator Example()
    {
        holdOn = false;
        yield return new WaitForSeconds(speed);
        if(GoPressed == false)
        {
            lose = true;
        }
        if(GoPressed == true)
        {
            won = true;
        }
        
    }


    IEnumerator Save()
    {
        yield return new WaitForSeconds(5);
        PlayerPrefs.SetFloat("speed", speed);
        PlayerPrefs.SetInt("combo", Combo);
        StartCoroutine(Save());
        if (invalisSp == true)
        {
            Social.ReportScore(Combo, "CgkIm6eM5P4ZEAIQAA", (bool success) =>
            {
                // handle success or failure
            });
            Social.ReportScore(leaderboardSpeed, "CgkIm6eM5P4ZEAIQKg", (bool success) =>
            {
                // handle success or failure
            });
        }
    }


    IEnumerator tut11()
    {
        yield return new WaitForSeconds(1);
        tutText.text = "Ready?";
        yield return new WaitForSeconds(1);
        tutText.text = "Steady";
        yield return new WaitForSeconds(1);
        tutText.text = "GO!";
        StartCoroutine(tut11());
    }

    public void AchievementsUI()
    {
        Social.ShowAchievementsUI();
    }
    public void LeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
    public void LogIn()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
    }

    void checkAchevements()
    {
        if (speed <= 1f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQAw", 100.0f, (bool success) => { });
        }
        if (speed <= 0.9f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQBA", 100.0f, (bool success) => { });
        }
        if (speed <= 0.8f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQBQ", 100.0f, (bool success) => { });
        }
        if (speed <= 0.7f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQBg", 100.0f, (bool success) => { });
        }
        if (speed <= 0.6f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQBw", 100.0f, (bool success) => { });
        }
        if (speed <= 0.5f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQCA", 100.0f, (bool success) => { });
        }
        if (speed <= 0.4f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQCQ", 100.0f, (bool success) => { });
        }
        if (speed <= 0.35f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQCg", 100.0f, (bool success) => { });
        }
        if (speed <= 0.3f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQCw", 100.0f, (bool success) => { });
        }
        if (speed <= 0.25f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQDA", 100.0f, (bool success) => { });
        }
        if (speed <= 0.2f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQDQ", 100.0f, (bool success) => { });
        }
        if (speed <= 0.15f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQDg", 100.0f, (bool success) => { });
        }
        if (speed <= 0.1f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQDw", 100.0f, (bool success) => { });
        }
        if (speed <= 0.05f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQEA", 100.0f, (bool success) => { });
        }
        if (speed <= 0.02f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQEQ", 100.0f, (bool success) => { });
        }
        if (speed <= 0.005f)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQEg", 100.0f, (bool success) => { });
        }
        if (Combo >= 2)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQEw", 100.0f, (bool success) => { });
        }
        if (Combo >= 3)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQFA", 100.0f, (bool success) => { });
        }
        if (Combo >= 4)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQFQ", 100.0f, (bool success) => { });
        }
        if (Combo >= 5)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQFg", 100.0f, (bool success) => { });
        }
        if (Combo >= 7)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQFw", 100.0f, (bool success) => { });
        }
        if (Combo >= 10)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQGA", 100.0f, (bool success) => { });
        }
        if (Combo >= 15)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQGQ", 100.0f, (bool success) => { });
        }
        if (Combo >= 20)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQGg", 100.0f, (bool success) => { });
        }
        if (Combo >= 25)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQGw", 100.0f, (bool success) => { });
        }
        if (Combo >= 30)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQHA", 100.0f, (bool success) => { });
        }
        if (Combo >= 35)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQHQ", 100.0f, (bool success) => { });
        }
        if (Combo >= 40)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQHg", 100.0f, (bool success) => { });
        }
        if (Combo >= 45)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQHw", 100.0f, (bool success) => { });
        }
        if (Combo >= 50)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQIA", 100.0f, (bool success) => { });
        }

    }
}
