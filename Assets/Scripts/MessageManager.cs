using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public GameObject messagePrefab;
    public Transform contentTransform;
    public Button nextMonthButton;
    private List<Message> messages = new List<Message>();
    private bool firstTimeClick = true;

    void Start()
    {
        nextMonthButton.onClick.AddListener(OnNextMonth);
        LoadMessages();
    }

    void LoadMessages()
    {
        if (firstTimeClick)
        {
            StartCoroutine(DisplayMessages());
            firstTimeClick = false;
        }
        else
        {
            foreach (var msg in messages)
            {
                CreateMessage(msg);
            }
        }
    }

    IEnumerator DisplayMessages()
    {
        foreach (var msg in messages)
        {
            CreateMessage(msg);
            yield return new WaitForSeconds(1); // Adjust delay as needed
        }
    }

    void CreateMessage(Message msg)
    {
        GameObject newMessage = Instantiate(messagePrefab, contentTransform);
        newMessage.transform.Find("NameText").GetComponent<Text>().text = msg.name;
        newMessage.transform.Find("MessageText").GetComponent<Text>().text = msg.text;
        newMessage.transform.SetAsLastSibling();
        AdjustMessagesPosition();
    }

    void AdjustMessagesPosition()
    {
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            Transform message = contentTransform.GetChild(i);
            message.localPosition = new Vector3(0, -i * 100, 0); // Adjust the spacing as needed
        }
    }

    void OnNextMonth()
    {
        UpdateMessagesForNewMonth();
        RefreshPhoneScene();
    }

    void UpdateMessagesForNewMonth()
    {
        // Update your messages list here based on the new month.
    }

    void RefreshPhoneScene()
    {
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }
        LoadMessages();
    }
}

[System.Serializable]
public class Message
{
    public string name;
    public string text;
}
