using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(RectTransform))]
public class ScoreFlowText : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private Text text = null;
    [SerializeField][HideInInspector]
    private RectTransform rectTransform = null;
    private void OnValidate()
    {
        text = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
    }

    [SerializeField]
    private float flowSpeed = 1.0f;

    [SerializeField]
    private float lifeTime = 1.0f;

    public void Setup(RectTransform parentTransform, int scoreValue, Vector3 position )
    {
        if(rectTransform)
        {
            rectTransform.SetParent( parentTransform );
            rectTransform.position = position;
            rectTransform.localScale = Vector3.one;
        }

        if (text)
            text.text += scoreValue.ToString();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0.0f)
            Destroy(gameObject);

        if(text)
        {
            var newColor = text.color;
            newColor.a = Mathf.Clamp(lifeTime, 0.0f, 1.0f);
            text.color = newColor;
        }

        if (rectTransform)
        {
            var shift = Vector3.zero;
            shift.y = flowSpeed * Time.deltaTime;
            rectTransform.position += shift;
        }
    }
}
