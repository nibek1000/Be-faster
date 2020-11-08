using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GP : MonoBehaviour {
    private void Start()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
    }
    public void LogIn()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
    }
    public void Achievements()
    {
        Social.ShowAchievementsUI();
    }
    public void Leaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}
