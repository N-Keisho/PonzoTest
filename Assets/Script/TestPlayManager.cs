using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class TestPlayManager : MonoBehaviour
{
    public GameObject Mask;
    public Button btn;
    public TMP_Text currentText;
    public ToggleGroup toggleGroup;
    public float waitTime = 1f;
    private int currentNum = 0;
    private int totalNum = 2;
    private IEnumerable<Toggle>  toggle;
    private bool isFinish = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstQuestion());
        currentText.text = currentNum + 1 + "";
    }

    void Update()
    {
        if (toggleGroup.AnyTogglesOn())
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }

    public void Submit()
    {
        if (toggleGroup.AnyTogglesOn() && currentNum < totalNum - 1)
        {
            StartCoroutine(NextQuestion());
        }
        else if (toggleGroup.AnyTogglesOn() && currentNum == totalNum - 1)
        {
            isFinish = true;
            StartCoroutine(NextQuestion());
        }
    }

    private IEnumerator FirstQuestion()
    {
        Mask.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Mask.SetActive(false);
    }

    private IEnumerator NextQuestion()
    {
        Mask.SetActive(true);
        toggleGroup.SetAllTogglesOff();
        yield return new WaitForSeconds(waitTime/2);
        if (isFinish)
        {
            SceneManager.LoadScene("Finish");
        }
        currentNum++;
        currentText.text = currentNum + 1 + "";
        yield return new WaitForSeconds(waitTime/2);
        Mask.SetActive(false);
    }
}
