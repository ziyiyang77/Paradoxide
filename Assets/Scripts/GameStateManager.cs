using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public bool hasSubmitted;

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
}
