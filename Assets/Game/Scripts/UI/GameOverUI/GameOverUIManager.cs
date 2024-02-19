using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO gameOver;
    
    private void Awake()
    {
        gameOver.RegisterListener(SetActiveAllChildren);
    }

    private void Start()
    {
        EnableAllChildrenRecursive(transform, false);
    }

    private void SetActiveAllChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name != "LeaderBoardUI")
            {
                child.gameObject.SetActive(true);
            }
        }
    }
    
    private void EnableAllChildrenRecursive(Transform parent, bool value)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(value);
        }
    }
    
    public void OnCLickRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void OnDisable()
    {
        gameOver.UnregisterListener(SetActiveAllChildren);
    }
}