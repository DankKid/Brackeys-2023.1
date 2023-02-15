using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField] List<AudioClip> audios;
    [SerializeField] AudioSource source;
    List<AudioClip> currentAudios;
    private void Start()
    {
        currentAudios = audios;

        for(int i =0; i < currentAudios.Count; i++)
        {
            Debug.Log(currentAudios[i].name);
        }
    }


}
