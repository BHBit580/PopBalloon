using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private IntDataSO playerScore;
    [SerializeField] private float scaleDuration = 0.25f;
    [SerializeField] private float maxScale = 1.5f;

    private TextMeshProUGUI scoreText;
    private int previousScore;

    private void Start()
    {
        playerScore.data = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (previousScore < playerScore.data)
        {
            previousScore = playerScore.data;
            AnimateScoreText();
        }
        scoreText.text = playerScore.data.ToString();
    }

    private void AnimateScoreText()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(scoreText.transform.DOScale(maxScale, scaleDuration));
        sequence.Append(scoreText.transform.DOScale(1, scaleDuration));
    }
}