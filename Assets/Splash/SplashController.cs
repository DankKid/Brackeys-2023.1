using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SplashController : MonoBehaviour
{
    public VideoPlayer vid;
    //public Animator anim;
    public GameObject splash;
    float timer;
    public RawImage image;
    public Image background;
    public GameObject imageGO;
    bool fading;
    bool backFading;
    [Range(0f, 10f)] public float fadeSpeed;

    [SerializeField] AudioSource music;


    void Start()
    {
        imageGO.SetActive(true);
        timer = 7;
        vid.Play();
        fading = false;
        backFading = false;
        music.volume = 0;
    }

    private void Update()
    {
        
        
        //Debug.Log(timer);
        if(timer > -1)
        {
            timer -= Time.deltaTime;
        }




        if (timer < 1 && !fading)
        {
            fading = true;
            image.CrossFadeAlpha(0f, 0.5f, true);
        }
        if(timer < 0 && !backFading)
        {
            backFading = true;
            background.CrossFadeAlpha(0f, 0.5f, true);
            
        }
        if(timer < -0.5f)
        {
            music.Play();
            
            Debug.Log("Started playing music!");
            
        }



    }

    private void fadeAudio()
    {
       music.volume += 0.005f;
    }

    private void FixedUpdate()
    {

        if (music.isPlaying && music.volume < 0.25f)
        {

            fadeAudio();
        }
        if(music.volume >= 0.25f)
            splash.SetActive(false);
    }

}
