using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimationController : MonoBehaviour
{
    public Sprite[] frames1; // animation 1
    public Sprite[] frames2; // animation 2
    public Sprite[] frames3; // animation 3
    public float framesPerSecond = 10f; // frame pre sec
    public Button[] switchButtons; // buttons

    private Image image;
    private int currentFrame;
    private float timer;
    private Sprite[] currentFrames; // current in use
    private bool playOnce; // tag if onece
    private bool switchToThirdRequested; // tag to the animation 3
    private bool thirdAnimationFinished; // tag if animation 3 finished

    void Start()
    {
        image = GetComponent<Image>();
        currentFrames = frames1; // initialize as first 
        foreach (Button button in switchButtons)
        {
            button.onClick.AddListener(RequestSwitchToThirdAnimation); // add listener
        }
    }

    void Update()
    {
        if (thirdAnimationFinished)
        {
            image.enabled = false; // hide images 
            return;
        }

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
                    thirdAnimationFinished = true;
                    return;
                }

                if (switchToThirdRequested)
                {
                    // listen for switch request
                    switchToThirdRequested = false;
                    currentFrames = frames3;
                    playOnce = true;
                }
                else if (currentFrames == frames1)
                {
                    //loop animation 2
                    currentFrames = frames2;
                }

                currentFrame = 0; // reset or replay
            }
        }
    }

    void RequestSwitchToThirdAnimation()
    {
        if (!thirdAnimationFinished)
        {
            switchToThirdRequested = true; // request switch
        }
    }
}
