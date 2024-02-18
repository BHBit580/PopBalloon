using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class SubmitScore : MonoBehaviour
{
    [SerializeField] private IntDataSO playerScore;
    [SerializeField] private LeaderBoardManager leaderBoardManager;
    
    public void OnSubmitScore()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "PlayerHighScore",
                    Value = playerScore.data
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnFailure);
    }
    
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully data sent to leaderBoard!");

        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerHighScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, GetDataFromLeaderBoard, OnFailure);
    }
    
    void GetDataFromLeaderBoard(GetLeaderboardResult result)
    {
        leaderBoardManager.ShowLeaderboard(result);
    }
    
    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }
}