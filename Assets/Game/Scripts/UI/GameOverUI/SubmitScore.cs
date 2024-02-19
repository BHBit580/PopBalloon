using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class SubmitScore : MonoBehaviour
{
    [SerializeField] private IntDataSO playerScore;
    [SerializeField] private GameObject leaderBoardUI;
    [SerializeField] private GameObject[] uiGameObjectsToDisable;
    
    public void OnSubmitScore()
    {
        leaderBoardUI.SetActive(true);
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
        DisableGameObjects();
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
        leaderBoardUI.GetComponent<LeaderBoardManager>().ShowLeaderboard(result);
    }
    
    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }
    
    void DisableGameObjects()
    {
        foreach (var obj in uiGameObjectsToDisable)
        {
            obj.SetActive(false);
        }
    }
    
}