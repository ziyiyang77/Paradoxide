using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    public string[] BEAnimations;
    public string[] TEAnimations;
    public string[] HEAnimations;
    public GameObject canvas;

    public AudioClip BEBGM;
    public AudioClip TEBGM;
    public AudioClip HEBGM;

    public AudioClip BEaudio;
    public AudioClip Heaudio;

    public AudioSource BGMSource;
    public AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Ensure the canvas is initially disabled
        canvas.SetActive(false);

        // Start a coroutine to play the appropriate animations and BGM based on player data
        StartCoroutine(PlayAnimationsAndBGM());
    }

    private IEnumerator PlayAnimationsAndBGM()
    {
        // Check player data
        GameData data = GameDataManager.instance.gameData;
        string[] animationsToPlay = null;
        AudioClip bgmToPlay = null;
        AudioClip audioToPlay = null;

        if(data.pollutionPercentage < 2 || data.bossSatisfaction < 2)
        {
            animationsToPlay = BEAnimations;
            bgmToPlay = BEBGM;
            audioToPlay = BEaudio;
        }
        else if (data.pollutionPercentage == 3 && data.bossSatisfaction == 7)
        {
            animationsToPlay = TEAnimations;
            bgmToPlay = TEBGM;
        }
        else if (data.pollutionPercentage == 5 && data.bossSatisfaction == 5)
        {
            animationsToPlay = TEAnimations;
            bgmToPlay = TEBGM;
        }
        else if (data.pollutionPercentage == 7 && data.bossSatisfaction == 3)
        {
            animationsToPlay = HEAnimations;
            bgmToPlay = HEBGM;
            audioToPlay = Heaudio;
        }

        // Play the selected BGM
        if (bgmToPlay != null)
        {
            BGMSource.clip = bgmToPlay;
            BGMSource.Play();
        }

        if (audioToPlay != null)
        {
            audioSource.clip = audioToPlay;
            audioSource.Play();
        }

        // Play the selected animations
        if (animationsToPlay != null)
        {
            foreach (string animationName in animationsToPlay)
            {
                animator.Play(animationName);

                // Wait for the length of the current animation
                float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(animationLength);
            }
        }

        // Enable the canvas after all animations have played
        canvas.SetActive(true);

        // Stop the animation by setting the animator's speed to 0
        animator.speed = 0;
    }
}
