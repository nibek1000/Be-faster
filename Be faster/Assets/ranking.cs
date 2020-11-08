using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class ranking : MonoBehaviour {
    public int Rank = 0;
    [Header("Script by XYZgames")]
    [Space(10)]
    [Header("UI settings")]
    public GameObject lvlUp;
    public GameObject lvlDown;
    public Text rankText;
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
    private float RandomF;
    private int toRaiseLevel = 0;
	void Start () {
        
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
        FirstTime = PlayerPrefs.GetInt("ft1");
        tut = PlayerPrefs.GetInt("tut1");
        Rank = PlayerPrefs.GetInt("rank");
        StartCoroutine(tut11());
        if (FirstTime == 1)
        {
            Rank = PlayerPrefs.GetInt("rank");
        }
        else
        {
            FirstTime++;
            PlayerPrefs.SetInt("ft1", FirstTime);
        }
        if(tut == 0)
        {
            Tutorial.SetActive(true);
            tut = 1;
            PlayerPrefs.SetInt("tut1", tut);
        }
        StartCoroutine(Save());
        mainText.text = "waiting for start...";
    }
	

	void Update () {
        Debug.Log(Rank + " : " + RandomF);
        achievementsReward();
        rankText.text = "Your rank is: " + Rank + "✪";
        if(Rank >= 10)
        {
            Rank--;
        }
        if(toRaiseLevel >= 5)
        {
            Rank++;
            toRaiseLevel -= 5;
            lvlUp.SetActive(true);
            StartCoroutine(SetFalse1());
        }
        if(toRaiseLevel <= -3)
        {
            Rank--;
            toRaiseLevel += 3;
            lvlDown.SetActive(true);
            StartCoroutine(SetFalse1());
        }
        lbs = speed * 100;
        leaderboardSpeed = (int)lbs;
        //checkAchevements();
        if(start <= -1)
        {
            start++;
        }
        FToString = speed.ToString("N2");
        if (lose == true)
        {
            losePanel.SetActive(true);
            toRaiseLevel--;
            loseSpeedText.text = "Is: " + FToString + "s" + " too fast for you?";
            speed += 0.01f;
            Combo = 0;
            lose = false;
            StartCoroutine(SetFalse());
        }
        if (won == true)
        {
            winPanel.SetActive(true);
            Combo++;
            toRaiseLevel++;
            winSpeedText.text = "You click on button faster then: " + FToString + "s";
            speed -= 0.02f;
            won = false;
            StartCoroutine(SetFalse());
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

    IEnumerator SetFalse1()
    {
        yield return new WaitForSeconds(3);
        lvlDown.SetActive(false);
        lvlUp.SetActive(false);
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(3);
        GoPressed = false;
        start--;
        mainText.text = "waiting for start...";
        losePanel.SetActive(false);
        tooFastPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void randomRank()
    {
        Debug.Log("Random: " + RandomF);
        if (Rank == 0)
        {
            RandomF = Random.Range(1f, 1.5f);
        }
        if(Rank == 1)
        {
            RandomF = Random.Range(0.75f, 1.0f);
        }
        if (Rank == 2)
        {
            RandomF = Random.Range(0.50f, 0.75f);
        }
        if (Rank == 3)
        {
            RandomF = Random.Range(0.30f, 0.5f);
        }
        if (Rank == 4)
        {
            RandomF = Random.Range(0.2f, 0.3f);
        }
        if (Rank == 5)
        {
            RandomF = Random.Range(0.1f, 0.2f);
        }
        if (Rank == 6)
        {
            RandomF = Random.Range(0.05f, 0.1f);
        }
        if (Rank == 7)
        {
            RandomF = Random.Range(0.02f, 0.05f);
        }
        if (Rank == 8)
        {
            RandomF = Random.Range(0.001f, 0.02f);
        }
        if (Rank == 9)
        {
            RandomF = Random.Range(0.001f, 0.001f);
        }

    }
    IEnumerator Ready()
    {
        //yield return new WaitForSeconds(1);
        mainText.text = "Ready";
        randomRank();
        yield return new WaitForSeconds(RandomF);
        mainText.text = "Steady";
        randomRank();
        yield return new WaitForSeconds(RandomF);
        mainText.text = "GO!";
        holdOn = false;
        if (tooFastPanel.active != true)
        {
            start++;
            StartCoroutine(Example());
        }
        
    }


    IEnumerator Example()
    {
        randomRank();
        yield return new WaitForSeconds(RandomF);
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
        PlayerPrefs.SetInt("rank", Rank);
        StartCoroutine(Save());
        Social.ReportScore(Rank, "CgkIm6eM5P4ZEAIQIQ", (bool success) => {});
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

    void achievementsReward()
    {
        if(Rank >= 0)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQIg", 100.0f, (bool success) => { });
        }
        if (Rank >= 1)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQIw", 100.0f, (bool success) => { });
        }
        if (Rank >= 2)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQJA", 100.0f, (bool success) => { });
        }
        if (Rank >= 3)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQJQ", 100.0f, (bool success) => { });
        }
        if (Rank >= 4)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQJg", 100.0f, (bool success) => { });
        }
        if (Rank >= 5)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQJw", 100.0f, (bool success) => { });
        }
        if (Rank >= 6)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQKA", 100.0f, (bool success) => { });
        }
        if (Rank >= 7)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQKQ", 100.0f, (bool success) => { });
        }
        if (Rank >= 8)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQKw", 100.0f, (bool success) => { });
        }
        if (Rank >= 9)
        {
            Social.ReportProgress("CgkIm6eM5P4ZEAIQLA", 100.0f, (bool success) => { });
        }

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

    
}
