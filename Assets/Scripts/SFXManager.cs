using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioClip buttonClickSFX;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonClickSFX()
    {
        if (buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX);
        }
    }
}
