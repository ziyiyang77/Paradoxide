using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessageManager : MonoBehaviour
{
    public GameObject messagePrefab; // Reference to the message prefab
    public GameObject messageBoxPrefab; // Reference to the detailed message box prefab
    public Transform contentTransform; // Reference to the Content GameObject's Transform
    public Transform messageBoxTransform; // Reference to the Transform for detailed message box
    public List<string> highSatisfactionBossMessages;
    public List<string> midSatisfactionBossMessages;
    public List<string> lowSatisfactionBossMessages;
    public List<string> highEnvironmentKidMessages;
    public List<string> midEnvironmentKidMessages;
    public List<string> lowEnvironmentKidMessages;
    public List<string> newsMessages; // List to store news messages for each month
    public AudioClip beepSFX; // Reference to the beep sound effect
    private AudioSource audioSource; // Reference to the AudioSource component
    public static bool firstTimeClick = true;
    public static int bossSatisfaction;
    public static int environmentLevel;
    public static int currentMonth = 0; // Assuming this will be updated with the current month in the game
    private static List<Message> messagesToDisplay = new List<Message>();
    private Queue<GameObject> displayedMessages = new Queue<GameObject>(); // Queue to keep track of displayed messages
    private List<GameObject> previousMessages = new List<GameObject>(); // List to store previous message GameObjects
    private bool animationsComplete = false;

    private Dictionary<string, Sprite> avatarSprites; // Dictionary to store avatar sprites

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource component
        LoadAvatarSprites(); // Load avatar sprites
        if (firstTimeClick)
        {
            InitializeMessages();
        }
        else
        {
            DisplayExistingMessages();
        }
    }

    void LoadAvatarSprites()
    {
        avatarSprites = new Dictionary<string, Sprite>
        {
            { "Boss", Resources.Load<Sprite>("BossAvatar") },
            { "Kid", Resources.Load<Sprite>("KidAvatar") },
            { "News", Resources.Load<Sprite>("NewsAvatar") }
        };
    }

    public void InitializeMessages()
    {
        messagesToDisplay.Clear();

        // Add News message for the current month
        if (currentMonth < newsMessages.Count)
        {
            messagesToDisplay.Add(new Message("News", newsMessages[currentMonth]));
            currentMonth++;
        }

        // Determine Boss message
        if (bossSatisfaction > 0)
        {
            messagesToDisplay.Add(new Message("Boss", GetRandomMessage(highSatisfactionBossMessages)));
        }
        else if (bossSatisfaction == 0)
        {
            messagesToDisplay.Add(new Message("Boss", GetRandomMessage(midSatisfactionBossMessages)));
        }
        else
        {
            messagesToDisplay.Add(new Message("Boss", GetRandomMessage(lowSatisfactionBossMessages)));
        }

        // Determine Kid message
        if (environmentLevel > 0)
        {
            messagesToDisplay.Add(new Message("Kid", GetRandomMessage(lowEnvironmentKidMessages)));
        }
        else if (environmentLevel == 0)
        {
            messagesToDisplay.Add(new Message("Kid", GetRandomMessage(midEnvironmentKidMessages)));
        }
        else
        {
            messagesToDisplay.Add(new Message("Kid", GetRandomMessage(highEnvironmentKidMessages)));
        }

        // Display messages
        StartCoroutine(DisplayMessages(messagesToDisplay));
        firstTimeClick = false;
    }

    public void DisplayExistingMessages()
    {
        for (int i = 0; i < messagesToDisplay.Count; i++)
        {
            var msg = messagesToDisplay[i];
            bool isLastMessage = (i == messagesToDisplay.Count - 1);
            CreateMessage(msg, isLastMessage);
        }
    }

    IEnumerator DisplayMessages(List<Message> messages)
    {
        for (int i = 0; i < messages.Count; i++)
        {
            var msg = messages[i];
            bool isLastMessage = (i == messages.Count - 1);
            CreateMessage(msg, isLastMessage);
            audioSource.PlayOneShot(beepSFX); // Play the beep SFX
            yield return new WaitForSeconds(1); // Adjust delay as needed
        }
        animationsComplete = true;
        EnableHoverFunctionality();
    }

    void CreateMessage(Message msg, bool isLastMessage)
    {
        if (displayedMessages.Count >= 3)
        {
            // Remove the oldest message
            GameObject oldestMessage = displayedMessages.Dequeue();
            Destroy(oldestMessage);
        }

        GameObject newMessage = Instantiate(messagePrefab, contentTransform);
        newMessage.transform.Find("Name").GetComponent<Text>().text = msg.name;
        newMessage.transform.Find("Text").GetComponent<Text>().text = msg.text;

        displayedMessages.Enqueue(newMessage);

        PositionAndAnimateMessages();
    }

    void PositionAndAnimateMessages()
    {
        int positionIndex = displayedMessages.Count - 1;
        foreach (var message in displayedMessages)
        {
            Vector3 targetPosition = new Vector3(0, 190 * positionIndex, 0);
            StartCoroutine(AnimateMessage(message, targetPosition));
            positionIndex--;
        }
    }

    IEnumerator AnimateMessage(GameObject message, Vector3 targetPosition)
    {
        float animationDuration = 0.5f; // Duration of the animation
        float elapsedTime = 0f; // Time elapsed since the start of the animation

        Vector3 startPosition = new Vector3(0,0,0); // Get the current position

        // Animation loop
        while (elapsedTime < animationDuration)
        {
            message.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / animationDuration); // Move the message to the target position

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        // Final adjustment to ensure position is correct
        message.transform.localPosition = targetPosition;
    }

    void EnableHoverFunctionality()
    {
        foreach (var message in displayedMessages)
        {
            // Add hover and click listeners
            EventTrigger trigger = message.AddComponent<EventTrigger>();
            AddEventTrigger(trigger, EventTriggerType.PointerEnter, (data) => { OnHoverEnter(message); });
            AddEventTrigger(trigger, EventTriggerType.PointerExit, (data) => { OnHoverExit(message); });
            AddEventTrigger(trigger, EventTriggerType.PointerClick, (data) => { OnMessageClick(message); });
        }
    }

    void OnHoverEnter(GameObject message)
    {
        message.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f); // Change to a lighter color
    }

    void OnHoverExit(GameObject message)
    {
        message.GetComponent<Image>().color = Color.white; // Change back to original color
    }

    void OnMessageClick(GameObject messageObj)
    {
        if (!animationsComplete) return;

        Message msg = new Message(
            messageObj.transform.Find("Name").GetComponent<Text>().text,
            messageObj.transform.Find("Text").GetComponent<Text>().text
        );

        // Store current messages
        foreach (Transform child in contentTransform)
        {
            previousMessages.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        // Create detailed message box
        GameObject messageBox = Instantiate(messageBoxPrefab, messageBoxTransform);
        messageBox.transform.Find("Avatar").GetComponent<Image>().sprite = GetAvatarSprite(msg.name);
        messageBox.transform.Find("MessageText").GetComponent<Text>().text = msg.text;

        // Add back button functionality
        Button backButton = messageBox.transform.Find("BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(() => OnBackButtonClick(messageBox));
    }

    void OnBackButtonClick(GameObject messageBox)
    {
        Destroy(messageBox);

        // Restore previous messages
        foreach (var msg in previousMessages)
        {
            msg.SetActive(true);
        }
        previousMessages.Clear();
    }

    Sprite GetAvatarSprite(string name)
    {
        if (avatarSprites.TryGetValue(name, out Sprite avatar))
        {
            return avatar;
        }
        return Resources.Load<Sprite>("DefaultAvatar"); // Return a default sprite if name is not found
    }

    void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(action));
        trigger.triggers.Add(entry);
    }

    string GetRandomMessage(List<string> messages)
    {
        int index = Random.Range(0, messages.Count);
        return messages[index];
    }

    public void RefreshPhoneScene()
    {
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }
        firstTimeClick = true; // Set firstTimeClick to true when refreshing
        displayedMessages.Clear(); // Clear the queue
    }
}

[System.Serializable]
public class Message
{
    public string name;
    public string text;

    public Message(string name, string text)
    {
        this.name = name;
        this.text = text;
    }
}
