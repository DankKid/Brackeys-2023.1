using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using CodeMonkey.Utils;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Slider slider;
    [SerializeField] AudioMixer audioMixer;
    public Vector2 scrollSpeed;
    [SerializeField] GameObject playButton, optionButton;

    [SerializeField] Animator playAnim, OptionAnim;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool optionsIsOpened = false;



    public void Options()
    {
        if (optionsIsOpened)
        {
            anim.SetTrigger("CloseOP");
        }
        if (!optionsIsOpened)
        {
            anim.SetTrigger("OpenOP");
        }
    }

    public void CloseOptions()
    {
        anim.SetTrigger("CloseOP");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void Start()
    {
        SetVolumeOnStart();


        scrollSpeed = new Vector2(0.01f, 0.0f);

        playButton.transform.GetComponent<Button_UI>().MouseOverOnceFunc = () => hoverOnPlay();
        playButton.transform.GetComponent<Button_UI>().MouseOutOnceFunc = () => hoverOffPlay();

        optionButton.transform.GetComponent<Button_UI>().MouseOverOnceFunc = () => hoverOnOptions();
        optionButton.transform.GetComponent<Button_UI>().MouseOutOnceFunc = () => hoverOffOptions();
    }
    private void SetVolumeOnStart()
    {
        float volume = 0;
        if (PlayerPrefs.HasKey("volume"))
            volume = PlayerPrefs.GetFloat("volume");
        slider.value = volume;
        SetVolume(volume);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();

        audioMixer.SetFloat("Volume", volume);

        if (volume == -20)
        {
            audioMixer.SetFloat("Volume", -80);
        }
    }



    private void hoverOnPlay()
    {
        playAnim.SetTrigger("PlayShake");
    }
    private void hoverOffPlay()
    {
        playAnim.SetTrigger("PlayStop");
    }
    private void hoverOnOptions()
    {
        OptionAnim.SetTrigger("OptionShake");
    }
    private void hoverOffOptions()
    {
        OptionAnim.SetTrigger("OptionStop");
    }
}
