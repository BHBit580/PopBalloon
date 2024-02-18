using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private StringDataSO playerName;

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
        }
        
        Login();
    }
    
    void Login()
    {
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
}