using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ItemDBSO itemDB;

    public ItemSO[] GetRandomItems(int count)
    {
        int number = 0;
        int[] result = GetUniqueRandomNumbers(count, 0, itemDB.items.Count); // 랜덤 값 배열에 저장
        ItemSO[] items = new ItemSO[count];

        foreach(int num in result) items[number++] = itemDB.items[num];

        return items;
    }

    private int[] GetUniqueRandomNumbers(int count, int min, int max)
    {
        int currentIndex = 0;

        if (count - 1 > max - min || max < min) return null;

        int number = 0;
        int[] numbers = new int[count];

        ResetList(numbers);

        while (currentIndex < count)
        {
            number = Random.Range(min, max);
            if (!ArrayContains(numbers, number)) numbers[currentIndex++] = number;
        }
        return numbers;
    }

    void ResetList(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++) nums[i] = -1;
    }

    bool ArrayContains(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
            if (array[i] == value) return true;
        return false;
    }
}
