using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO showLeaderBoard;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private GameObject rowTemplate;
    [SerializeField] private float distanceBetweenRows = 150f;
    [SerializeField] private Vector2 firstRowRectPosition;
    [SerializeField] private float leaderBoardPositionX;
    [SerializeField] private float animationTime = 0.5f;
    
    private void Awake()
    {
        showLeaderBoard.RegisterListener(LeaderBoardAnimation);
    }

    private void Start()
    {
        loadingText.SetActive(false);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1800, 0);
    }

    void LeaderBoardAnimation()
    {
        loadingText.SetActive(true);
        Tween leaderBoardAnimation = gameObject.GetComponent<RectTransform>().DOAnchorPosX(leaderBoardPositionX, animationTime);
        
        leaderBoardAnimation.onComplete += OnLeaderboardAnimationComplete;
    }

    private void OnLeaderboardAnimationComplete()
    {
        StartCoroutine(LeaderboardRoutine());
    }

    private IEnumerator LeaderboardRoutine()                 //Intentionally added 2 seconds delay cuz server was not able to update values in time
    {
        yield return new WaitForSeconds(2f);
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerHighScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, ShowLeaderboard, OnFailure);
    }


    private void ShowLeaderboard(GetLeaderboardResult result)
    {
        loadingText.SetActive(false);                                                    
        for(int i = 0 ; i<result.Leaderboard.Count ; i++)
        {
            Debug.Log("Position: " + result.Leaderboard[i].Position + ", Score: " + result.Leaderboard[i].StatValue + 
                      ", PlayFab ID: " + result.Leaderboard[i].PlayFabId);
            
            GameObject row = Instantiate(rowTemplate, transform);
            row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = result.Leaderboard[i].Position.ToString();
            row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = result.Leaderboard[i].DisplayName;
            row.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = result.Leaderboard[i].StatValue.ToString();
            
            Vector2 rowPosition;
            if (i == 0)
            {
                rowPosition = firstRowRectPosition;
            }
            else
            {
                rowPosition = new Vector2(firstRowRectPosition.x, firstRowRectPosition.y - distanceBetweenRows * i);
            }
            
            row.GetComponent<RectTransform>().anchoredPosition = rowPosition;
        }
    }

    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }

    private void OnDestroy()
    {
        showLeaderBoard.UnregisterListener(LeaderBoardAnimation);
    }
}
