using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public GameObject objectToClose;

    private void Start()
    {
        // If this is a UI button, add the click listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(CloseObject);
        }
    }

    private void OnMouseDown()
    {
        // Only handle mouse clicks if this is not a UI button
        if (GetComponent<Button>() == null)
        {
            CloseObject();
        }
    }

    private void CloseObject()
    {
        if (objectToClose != null)
        {
            objectToClose.SetActive(false); // Close the object
        }
        else
        {
            Debug.LogError("Object to close is not assigned.");
        }
    }
}
