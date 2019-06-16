using System.Collections;
using UnityEngine;

public class TomatoSpawner : MonoBehaviour
{
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

    private void Update()
    {
        
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
        var h = 3.0f;
        var w = 2.8f;
        var newTomato = Instantiate(tomatoSample);
        newTomato.transform.position += new Vector3(Random.Range(-w, w), Random.Range(-h, h), 0.0f);
        newTomato.Setup(GetRandomRange(growRange), GetRandomRange(lifeRange));
    }
}
