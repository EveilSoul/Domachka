using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{

    private void Start()
    {
        var res = GenerateNumbers(6);
        //GenerateVariants(3, res);
    }

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

    //public List<List<int>> GenerateVariants(int count, List<int> example)
    //{
    //    var res = new List<List<int>>();

    //    for(int i = 0; i < count; i++)
    //    {
    //        res.Add(new List<int>());
    //        for(int j = 0; j < example.Count; j++)
    //        {
    //            res[i].Add(example[j]);
    //        }

    //        // ���������� ����, ������� ����� �������� 
    //        var countOfNumbersToRandom = Random.Range(1, (example.Count - 2) >= 4 ? 4 : example.Count - 2);
    //        // ����� �������� ����� ����� ����� ������ � ���������. ����� ����� ����� ��� �� ���� ��������, ����� �� �������.
    //        var notUsedNumberIndex = Enumerable.Range(0, example.Count - 2).ToList();
    //        // ��� �� �������������� �����.
    //        var notUsedNumbers = Enumerable.Range(0, 10).Except(example).ToList();

    //        for (int j = 0; j < countOfNumbersToRandom; j++)
    //        {
    //            // ���������� ������ �����, ������� ����� ������
    //            var randIndex = Random.Range(0, notUsedNumberIndex.Count);
    //            // ���������� ������ ����� �����
    //            var rand = Random.Range(0, notUsedNumbers.Count());
    //            notUsedNumbers.Add(res[i][notUsedNumberIndex[randIndex] + 1]);
    //            res[i][notUsedNumberIndex[randIndex] + 1] = notUsedNumbers[rand];
    //            notUsedNumbers.RemoveAt(rand);
    //            notUsedNumberIndex.RemoveAt(randIndex);
                
    //        }
    //    }

    //    return res;
    //}
}
