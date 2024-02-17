using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class SubmitScore : MonoBehaviour
{
    [SerializeField] private InputName inputName;
    [SerializeField] private SOIntData playerScore;
    
    public void OnSubmitScore()
    {
        if (string.IsNullOrEmpty(inputName.GetPlayerName())) 
        {
            Debug.Log("No name entered!");
            return;
        }
     
        SendLeaderboard(playerScore.data);
    }
    
    private void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "PlayerHighScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnFailure);
    }
    
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        PlayfabManager.Instance.GetLeaderboard();
        Debug.Log("Successful leaderboard sent!");
    }
    
    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }

}
