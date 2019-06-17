using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BlowEffect : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private ParticleSystem particle = null;

    private void OnValidate()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        StartCoroutine(WaitForEndAndKill());
    }

    private IEnumerator WaitForEndAndKill()
    {
        while(particle && particle.IsAlive())
            yield return null;
        Destroy(gameObject);
    }
}
