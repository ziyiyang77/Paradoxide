using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BackgroundAnimation : MonoBehaviour
{
    public Sprite[] frames1; // animation 1
    public Sprite[] frames2; // animation 2
    public float framesPerSecond = 10f; // frame/sec
    public Button specialButton; // the new game button
    public ButtonConfig[] buttonConfigs; // other buttons
    public AudioSource audioSource; // AudioPlayer
    public VideoPlayer videoPlayer; // VideoPlayer
    public string sceneToLoadAfterVideo; // new game scene name here

    [System.Serializable]
    public class ButtonConfig
    {
        public Button button;
        public string sceneName;
    }

    private Image image;
    private int currentFrame;
    private float timer;
    private Sprite[] currentFrames; // current animation frame in use
    private bool playOnce; // tag play once
    private bool switchRequested; // tag if switched 
    private bool secondAnimationFinished; // tag if finished

    void Start()
    {
        image = GetComponent<Image>();
        currentFrames = frames1; // initialization
        specialButton.onClick.AddListener(RequestSwitchAnimation); // button listener

        // button listener
        foreach (ButtonConfig config in buttonConfigs)
        {
            config.button.onClick.AddListener(() => SceneManager.LoadScene(config.sceneName));
        }

        videoPlayer.loopPointReached += OnVideoEnd; // loop end
    }

    void Update()
    {
        if (secondAnimationFinished)
            return; // 如果第二组动画已经播放完，停止更新

        timer += Time.deltaTime;
        if (timer >= 1f / framesPerSecond)
        {
            timer = 0f;

            if (currentFrame < currentFrames.Length)
            {
                image.sprite = currentFrames[currentFrame];
                currentFrame++;
            }
            else
            {
                if (playOnce)
                {
                    // tag finished
                    secondAnimationFinished = true;
                    OnSecondAnimationEnd();
                    return;
                }

                if (switchRequested)
                {
                    // request switch
                    switchRequested = false;
                    currentFrames = frames2;
                    playOnce = true;
                }

                currentFrame = 0; // replay or reset
            }
        }
    }

    void RequestSwitchAnimation()
    {
        if (!playOnce && currentFrames == frames1)
        {
            switchRequested = true; // request switch
        }
    }

    void OnSecondAnimationEnd()
    {
        // after second animation played
        audioSource.Stop();
        image.enabled = false; // 隐藏图像
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // switch scene
        SceneManager.LoadScene(sceneToLoadAfterVideo);
    }
}
