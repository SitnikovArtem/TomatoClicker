using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BlowEffect : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForEndAndKill());
    }

    private IEnumerator WaitForEndAndKill()
    {
        var particle = GetComponent<ParticleSystem>();
        while(particle && particle.IsAlive())
            yield return null;
        Destroy(gameObject);
    }
}
