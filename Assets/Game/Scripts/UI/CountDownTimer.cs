using TMPro;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private VoidEventChannelSO gameOver;
    private TextMeshProUGUI timeText;

    private void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        maxTime -= Time.deltaTime;
        timeText.text = maxTime.ToString("F0");
        if (maxTime <= 0)
        {
            gameOver.RaiseEvent();
            maxTime = 0;
            gameObject.SetActive(false);
        }
    }
}
