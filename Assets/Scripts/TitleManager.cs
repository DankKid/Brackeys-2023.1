using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using CodeMonkey.Utils;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Animator optionAnim;
    [SerializeField] Slider slider;
    [SerializeField] AudioMixer audioMixer;
    
    [SerializeField] GameObject playButton, optionButton, backButton;

    //[SerializeField] Animator playAnim, OptionAnim;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool optionsIsOpened = false;


    
    public void Options()
    {
        if (optionsIsOpened)
        {
            optionAnim.SetTrigger("CloseOP");
        }
        if (!optionsIsOpened)
        {
            optionAnim.SetTrigger("OpenOP");
        }
    }

    public void CloseOptions()
    {
        optionAnim.SetTrigger("CloseOP");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void Start()
    {
        SetVolumeOnStart();
        
        playButton.transform.GetComponent<Button_UI>().MouseOverOnceFunc = () => hoverOnPlay();
        playButton.transform.GetComponent<Button_UI>().MouseOutOnceFunc = () => hoverOffPlay();

        optionButton.transform.GetComponent<Button_UI>().MouseOverOnceFunc = () => hoverOnOptions();
        optionButton.transform.GetComponent<Button_UI>().MouseOutOnceFunc = () => hoverOffOptions();

        backButton.transform.GetComponent<Button_UI>().MouseOverOnceFunc = () => hoverOnBack();
        backButton.transform.GetComponent<Button_UI>().MouseOutOnceFunc = () => hoverOffBack();
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
        playButton.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    }
    private void hoverOffPlay()
    {
        playButton.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private void hoverOnOptions()
    {
        optionButton.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    }
    private void hoverOffOptions()
    {
        optionButton.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private void hoverOnBack()
    {
        backButton.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    }
    private void hoverOffBack()
    {
        backButton.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
