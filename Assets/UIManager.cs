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
        yield return new WaitForSeconds(2f);
        errorText.gameObject.SetActive(false);
    }
    
    public void DisplayTasks(Task[] tasks)
    {
        foreach (var taskUI in activeTasksUI)
        {
            Destroy(taskUI);
        }
        activeTasksUI.Clear();
        
        Debug.Log(tasks.Length);
        for (var i = 0 ; i < tasks.Length ; i++)
        {
            if (tasks[i].checkedState)
            {
                GameObject newCheckedBox = Instantiate(checkedBoxPrefab,canvas.position,Quaternion.identity);
                newCheckedBox.transform.parent = canvas;
                newCheckedBox.transform.position = new Vector3(8, 550 - 30 * i,0);;
                newCheckedBox.GetComponentInChildren<TextMeshProUGUI>().text = tasks[i].description;
                activeTasksUI.Add(newCheckedBox);

            }
            else
            {
                GameObject newUncheckedBox =Instantiate(uncheckedBoxPrefab,canvas.position,Quaternion.identity);
                newUncheckedBox.transform.parent = canvas;
                newUncheckedBox.transform.position = new Vector3(8, 550 - 30 * i,0);
                newUncheckedBox.GetComponentInChildren<TextMeshProUGUI>().text = tasks[i].description;
                activeTasksUI.Add(newUncheckedBox);
            }
        }
    }
    
}