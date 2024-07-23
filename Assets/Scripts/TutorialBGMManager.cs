using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBGMManager : MonoBehaviour
{
    public static TutorialBGMManager instance;

    public AudioClip tutorialBGM;

    private AudioSource audioSource;

    public string sceneName1;
    public string sceneName2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            PlayTutorialBGM();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckAndDestroy();
    }

    private void PlayTutorialBGM()
    {
        if (audioSource.clip != tutorialBGM)
        {
            audioSource.clip = tutorialBGM;
            audioSource.Play();
        }
    }

    private void CheckAndDestroy()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "Tutorial1" && sceneName != "Tutorial2")
        {
            Destroy(gameObject);
        }
    }
}
