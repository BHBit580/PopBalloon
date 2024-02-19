using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class SubmitScore : MonoBehaviour
{
    [SerializeField] private IntDataSO playerScore;
    [SerializeField] private VoidEventChannelSO showLeaderBoard;
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
        showLeaderBoard.RaiseEvent();
    }
    
    
    void DisableGameObjects()
    {
        foreach (var obj in uiGameObjectsToDisable)
        {
            obj.SetActive(false);
        }
    }
    
    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }
    
}