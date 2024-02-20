using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO gameOver;
    [SerializeField] private float animationTime = 0.5f;
    
    private void Awake()
    {
        gameOver.RegisterListener(SetActiveAllChildren);
        gameOver.RegisterListener(GameOverAnimation);
    }

    private void Start()
    {
        EnableAllChildrenRecursive(transform, false);
    }

    private void SetActiveAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    
    private void EnableAllChildrenRecursive(Transform parent, bool value)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(value);
        }
    }

    private void GameOverAnimation()
    {
        GetComponent<RectTransform>().localScale = Vector3.zero;
        GetComponent<RectTransform>().DOScale(1, animationTime);
    }
    
    public void OnCLickRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void OnDisable()
    {
        gameOver.UnregisterListener(SetActiveAllChildren);
        gameOver.UnregisterListener(GameOverAnimation);
    }
}