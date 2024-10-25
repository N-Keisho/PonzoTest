using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ImageManager : MonoBehaviour
{
    public Sprite[] sp;
    public GameObject imgObj;
    private Image img;
    public int currentNum = 0;
    public Question currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        sp = Resources.LoadAll<Sprite>("Images");
        if (sp == null)
        {
            Debug.Log("Image not found");
            return;
        }

        sp = sp.OrderBy(x => Guid.NewGuid()).ToArray();
        img = imgObj.GetComponent<Image>();
        img.sprite = sp[currentNum];
        SetQuestion();
    }

    public void NextImage()
    {
        if (currentNum < sp.Length - 1)
        {
            currentNum++;
            img.sprite = sp[currentNum];
            SetQuestion();
        }
    }

    private void SetQuestion()
    {
        currentQuestion = new Question();
        string name = img.sprite.name;
        string[] split = name.Split('_');
        currentQuestion.angle = int.Parse(split[0].Substring(1));
        currentQuestion.position = int.Parse(split[1].Substring(1));
        currentQuestion.calibration = int.Parse(split[2].Substring(1));
        currentQuestion.modulus = int.Parse(split[3].Substring(1));        
        currentQuestion.answer = 0;
    }
}
