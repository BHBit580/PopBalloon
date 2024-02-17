using System;
using UnityEngine;

public class InputName : MonoBehaviour
{
    [SerializeField] private StringDataSO playerName;

    private void Start()
    {
        playerName.data = "Player";
    }

    public void OnEnteringTheName(string name)
    {
        playerName.data = name;
        Debug.Log(playerName.data);
    }
}
