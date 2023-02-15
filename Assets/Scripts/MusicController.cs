using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField] List<AudioClip> audios;
    [SerializeField] AudioSource source;
    List<AudioClip> currentAudios = new List<AudioClip>();
    int index;
    private void Start()
    {
        for(int i = 0; i < audios.Count - 1; i++)
        {
            currentAudios.Add(audios[i]);
        }
        
        index = randomInt();
        source.clip = currentAudios[index];
        source.Play();

        
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            Debug.Log("Stopped!");
            NextSong();
        }
        if (Input.GetKeyDown(KeyCode.Space))
            NextSong();
    }

    private int randomInt()
    {
        int value = Random.Range(0, currentAudios.Count - 1);
        return value;
    }


    void NextSong()
    {
        Debug.Log("Next");




        currentAudios.RemoveAt(index);
        if (currentAudios.Count == 0)
        {
            for (int i = 0; i < audios.Count - 1; i++)
            {
                currentAudios.Add(audios[i]);
            }
        }
        index = randomInt();
        source.clip = currentAudios[index];
        source.Play();
    }

}
