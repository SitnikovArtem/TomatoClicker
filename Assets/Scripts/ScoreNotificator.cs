using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ScoreNotificator : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private RectTransform rectTransform = null;
    private void OnValidate()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    [SerializeField]
    private GameObject scoreFlowTextSample = null;

    public static ScoreNotificator Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void NotifyScore(int value, Vector3 spawnPosition)
    {
        var newScoreFlowText = Instantiate(scoreFlowTextSample);
        if(newScoreFlowText)
        {
            var scoreFlowText = newScoreFlowText.GetComponent<ScoreFlowText>();
            if (scoreFlowText)
                scoreFlowText.Setup(rectTransform, value, spawnPosition);
        }
    }
}
