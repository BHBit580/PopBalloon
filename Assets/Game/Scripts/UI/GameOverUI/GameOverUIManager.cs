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
        EnableAllChildrenRecursive(transform, true);
    }
    
    private void EnableAllChildrenRecursive(Transform parent, bool value)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            EnableAllChildrenRecursive(child, value);
            if (child.gameObject != null) child.gameObject.SetActive(value);
        }
    }

    
    private void OnDisable()
    {
        gameOver.UnregisterListener(SetActiveAllChildren);
    }
}
