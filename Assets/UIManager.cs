using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; set; }

    public TextMeshProUGUI errorText;
    public TextMeshProUGUI infoText;
    public GameObject checkedBoxPrefab;
    public Transform canvas;
    public GameObject uncheckedBoxPrefab;
    private List<GameObject> activeTasksUI = new List<GameObject>();
    public GameManager gameManager;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void DisplayInfoMessage(string message)
    {
        infoText.text = message;
        infoText.gameObject.SetActive(true); // Show the collect text
    }

    public void HideInfoMessage()
    {
        infoText.text = "";
        infoText.gameObject.SetActive(false); // Hide the collect text
    }

    public void DisplaySuccessMessage(string message)
    {
        errorText.color = Color.green;
        StartCoroutine(DisplayMessageCoroutine(message));
    }


    public void DisplayErrorMessage(string message)
    {
        errorText.color = Color.red;
        StartCoroutine(DisplayMessageCoroutine(message));
    }

    IEnumerator DisplayMessageCoroutine(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        errorText.gameObject.SetActive(false);
    }
    
    public void DisplayTasks(Task[] tasks)
    {
        foreach (var taskUI in activeTasksUI)
        {
            Destroy(taskUI);
        }
        activeTasksUI.Clear();
        if (tasks.Length == 0)
        {
            gameManager.GameOver(true);
        }
        else
        {
            for (var i = 0 ; i < tasks.Length ; i++)
            {
                if (tasks[i].checkedState)
                {
                    GameObject newCheckedBox = Instantiate(checkedBoxPrefab, canvas);
                    RectTransform rectTransform = newCheckedBox.GetComponent<RectTransform>();
                    rectTransform.anchorMin = new Vector2(0, 1); 
                    rectTransform.anchorMax = new Vector2(0, 1); 
                    rectTransform.pivot = new Vector2(0, 1);     
                    rectTransform.anchoredPosition = new Vector2(0, - 30 * i); 
                    newCheckedBox.GetComponentInChildren<TextMeshProUGUI>().text = tasks[i].description;
                    activeTasksUI.Add(newCheckedBox);
                }
                else
                {
                    GameObject newUncheckedBox = Instantiate(uncheckedBoxPrefab, canvas);
                    RectTransform rectTransform = newUncheckedBox.GetComponent<RectTransform>();
                    rectTransform.anchorMin = new Vector2(0, 1); 
                    rectTransform.anchorMax = new Vector2(0, 1);
                    rectTransform.pivot = new Vector2(0, 1);    
                    rectTransform.anchoredPosition = new Vector2(0, - 30 * i); 
                    newUncheckedBox.GetComponentInChildren<TextMeshProUGUI>().text = tasks[i].description;
                    activeTasksUI.Add(newUncheckedBox);
                }
            }
        }
        
    }
    
}