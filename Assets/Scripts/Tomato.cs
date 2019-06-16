using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Tomato : MonoBehaviour
{
    private float growSpeed = 1.0f;
    private float lifeTime = 1.0f;
    private float timeLeft = 0.0f;
    private bool isPoor = false;

    [SerializeField]
    private GameObject blowEffect = null;
    [SerializeField]
    private float poorTime = 0.5f;
    [SerializeField]
    private float safeTime = 1.0f;
    [SerializeField]
    public int rawCost = 1;
    [SerializeField]
    public int doneCost = 5;

    [SerializeField]
    private Color rawColor = Color.cyan;
    [SerializeField]
    private Color doneColor = Color.cyan;
    [SerializeField]
    private Color poorColor = Color.cyan;

    [SerializeField][HideInInspector]
    private MeshRenderer meshRenderer = null;
    [SerializeField][HideInInspector]
    private Rigidbody rigidbody = null;
    [SerializeField][HideInInspector]
    private SphereCollider sphereCollider = null;

    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }


    public void Setup( float _growSpeed, float _lifeTime)
    {
        growSpeed = _growSpeed;
        lifeTime = Mathf.Max( _lifeTime, safeTime );
        UpdateColor();
        UpdateSize();
    }

    private void UpdateColor()
    {
        if (!meshRenderer)
            return;

        var t = isPoor ? 1.0f - (lifeTime + poorTime - timeLeft) / poorTime : 1.0f - (lifeTime - safeTime - timeLeft) / lifeTime;
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        var currentColor = (Color)(isPoor ? Vector4.Lerp(doneColor, poorColor, t ) : Vector4.Lerp(rawColor, doneColor, t * t ) );
        meshRenderer.material.color = currentColor;
    }
    private void UpdateSize()
    {
        var t = isPoor ? (lifeTime + poorTime - timeLeft) / poorTime * lifeTime : Mathf.Min(timeLeft, lifeTime);
        t = Mathf.Max(0.0f, t);
        var finalScale = Vector3.one * t * growSpeed;
        transform.localScale = finalScale;
    }

    private void Update()
    {
        timeLeft += Time.fixedDeltaTime;
        if (timeLeft > lifeTime + poorTime)
            Kill();

        isPoor = timeLeft > lifeTime;
        if( isPoor && rigidbody)
        {
            rigidbody.useGravity = true;
            sphereCollider.isTrigger = true;
        }


        UpdateColor();
        UpdateSize();
    }

    private int CalcScore()
    {
        if (timeLeft < lifeTime - safeTime * 2)
            return 0;
        if (timeLeft < lifeTime - safeTime)
            return rawCost;
        if (timeLeft < lifeTime + safeTime)
            return doneCost;
        return 0;
    }

    private void OnMouseDown()
    {
        var score = CalcScore();
        GameManager.Instance.AddScore(score);
        Kill();
    }

    private void Blow()
    {
        var newBlowEffect = Instantiate(blowEffect);
        if (newBlowEffect)
        {
            newBlowEffect.transform.position = transform.position;
            var renderer = newBlowEffect.GetComponent<Renderer>();
            if (renderer && meshRenderer)
                renderer.material.color = meshRenderer.material.color;
        }
    }

    private void Kill()
    {
        Blow();
        Destroy(gameObject);
    }
}
