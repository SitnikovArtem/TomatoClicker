using System.Collections;
using UnityEngine;

public class TomatoSpawner : MonoBehaviour
{
    [SerializeField]
    private Rect spawnRect = Rect.zero;

    [SerializeField]    
    private Vector2 spawnRange = Vector2.one;

    [SerializeField]
    private Vector2 lifeRange = Vector2.one;

    [SerializeField]
    private Vector2 growRange = Vector2.one;

    [SerializeField]
    private Tomato tomatoSample = null;

    private void Start()
    {
        StartCoroutine( SpawnCoroutine() );
    }

    private float GetRandomRange(Vector2 range)
    {
        var min = Mathf.Min(range.x, range.y);
        var max = Mathf.Max(range.x, range.y);
        return Random.Range(min, max);
    }

    private IEnumerator SpawnCoroutine()
    {
        while( true )
        {
            yield return new WaitForSeconds(GetRandomRange(spawnRange));
            SpawnBubble();
        }
    }
    private void SpawnBubble()
    {
        var newTomato = Instantiate(tomatoSample);
        newTomato.transform.position += new Vector3(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax), 0.0f);
        newTomato.Setup(GetRandomRange(growRange), GetRandomRange(lifeRange));
    }
}
