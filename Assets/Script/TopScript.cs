using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Topscript : MonoBehaviour 
{
    public TMP_InputField inputField;
    public Button btn;
    private string sceneName = "Main";

    private void Update(){
        if (inputField.text == "")
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }


    public void SetID()
    {
        string id = inputField.text;
        Debug.Log("ID is set to " + id);
        PlayerPrefs.SetString("ID", id);
        btn.interactable = false;
    }

    public void LoadMain(){
        if (PlayerPrefs.GetString("ID", "Nan") == "Nan" || inputField.text == "")
        {
            Debug.Log("ID is not set");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
