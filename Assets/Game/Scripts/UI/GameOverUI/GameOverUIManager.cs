using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private void OnDisable()
    {
        gameOver.UnregisterListener(SetActiveAllChildren);
    }
}