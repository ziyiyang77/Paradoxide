using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public Text subtitleText; // Reference to the Text component for displaying subtitles
    public float subtitleDisplayTime = 3f; // Time each subtitle is displayed
    public string subtitleFileName; // Name of the subtitle file (without extension)

    private Queue<string> subtitlesQueue = new Queue<string>(); // Queue to store subtitles

    void Start()
    {
        // Ensure the subtitle text is initially empty
        subtitleText.text = "";
        LoadSubtitlesFromFile();
        StartSubtitles();
    }

    private void LoadSubtitlesFromFile()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(subtitleFileName);
        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split('\n');
            foreach (string line in lines)
            {
                subtitlesQueue.Enqueue(line.Trim());
            }
        }
        else
        {
            Debug.LogError("Subtitle file not found in Resources: " + subtitleFileName);
        }
    }

    public void StartSubtitles()
    {
        StartCoroutine(DisplaySubtitles());
    }

    private IEnumerator DisplaySubtitles()
    {
        while (subtitlesQueue.Count > 0)
        {
            subtitleText.text = subtitlesQueue.Dequeue();
            yield return new WaitForSeconds(subtitleDisplayTime);
            subtitleText.text = "";
        }
    }
}
