using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private StringDataSO playerName;
    [SerializeField] private GameObject nameInputField; 
    [SerializeField] private GameObject loadingTitle;

    [Header("Animation")]
    [SerializeField] private float time = 0.2f;
    [SerializeField] private float inputLastX = -1200;
    [SerializeField] private float loadingLastX = 146;
    public void OnClickPlayButton()
    {
        if (string.IsNullOrEmpty(playerName.data))
        {
            Debug.Log("No name entered!");
            return;
        }

        if (playerName.data.Length < 3 || playerName.data.Length > 25)
        {
            Debug.Log("Name must be between 3 and 25 characters long!");
            return;
        }
        
        Login();
    }
    
    void Login()
    {
        UIAnimationEffect();
        var request = new LoginWithCustomIDRequest
        {
            // Generate a new unique ID every time
            CustomId = Guid.NewGuid().ToString(),
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnFailure);
    }
    
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
        SendNameData();
    }
    
    private void SendNameData()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = playerName.data
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnFailure);
    }
    
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Successful display name sent!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }

    private void UIAnimationEffect()
    {
        gameObject.SetActive(false);
        nameInputField.GetComponent<RectTransform>().DOAnchorPosX(inputLastX, time , true);
        loadingTitle.GetComponent<RectTransform>().DOAnchorPosX(loadingLastX, time , true);
    }
}