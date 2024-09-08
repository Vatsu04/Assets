using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    private void Awake()
    {
        instance = this;
    }

    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        scoreText.text = score.ToString();
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (score >= 26)
        {
            PuzzleManager.Instance.GameOver();
        }
    }

    public int GetScore()
    {
        return score;
    }
}
