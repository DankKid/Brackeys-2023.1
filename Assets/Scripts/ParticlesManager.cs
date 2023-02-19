using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;

    public void PlayDie(Vector2 pos)
    {
        Instantiate(a, pos, Quaternion.identity);
    }
    public void PlayProjectile(Vector2 pos)
    {
        Instantiate(b, pos, Quaternion.identity);
    }
    public void PlayShoot(Vector2 pos)
    {
        // Instantiate(c, pos, Quaternion.identity);
    }
}
