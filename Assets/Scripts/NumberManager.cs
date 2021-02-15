using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        var test = new List<int>() { 1, 2, 3, 4, 5, 6 };
        var images = GetNumberImages(test, true);
        for (int i = 0; i < test.Count; i++)
        {
            var img = new GameObject().AddComponent<Image>();
            img.gameObject.transform.SetParent(parent.transform);
            img.sprite = images[i];
            //img.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static List<Sprite> GetNumberImages(List<int> numbers, bool isArabic)
    {
        List<Sprite> result = new List<Sprite>();
        var dir = isArabic ? "Arabic" : "Cube";
        foreach (var num in numbers)
        {
            result.Add(Resources.Load<Sprite>($"{dir}/{num}"));
        }
        return result;
    }
}
