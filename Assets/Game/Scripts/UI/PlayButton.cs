using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private StringDataSO playerName;
    public void OnClickPlayButton()
    {
        SendNameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void SendNameData()
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
    }
    
    void OnFailure(PlayFabError error)
    {
        Debug.LogWarning("Error: " + error.GenerateErrorReport());
    }
    
}
