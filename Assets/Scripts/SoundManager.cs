using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] List<AudioClip> zombieGrunt, zombieGloat;

    [SerializeField] AudioClip shoot, hit, place, failPlace, select;

    public void PlayZombieGrunt(AudioSource source)
    {
        AudioClip clip;

        int index = Random.Range(0, zombieGrunt.Count-1);
        clip = zombieGrunt[index];
        float pitch = Random.Range(-3, 3);
        source.pitch = pitch;
        source.clip = clip;
        source.Play();
    }

    public void PlayZombieGloat(AudioSource source)
    {
        AudioClip clip;

        int index = Random.Range(0, zombieGloat.Count - 1);
        clip = zombieGrunt[index];
        float pitch = Random.Range(-3, 3);
        source.pitch = pitch;
        source.clip = clip;
        source.Play();

    }

    public void PlayShoot(AudioSource source)
    {
        print("Shoot");
        source.clip = shoot; 
        source.pitch = Random.Range(-1, 2);
        source.Play();
    }

    public void PlayHit(AudioSource source)
    {
        source.clip = hit;
        source.pitch = Random.Range(-3, 3);
        source.Play();
    }

    public void PlayPlace(AudioSource source)
    {
        source.clip = place;
        source.Play();
    }

    public void PlayFailPlace(AudioSource source)
    {
        source.clip = failPlace;
        source.Play();
    }

    public void PlaySelect(AudioSource source)
    {
        source.clip = select;
        source.Play();
    }

}
