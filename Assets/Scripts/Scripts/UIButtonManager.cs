using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonConfig
    {
        public GameObject buttonObject; 
    }

    public ButtonConfig[] buttonConfigs; 

    private bool buttonsAreVisible = false; // if visible

    void Start()
    {
        foreach (ButtonConfig config in buttonConfigs)
        {
            config.buttonObject.SetActive(false);
            Button button = config.buttonObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(OnAnyButtonClick);
            }
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !buttonsAreVisible)
        {
            // reset and enable
            foreach (ButtonConfig config in buttonConfigs)
            {
                config.buttonObject.SetActive(true);
            }
            buttonsAreVisible = true;
        }
    }

    void OnAnyButtonClick()
    {
        // �����ⰴť�����������Э���ӳ����ٰ�ť
        StartCoroutine(DestroyButtonsAfterDelay());
    }

    IEnumerator DestroyButtonsAfterDelay()
    {
        // �ӳ�һ����
        yield return new WaitForSeconds(1f);

        // destroy all 
        foreach (ButtonConfig config in buttonConfigs)
        {
            Destroy(config.buttonObject);
        }
        buttonsAreVisible = false;
    }
}
