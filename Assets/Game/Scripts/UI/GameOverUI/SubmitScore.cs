using UnityEngine;
using UnityEngine.UI;

public class SubmitScore : MonoBehaviour
{
    [SerializeField] private InputName inputName;
    [SerializeField] private SOIntData playerScore;
    
    public void OnSubmitScore()
    {
        if (inputName.GetPlayerName() == "")
        {
            Debug.Log("No name entered!");
            return;
        }
        
        Debug.Log(inputName.GetPlayerName());
        Debug.Log(playerScore.data);
    }
}
