using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : GenericSingleton<PlayfabManager>
{
    private void Start()
    {
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
    }
    
    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }
}
