using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberManager : MonoBehaviour
{
    public TMP_Text currentText;
    public TMP_Text totalText;
    public GameObject imgManager;
    private ImageManager imageManager;
    // Start is called before the first frame update
    void Start()
    {
        imageManager = imgManager.GetComponent<ImageManager>();
        currentText.text = imageManager.currentNum + 1 + "";
        totalText.text = imageManager.sp.Length + "";
    }

    // Update is called once per frame
    void Update()
    {
        currentText.text = imageManager.currentNum + 1 + "";
        if(totalText.text == "0")
        {
            totalText.text = imageManager.sp.Length + "";
        }
    }
}
