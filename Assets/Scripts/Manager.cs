using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public Timer Timer;
    public InputField StartTimeValueText;
    public GameObject TestPanel;
    public GameObject MakeChoosePanel;
    public GameObject SamplePanel;
    public GameObject VariantsPanel;
    public GameObject ImagePrefab;
    public GameObject ButtonPrefab;

    private NumberManager numberManager;
    private List<int> currentSample;

    void Start()
    {
        instance = this;
        StartTimeValueText.text = Timer.startValue.ToString();
        numberManager = GetComponent<NumberManager>();
        currentSample = new List<int>();
    }

    public void OnTimerStop()
    {
        TestPanel.SetActive(false);
        MakeChoosePanel.SetActive(true);
        GenerateAllNumbers();
    }

    public void ChangeStartTime(string newVal)
    {
        if (newVal == null || newVal == "")
            return;
        Timer.startValue = int.Parse(newVal);
    }

    public void GenerateSample(int numberCount)
    {
        var numbers = numberManager.GenerateNumbers(numberCount);
        currentSample = numbers;
        numberManager.GenerateImages(ImagePrefab, SamplePanel, numbers, true, true);
    }

    public void GenerateAllNumbers()
    {
        var numbers = Enumerable.Range(0, 10).ToList();
        numberManager.GenerateImages(ButtonPrefab, VariantsPanel, numbers, false, false);
    }

    public void CheckCorrect(GameObject button)
    {
        bool isCorrect = false;
        int number = int.Parse(button.name);
        var img = button.GetComponent<Image>();
        if (currentSample.Contains(number))
        {
            img.color = Color.green;
            isCorrect = true;
        }
        else
        {
            img.color = Color.red;
        }

        SaveData("Arabic", 6, "Big", isCorrect);
    }

    public void SaveData(string type, int numberCount, string size, bool isCorrect)
    {
        string writePath = @"result.txt";

        using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
        {
            sw.WriteLine("0 "+ type+" " + numberCount.ToString()+" " + size + " " + ((isCorrect)?"1":"0"));
        }
    }
}
