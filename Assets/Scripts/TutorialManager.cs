using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorialObjects;
    [SerializeField] private string nextSceneName;

    private int currentIndex = 0;

    private void Start()
    {
        // Initialize the tutorial by activating the first object and deactivating the rest
        UpdateTutorialObjects();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdvanceTutorial();
        }
    }

    private void AdvanceTutorial()
    {
        // If there are still tutorial objects left to show
        if (currentIndex < tutorialObjects.Count)
        {
            // Deactivate the current tutorial object if it's not marked to stay active
            if (tutorialObjects[currentIndex] != null)
            {
                tutorialObjects[currentIndex].SetActive(false);
            }

            currentIndex++;

            // If we've reached the end of the tutorial, switch to the next scene
            if (currentIndex >= tutorialObjects.Count)
            {
                SceneTransitionManager.instance.FadeAndLoadScene(nextSceneName);
            }
            else
            {
                // Activate the next tutorial object
                UpdateTutorialObjects();
            }
        }
    }

    private void UpdateTutorialObjects()
    {
        // Activate the current tutorial object
        if (currentIndex < tutorialObjects.Count && tutorialObjects[currentIndex] != null)
        {
            tutorialObjects[currentIndex].SetActive(true);
        }
    }
}
