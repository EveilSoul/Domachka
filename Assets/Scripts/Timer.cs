using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    public Manager Manager;
    
    public int startValue = 5;
    private float currentValue;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        currentValue -= Time.deltaTime;
        if (currentValue < 0)
            currentValue = 0;
        TimerText.text = (Mathf.Round(currentValue*100)/100).ToString("F2");

        if(currentValue == 0)
        {
            Manager.OnTimerStop();
        }
    }
}
