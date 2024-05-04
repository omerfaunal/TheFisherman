using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; set; }

    public TextMeshProUGUI errorText;
    public TextMeshProUGUI infoText;

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
}