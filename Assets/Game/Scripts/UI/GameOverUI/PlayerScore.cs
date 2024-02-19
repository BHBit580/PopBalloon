using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private IntDataSO playerScore;

    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = playerScore.data.ToString();
    }
}