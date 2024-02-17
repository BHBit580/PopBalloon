using System;
using UnityEngine;
using PlayFab.ClientModels;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private GameObject rowTemplate;
    [SerializeField] private float distanceBetweenRows = 150f;
    
    private GetLeaderboardResult leaderboardResult;
    
    public void ShowLeaderboard(GetLeaderboardResult result)
    {
        for(int i = 0 ; i<result.Leaderboard.Count ; i++)
        {
            Debug.Log("Position: " + result.Leaderboard[i].Position + ", Score: " + result.Leaderboard[i].StatValue + 
                      ", PlayFab ID: " + result.Leaderboard[i].PlayFabId);
            
            GameObject row = Instantiate(rowTemplate, transform);
            row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = result.Leaderboard[i].Position.ToString();
            row.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = result.Leaderboard[i].DisplayName;
            row.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = result.Leaderboard[i].StatValue.ToString();
            
            float rowPositionY = -distanceBetweenRows * i;
            row.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, rowPositionY);
        }
    }
}