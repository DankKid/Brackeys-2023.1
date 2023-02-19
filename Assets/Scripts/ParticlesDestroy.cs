using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestroy : MonoBehaviour
{
    public ParticleSystem p;

    private void Update()
    {
        if (!p.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
