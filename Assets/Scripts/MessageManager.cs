using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public GameObject messagePrefab; // Reference to the message prefab
    public Transform contentTransform; // Reference to the Content GameObject's Transform
    public List<string> highSatisfactionBossMessages;
    public List<string> midSatisfactionBossMessages;
    public List<string> lowSatisfactionBossMessages;
    public List<string> highEnvironmentKidMessages;
    public List<string> midEnvironmentKidMessages;
    public List<string> lowEnvironmentKidMessages;
    public static bool firstTimeClick = true;
    public static int bossSatisfaction;
    public static int environmentLevel;
    private static List<Message> messagesToDisplay = new List<Message>();

    void Start()
    {
        //DontDestroyOnLoad(gameObject);

        if (firstTimeClick)
        {
            InitializeMessages();
        }
        else
        {
            DisplayExistingMessages();
        }
    }

    public void InitializeMessages()
    {
        messagesToDisplay.Clear();

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
            messagesToDisplay.Add(new Message("Kid", GetRandomMessage(highEnvironmentKidMessages)));
        }
        else if (environmentLevel == 0)
        {
            messagesToDisplay.Add(new Message("Kid", GetRandomMessage(midEnvironmentKidMessages)));
        }
        else
        {
            messagesToDisplay.Add(new Message("Kid", GetRandomMessage(lowEnvironmentKidMessages)));
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
            yield return new WaitForSeconds(1); // Adjust delay as needed
        }
    }

    void CreateMessage(Message msg, bool isLastMessage)
    {
        GameObject newMessage = Instantiate(messagePrefab, contentTransform);
        newMessage.transform.Find("Name").GetComponent<Text>().text = msg.name;
        newMessage.transform.Find("Text").GetComponent<Text>().text = msg.text;
        newMessage.transform.SetAsLastSibling();

        if (firstTimeClick)
        {
            if (isLastMessage)
            {
                PositionLastMessage(newMessage);
            }
            else
            {
                StartCoroutine(AnimateMessages(newMessage));
            }
        }
        else
        {
            PositionMessage(newMessage, isLastMessage);
        }
    }

    void PositionLastMessage(GameObject newMessage)
    {
        newMessage.transform.localPosition = new Vector3(0, 0, 0); // Position it at the bottom
    }

    void PositionMessage(GameObject newMessage, bool isLastMessage)
    {
        if (isLastMessage)
        {
            newMessage.transform.localPosition = new Vector3(0, 0, 0); // Position it at the bottom
        }
        else
        {
            newMessage.transform.localPosition = new Vector3(0, 300, 0); // Position it 300 units up
        }
    }

    IEnumerator AnimateMessages(GameObject newMessage)
    {
        float animationDuration = 0.5f; // Duration of the animation
        float elapsedTime = 0f; // Time elapsed since the start of the animation

        Vector3 newMessageStartPos = new Vector3(0, 0, 0); // Starting position at the bottom
        Vector3 newMessageEndPos = new Vector3(newMessageStartPos.x, newMessageStartPos.y + 300, newMessageStartPos.z); // Target position 300 units up

        newMessage.transform.localPosition = newMessageStartPos; // Set the new message's initial position

        // Animation loop
        while (elapsedTime < animationDuration)
        {
            newMessage.transform.localPosition = Vector3.Lerp(newMessageStartPos, newMessageEndPos, elapsedTime / animationDuration); // Move the new message up

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        // Final adjustment to ensure position is correct
        newMessage.transform.localPosition = newMessageEndPos;
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
