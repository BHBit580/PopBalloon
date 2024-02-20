using DG.Tweening;
using UnityEngine;
public class PlayButton : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO startGameEvent;
    [SerializeField] private StringDataSO playerName;
    [SerializeField] private GameObject nameInputField; 
    
    [Header("Animation")]
    [SerializeField] private GameObject loadingTitle;
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
        
        startGameEvent.RaiseEvent();
        UIAnimationEffect();
    }
    
    private void UIAnimationEffect()
    {
        nameInputField.GetComponent<RectTransform>().DOAnchorPosX(inputLastX, time , true);
        loadingTitle.GetComponent<RectTransform>().DOAnchorPosX(loadingLastX, time , true);
        gameObject.SetActive(false);
    }
}