using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public const string BACKGROUND_TIMESTAMP_KEY = "BackgroundSecond";
    public const string SOUND_VOLUME_KEY = "SoundVolume";

    public Slider MusicVolumeSlider;
    public AudioSource AmbientalMusic;

    void Awake()
    {
        if (!PlayerPrefs.HasKey(SOUND_VOLUME_KEY))
            PlayerPrefs.SetInt(SOUND_VOLUME_KEY, 100);

        if (!PlayerPrefs.HasKey(BACKGROUND_TIMESTAMP_KEY))
            PlayerPrefs.SetFloat(BACKGROUND_TIMESTAMP_KEY, 0.0f);

        PlayerPrefs.Save();
    }

    void Start()
    {
        MusicVolumeSlider.value = PlayerPrefs.GetInt(SOUND_VOLUME_KEY);
        AmbientalMusic.time = PlayerPrefs.GetFloat(BACKGROUND_TIMESTAMP_KEY);
        AmbientalMusic.Play();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnMusicVolumeChanged()
    {
        PlayerPrefs.SetInt(SOUND_VOLUME_KEY, (int)MusicVolumeSlider.value);
        PlayerPrefs.Save();
        AmbientalMusic.volume = MusicVolumeSlider.value / 100.0f;
    }

    public void NewGame()
    {
        PlayerPrefs.SetFloat(BACKGROUND_TIMESTAMP_KEY, AmbientalMusic.time);
        PlayerPrefs.Save();

        SceneManager.LoadScene("SampleScene");
    }

    public void OptionsPressed()
    {
        MusicVolumeSlider.transform.gameObject.SetActive(!MusicVolumeSlider.transform.gameObject.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
