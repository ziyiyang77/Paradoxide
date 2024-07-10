using UnityEngine;

public class OpenObject : MonoBehaviour
{
    public GameObject objectToOpen;

    private void Start()
    {
        if (objectToOpen != null)
        {
            objectToOpen.SetActive(false); // Ensure the object to open is initially inactive
        }
        else
        {
            Debug.LogError("Object to open is not assigned.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (objectToOpen != null)
                {
                    objectToOpen.SetActive(true); // Open the object
                }
            }
        }
    }
}
