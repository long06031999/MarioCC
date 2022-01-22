using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
  bool IsMain = true;

  [Header("Menu Layout Area")]
  public GameObject MainMenuLayout;
  public GameObject OptionsMenuLayout;

  [Header("Main Menu Area")]

  [Header("Options Menu Area")]
  public Button OptionsButton;

  [Header("Settings Area")]
  public AudioMixer audioMixer;
  public Slider slider;
  public Text VolumeValueLabel;
  public Text TopText;

  Resolution[] resolutions;
  public Dropdown resolutionDropdown;

  public Slider MusicSlider;
  public Text MusicValueLable;

  [Header("Game Area")]
  public GameObject mario;
  private void Start()
  {

    resolutions = Screen.resolutions;
    resolutionDropdown.ClearOptions();

    List<string> options = new List<string>();

    int currentResolutionIndex = 0;
    for (int i = 0; i < resolutions.Length; i++)
    {
      string option = resolutions[i].width + "x" + resolutions[i].height;
      options.Add(option);

      if (resolutions[i].width == Screen.currentResolution.width &&
          resolutions[i].height == Screen.currentResolution.height)
      {
        currentResolutionIndex = i;
      }
    }

    resolutionDropdown.AddOptions(options);
    resolutionDropdown.value = currentResolutionIndex;
    resolutionDropdown.RefreshShownValue();
  }

  public void SetResolution(int resolutionIndex)
  {
    Resolution resolution = resolutions[resolutionIndex];
    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
  }
  private void Awake()
  {
    float volumeValue, musicValue;
    if (audioMixer.GetFloat("Effect", out volumeValue))
    {
      VolumeValueLabel.text = (volumeValue + 80) + "%";
      slider.value = volumeValue;
    }
    if (audioMixer.GetFloat("Music", out musicValue))
    {
      MusicValueLable.text = (musicValue + 80) + "%";
      MusicSlider.value = musicValue;
    }
  }
  public void PlayGame()
  {
    GameManager.Instance.StartGame();
    // audioMixer.GetFloat("MasterVolume")

    // SceneManager.LoadScene("Level 1");
    StartCoroutine(Loading());
    // GameManager.Instance.SaveGame();
  }

  IEnumerator Loading()
  {
    Scene currentScene = SceneManager.GetActiveScene();

    // The Application loads the Scene in the background at the same time as the current Scene.
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

    // Wait until the last operation fully loads to return anything
    while (!asyncLoad.isDone)
    {
      Debug.Log("...Loading Done");
      yield return null;
    }

    Debug.Log("Init");

    GameObject gameObject = Instantiate(mario, new Vector2(0, 5), Quaternion.identity);

    gameObject.transform.position = new Vector2(0, 5);
    // GameManager.Instance.SaveGame(gameObject.GetComponent<MarioController>());
    SaveGameWhenPlay(gameObject.GetComponent<MarioController>(), 1);
    // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(1));
    // Unload the previous Scene
    yield return SceneManager.UnloadSceneAsync(currentScene);
    // yield return GameManager.Instance.MoveGameObjectToScene(gameObject, 1);
  }

  void SaveGameWhenPlay(MarioController marioController, int sceneIndex)
  {
    PlayerData playerData = new PlayerData(marioController.Health, marioController.MaxHealth, marioController.transform.position,
  sceneIndex, marioController.TotalTime, marioController.CurrentLevel, marioController.bulletNumber, 3);

    // Create File
    string path = Path.Combine(Application.persistentDataPath, "player.hd");
    FileStream fileStream = File.Create(path);

    // Write
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    binaryFormatter.Serialize(fileStream, playerData);

    fileStream.Close();
    Debug.Log("...Saved");
  }

  public void Quit()
  {
    Application.Quit();
  }

  public void OnOptionsButtonClicked()
  {
    if (IsMain)
    {
      LoadOptionsMenu();
    }
    else
    {
      LoadMainMenu();
    }
  }

  public void LoadOptionsMenu()
  {
    IsMain = false;
    MainMenuLayout.SetActive(false);
    OptionsMenuLayout.SetActive(true);
  }

  public void LoadMainMenu()
  {
    IsMain = true;
    MainMenuLayout.SetActive(true);
    OptionsMenuLayout.SetActive(false);
  }

  public void SetVolume(float Volume)
  {
    if (audioMixer)
    {
      audioMixer.SetFloat("Effect", Volume);
      VolumeValueLabel.text = (Volume + 80) + "%";
    }
    else
    {
      Debug.Log("Audio Mixer not be assign");
    }
  }

  public void SetMusic(float Volume)
  {
    if (audioMixer)
    {
      audioMixer.SetFloat("Music", Volume);
      MusicValueLable.text = (Volume + 80) + "%";
    }
    else
    {
      Debug.Log("Audio Mixer not be assign");
    }
  }
  public void SetQuality(int qualityIndex)
  {
    QualitySettings.SetQualityLevel(qualityIndex);
  }

  public void LoadSavedGame()
  {

    // GameObject gameObject = Instantiate(mario, new Vector2(0, 5), Quaternion.identity);
    // GameManager.Instance.LoadSavedScene(gameObject);
    StartCoroutine(LoadingSavedGame());
  }

  public void SetTopText()
  {
    Top top = GameManager.Instance.GetTopChallenge();

    if (top != null)
    {

      TopText.text = top.name + ": " + top.ponit + "s";
    }
    else
    {
      TopText.text = "none";
    }
  }

  IEnumerator LoadingSavedGame()
  {
    Scene currentScene = SceneManager.GetActiveScene();


    // Load Scene
    string path = Path.Combine(Application.persistentDataPath, "player.hd");
    FileStream fileStream = File.OpenRead(path);
    if (fileStream != null)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);


      fileStream.Close();

      // The Application loads the Scene in the background at the same time as the current Scene.
      AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(playerData.sceneIndex, LoadSceneMode.Additive);

      // Wait until the last operation fully loads to return anything
      while (!asyncLoad.isDone)
      {
        Debug.Log("...Loading Done");
        yield return null;
      }

      Debug.Log("Init");

      GameObject gameObject = Instantiate(mario, new Vector2(0, 5), Quaternion.identity);

      MarioController controller = gameObject.GetComponent<MarioController>();
      // Setup controller
      controller.IncommingLevel = (MarioLevelEnum)playerData.level;
      controller.MaxHealth = playerData.maxHealth;
      controller.Health = playerData.health;
      controller.transform.position = new Vector2(playerData.position[0], playerData.position[1]);
      controller.TotalTime = playerData.totalTime;
      controller.bulletNumber = playerData.bulletNumber;
      controller.LifePoint = playerData.lifePoint;
      // gameObject.transform.position = new Vector2(0, 5);
      // int sceneIndex = GameManager.Instance.LoadSavedGame(gameObject.GetComponent<MarioController>());
      // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
      SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(playerData.sceneIndex));
      // Unload the previous Scene
      yield return SceneManager.UnloadSceneAsync(currentScene);
    }
    else
    {
      yield return 0;
    }
  }
}
