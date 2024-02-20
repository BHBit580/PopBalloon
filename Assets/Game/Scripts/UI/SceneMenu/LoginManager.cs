using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoginManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO startGameEvent;
    [SerializeField] private StringDataSO playerName;
    private void Awake()
    {
        startGameEvent.RegisterListener(Login);
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

    private void OnDestroy()
    {
        startGameEvent.UnregisterListener(Login);
    }
}
