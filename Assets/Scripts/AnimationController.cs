using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    public string animationPlayed;
    public GameObject canvas;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(animationPlayed);

        // Ensure the canvas is initially disabled
        canvas.SetActive(false);

        // Start a coroutine to enable the canvas after the animation ends
        StartCoroutine(WaitForAnimationEnd());
    }

    private System.Collections.IEnumerator WaitForAnimationEnd()
    {
        // Wait for the length of the animation
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        // Enable the canvas
        canvas.SetActive(true);

        // Stop the animation by setting the animator's speed to 0
        animator.speed = 0;
    }
}
