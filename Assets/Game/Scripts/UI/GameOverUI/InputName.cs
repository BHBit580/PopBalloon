using UnityEngine;

public class InputName : MonoBehaviour
{
    private string playerNameInput;

    public string GetPlayerName()
    {
        return playerNameInput;
    }
    
    public void OnEnteringTheName(string name)
    {
        playerNameInput = name;
        Debug.Log(playerNameInput);
    }
}
