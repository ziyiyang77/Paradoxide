using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioClip springBGM;
    public AudioClip summerBGM;
    public AudioClip autumnBGM;
    public AudioClip winterBGM;

    private AudioSource audioSource;
    private GameDataForUI gameData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameData = GameDataManager.instance.gameDataForUI;
        UpdateBGM();
    }

    void Update()
    {
        UpdateBGM();
    }

    void UpdateBGM()
    {
        int currentMonth = gameData.currentMonth;

        AudioClip newBGM = null;

        if (currentMonth >= 1 && currentMonth <= 3)
        {
            newBGM = winterBGM;
        }
        else if (currentMonth >= 4 && currentMonth <= 6)
        {
            newBGM = springBGM;
        }
        else if (currentMonth >= 7 && currentMonth <= 9)
        {
            newBGM = summerBGM;
        }
        else if (currentMonth >= 10 && currentMonth <= 12)
        {
            newBGM = autumnBGM;
        }

        if (audioSource.clip != newBGM)
        {
            audioSource.clip = newBGM;
            audioSource.Play();
        }
    }
}
