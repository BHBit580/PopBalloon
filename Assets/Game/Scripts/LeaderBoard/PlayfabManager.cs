using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    private void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
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
