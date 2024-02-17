using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class SubmitScore : MonoBehaviour
{
    [SerializeField] private StringDataSO playerName;
    [SerializeField] private IntDataSO playerScore;
    [SerializeField] private LeaderBoardManager leaderBoardManager;
    
    public void OnSubmitScore()
    {
        if (string.IsNullOrEmpty(playerName.data))
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
        Debug.Log("Successful leaderboard sent!");
        GetLeaderboard();
    }
    
    
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerHighScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnFailure);
    }
    
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        leaderBoardManager.ShowLeaderboard(result);
    }
    
    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }

}
