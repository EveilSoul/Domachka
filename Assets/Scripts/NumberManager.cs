using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{
    public List<int> GenerateNumbers(int count)
    {
        var res = new List<int>();
        var notUsedNumbers = Enumerable.Range(0, 10).ToList();
        for (int i = 0; i < count; i++)
        {
            var rand = Random.Range(0, notUsedNumbers.Count());
            res.Add(notUsedNumbers[rand]);
            Debug.Log(res[i]);
            notUsedNumbers.RemoveAt(rand);
        }
        return res;
    }

    public void GenerateImages(GameObject prefab, GameObject parent, List<int> numbers, bool isArabic, bool isImage)
    {
        var images = GetNumberImages(numbers, isArabic);
        for (int i = 0; i < numbers.Count; i++)
        {
            if (isImage)
            {
                var img = Instantiate(prefab, parent.transform).GetComponent<Image>();
                img.sprite = images[i];
            }
            else
            {
                var button = Instantiate(prefab, parent.transform).GetComponent<Button>();
                if(isArabic)
                    button.transform.GetChild(0).GetComponent<Image>().sprite = images[i];
                else
                {
                    button.transform.GetChild(0).gameObject.SetActive(false);
                    button.GetComponent<Image>().sprite = images[i];
                }
                button.name = i.ToString();
            }
           
            //img.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
        }
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
