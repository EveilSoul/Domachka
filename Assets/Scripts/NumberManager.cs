
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public List<Tuple<int, NumberSize>> GenerateImages(GameObject prefab, GameObject parent, List<int> numbers, bool isArabic, bool isImage)
    {
        List<Tuple<int, NumberSize>> result = new List<Tuple<int, NumberSize>>();
        var images = GetNumberImages(numbers, isArabic);
        List<RectTransform> imagesTransforms = new List<RectTransform>();
        for (int i = 0; i < numbers.Count; i++)
        {
            if (isImage)
            {
                var size = (NumberSize)Random.Range(0, 3);
                result.Add(Tuple.Create(numbers[i], size));
                var img = Instantiate(prefab, parent.transform).GetComponent<Image>();
                img.sprite = images[i];
                RectTransform rectTransform = img.GetComponent<RectTransform>();
                rectTransform.sizeDelta = GetSizeDelta(size);
                imagesTransforms.Add(rectTransform);
            }
            else
            {
                var button = Instantiate(prefab, parent.transform).GetComponent<Button>();
                if (isArabic)
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

        if (isImage)
            PlaceImages(imagesTransforms);
        return result;
    }

    private void PlaceImages(List<RectTransform> imagesTransforms)
    {
        var center = new Vector2(Screen.width, Screen.height) / 2;
        float offset = 0;
        float numberOffset = 2;
        var totalLen = imagesTransforms.Sum(x => x.sizeDelta.x) + numberOffset * imagesTransforms.Count - 1;
        foreach (var transform in imagesTransforms)
        {
            transform.anchoredPosition = new Vector2(totalLen / 2 - transform.sizeDelta.x / 2 - offset, 0);
            offset += transform.sizeDelta.x + numberOffset;
        }
    }

    private Vector2 GetSizeDelta(NumberSize size)
    {
        var normalSize = new Vector2(50, 50);
        switch (size)
        {
            case NumberSize.Big:
                return 1.5f * normalSize;
            case NumberSize.Medium:
                return normalSize;
            case NumberSize.Small:
                return 0.5f * normalSize;
        }
        return Vector2.zero;
    }

    public static NumberSize DefineSizeByTransform(RectTransform img)
    {
        var normalSize = new Vector2(50, 50).x;
        if (img.sizeDelta.x > normalSize)
            return NumberSize.Big;
        else if (img.sizeDelta.x < normalSize)
            return NumberSize.Small;
        else return NumberSize.Medium;
    }

    public static List<Sprite> GetNumberImages(List<int> numbers, bool isArabic)
    {
        List<Sprite> result = new List<Sprite>();
        var dir = isArabic ? "NewArabic" : "Cube";
        foreach (var num in numbers)
        {
            result.Add(Resources.Load<Sprite>($"{dir}/{num}"));
        }
        return result;
    }
}


public enum NumberSize
{
    Small,
    Medium,
    Big
}