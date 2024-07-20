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
        // 当任意按钮被点击，启动协程延迟销毁按钮
        StartCoroutine(DestroyButtonsAfterDelay());
    }

    IEnumerator DestroyButtonsAfterDelay()
    {
        // 延迟一秒钟
        yield return new WaitForSeconds(1f);

        // destroy all 
        foreach (ButtonConfig config in buttonConfigs)
        {
            Destroy(config.buttonObject);
        }
        buttonsAreVisible = false;
    }
}
