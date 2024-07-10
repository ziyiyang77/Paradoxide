using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    void Awake()
    {
        // Remove the default message
        RemoveChildren();
        StartStory();
    }

    // Creates a new Story object with the compiled story which we can then play!
    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            int buttonCount = story.currentChoices.Count;
            for (int i = 0; i < buttonCount; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim(), buttonCount, i);
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            // Optionally, you can add an end of story restart button here
            // Button choice = CreateChoiceView("End of story.\nRestart?", 1, 0);
            // choice.onClick.AddListener(delegate {
            //     StartStory();
            // });
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a textbox showing the line of text
    void CreateContentView(string text)
    {
        // Instantiate the text box prefab
        GameObject dialogueInstance = Instantiate(textPrefab, canvas.transform);

        // Find the Text component within the prefab
        Text storyText = dialogueInstance.GetComponentInChildren<Text>();
        if (storyText != null)
        {
            storyText.text = text;
        }
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text, int totalChoices, int index)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Calculate position for the button on an arc
        Vector3 position = CalculateButtonPosition(totalChoices, index);
        choice.transform.localPosition = position;

        return choice;
    }

    // Calculates a position for a button along an arc between 180 and 270 degrees
    Vector3 CalculateButtonPosition(int totalChoices, int index)
    {
        // Calculate the step in y based on the total height (radius * 2)
        float totalHeight = 600.0f * 2; // The diameter of the circle
        float yStep = totalHeight / (totalChoices + 1); // Plus 1 to leave space at the top and bottom

        // Calculate the y position for the current button
        float y = -600.0f + ((index + 1) * yStep);

        // Calculate the corresponding x position to maintain the arc
        float x = Mathf.Sqrt(400.0f * 400.0f - y * y);

        return new Vector3(-x, y, 0); // Negative x to ensure the arc is on the right-down part
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField]
    private Canvas canvas = null;

    // UI Prefabs
    [SerializeField]
    private GameObject textPrefab = null; // Reference to the dialogue box prefab
    [SerializeField]
    private Button buttonPrefab = null;
}
