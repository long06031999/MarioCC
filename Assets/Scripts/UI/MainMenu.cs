using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  public AudioMixer audioMixer;
  public Slider slider;
  public Text VolumeValueLabel;

  private void Awake()
  {
    float volumeValue;
    if (audioMixer.GetFloat("MasterVolume", out volumeValue))
    {
      VolumeValueLabel.text = volumeValue.ToString();
      slider.value = volumeValue;
    }
  }
  public void PlayGame()
  {
    // audioMixer.GetFloat("MasterVolume")
    SceneManager.LoadScene("Level 1");
  }

  public void Quit()
  {
    Application.Quit();
  }

  public void SetVolume(float Volume)
  {
    if (audioMixer)
    {
      audioMixer.SetFloat("MasterVolume", Volume);
      VolumeValueLabel.text = Volume.ToString();
    }
    else
    {
      Debug.Log("Audio Mixer not be assign");
    }
  }
}
