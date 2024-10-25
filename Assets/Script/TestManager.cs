using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class TestManager : MonoBehaviour
{
    public GameObject Mask;
    public GameObject imgManager;
    public GameObject CSVExporter;
    public Button btn;
    public ToggleGroup toggleGroup;
    public List<Question> questions;
    private IEnumerable<Toggle>  toggle;
    private ImageManager imageManager;
    private CSVExporter exporter;
    private bool isFinish = false;
    // Start is called before the first frame update
    void Start()
    {
        imageManager = imgManager.GetComponent<ImageManager>();
        exporter = CSVExporter.GetComponent<CSVExporter>();
        Mask.SetActive(false);
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
        if (toggleGroup.AnyTogglesOn() && imageManager.currentNum < imageManager.sp.Length - 1)
        {
            StartCoroutine(NextQuestion());
        }
        else if (imageManager.currentNum == imageManager.sp.Length - 1)
        {
            isFinish = true;
            StartCoroutine(NextQuestion());
        }
    }

    private IEnumerator NextQuestion()
    {
        Mask.SetActive(true);
        toggle = toggleGroup.ActiveToggles();
        foreach (var item in toggle)
        {
            if (item.isOn)
            {
                SetAnswer(item.name);
            }
        }
        toggleGroup.SetAllTogglesOff();
        imageManager.NextImage();
        yield return new WaitForSeconds(0.5f);
        if (isFinish)
        {
            Save();
            SceneManager.LoadScene("Finish");
        }
        yield return new WaitForSeconds(0.5f);
        Mask.SetActive(false);
    }

    private void SetAnswer(string name)
    {
        imageManager.currentQuestion.answer = int.Parse(name);
        questions.Add(imageManager.currentQuestion);
    }

    private void Save(){
        string id = PlayerPrefs.GetString("ID", "Nan");
        DateTime dt = DateTime.Now;
        string date = dt.ToString("yyyy/MM/dd HH:mm:ss");
        exporter.WriteToFile("ID," + id);
        exporter.WriteToFile("Date," + date);
        exporter.WriteToFile("order,angle,position,calibration,modulus,answer");
        int i = 1;
        foreach (Question item in questions)
        {
            string data = i + "," + item.angle + "," + item.position + "," + item.calibration + "," + item.modulus + "," + item.answer;
            exporter.WriteToFile(data);
            i++;
        }
        
    }
}
