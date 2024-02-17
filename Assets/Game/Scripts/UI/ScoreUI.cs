using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private SOIntData playerScore;

    private TextMeshProUGUI scoreText;
    private void Start()
    {
        playerScore.data = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        scoreText.text = playerScore.data.ToString();
    }
}
