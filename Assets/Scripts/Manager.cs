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
    public GameObject MainPanel;
    public GameObject MakeChoosePanel;
    public GameObject SamplePanel;
    public GameObject VariantsPanel;
    public GameObject ImagePrefab;
    public GameObject ButtonPrefab;
    public Text QuestionCounterText;
    public Button NextQuestionButton;

    private NumberManager numberManager;
    private List<int> currentSample;

    private int totalCheckCount;
    private int totalTestCount;

    private int number—ount;

    private List<System.Tuple<int, NumberSize>> numberSizes;
    private List<bool> tests;

    void Start()
    {
        instance = this;
        StartTimeValueText.text = Timer.startValue.ToString();
        numberManager = GetComponent<NumberManager>();
        currentSample = new List<int>();
        InitializeTests();
    }

    private void InitializeTests()
    {
        tests = new List<bool>();
        tests.AddRange(Enumerable.Range(0, 10).Select(x => true));
        for (int i = 0; i < 10; i++)
        {
            var index = Random.Range(0, tests.Count);
            tests.Insert(index, false);
        }
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
        QuestionCounterText.text = $"¬ÓÔÓÒ {totalTestCount + 1} ËÁ {tests.Count}";
        totalCheckCount = numberCount;
        number—ount = numberCount;
        var numbers = numberManager.GenerateNumbers(numberCount);
        currentSample = numbers;
        numberSizes = numberManager.GenerateImages(ImagePrefab, SamplePanel, numbers, tests[totalTestCount], true);
        NextQuestionButton.interactable = false;
    }

    public void GenerateAllNumbers()
    {
        var numbers = Enumerable.Range(0, 10).ToList();
        numberManager.GenerateImages(ButtonPrefab, VariantsPanel, numbers, tests[totalTestCount], false);
    }

    public void CheckCorrect(GameObject button)
    {
        if (totalCheckCount > 0)
        {
            int number = int.Parse(button.name);
            var img = button.GetComponent<Image>();
            if (currentSample.Contains(number))
            {
                img.color = Color.green;
                var size = numberSizes.First(x => x.Item1 == number).Item2;
                SaveData(tests[totalTestCount] ? "Arabic" : "Pictogram", number—ount, size.ToString(), true);
                currentSample.Remove(number);
            }
            else
            {
                img.color = Color.red;
            }
            totalCheckCount--;
        }

        if (totalCheckCount == 0)
        {
            NextQuestionButton.interactable = true;
            foreach(var num in currentSample)
            {
                var size = numberSizes.First(x => x.Item1 == num).Item2;
                SaveData(tests[totalTestCount] ? "Arabic" : "Pictogram", number—ount, size.ToString(), false);
            }
            totalCheckCount = -1;
        }
    }

    public void DisplayNext()
    {
        totalTestCount++;

        if(totalTestCount == 20)
        {
            ExitToMainMenu();
            return;
        }

        int count = SamplePanel.transform.childCount;
        Enumerable.Range(0, count).ToList().ForEach(i => Destroy(SamplePanel.transform.GetChild(i).gameObject));
        count = VariantsPanel.transform.childCount;
        Enumerable.Range(0, count).ToList().ForEach(i => Destroy(VariantsPanel.transform.GetChild(i).gameObject));
        TestPanel.SetActive(true);
        MakeChoosePanel.SetActive(false);

        GenerateSample(number—ount);
    }

    public void ExitToMainMenu()
    {
        int count = SamplePanel.transform.childCount;
        Enumerable.Range(0, count).ToList().ForEach(i => Destroy(SamplePanel.transform.GetChild(i).gameObject));
        count = VariantsPanel.transform.childCount;
        Enumerable.Range(0, count).ToList().ForEach(i => Destroy(VariantsPanel.transform.GetChild(i).gameObject));
        InitializeTests();
        TestPanel.SetActive(false);
        MakeChoosePanel.SetActive(false);
        MainPanel.SetActive(true);
        totalTestCount = 0;
    }

    public void SetNumberCount(int Òount)
    {
        number—ount = Òount;
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
