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

    [SerializeField] AudioSource music;


    void Start()
    {
        imageGO.SetActive(true);
        timer = 7;
        vid.Play();
        fading = false;
        backFading = false;
    }

    private void Update()
    {
        
        
        //Debug.Log(timer);
        if(timer > -1)
        {
            timer -= Time.deltaTime;
        }


        

        if(timer < 1 && !fading)
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
            splash.SetActive(false);
        }

    }


    private void FixedUpdate()
    {
        
    }

}
