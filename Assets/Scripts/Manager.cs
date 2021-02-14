using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Timer Timer;
    public InputField StartTimeValueText;
    public GameObject TestPanel;
    public GameObject MakeChoosePanel;

    void Start()
    {
        StartTimeValueText.text = Timer.startValue.ToString();
    }

    public void OnTimerStop()
    {
        TestPanel.SetActive(false);
        MakeChoosePanel.SetActive(true);
    }

    public void ChangeStartTime(string newVal)
    {
        if (newVal == null || newVal == "")
            return;
        Timer.startValue = int.Parse(newVal);
    }
}
