using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;

    [SerializeField]
    private Text scoreText = null;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int value)
    {
        score += value;
        if(scoreText)
            scoreText.text = score.ToString();
    }
}
