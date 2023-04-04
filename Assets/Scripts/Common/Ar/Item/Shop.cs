using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ItemDBSO itemDB;

    public ItemSO[] GetRandomItems(int count)
    {
        int[] result = GetUniqueRandomNumbers(count, 0, itemDB.items.Length); // 랜덤 값 배열에 저장
        ItemSO[] items = new ItemSO[count];

        int number = 0;

        foreach(int num in result)
        {
            items[number++] = itemDB.items[num];
        }

        Debug.Log(items[0].name);
        return items;
    }

    private int[] GetUniqueRandomNumbers(int count, int min, int max)
    {
        int currentIndex = 0;

        if (count > (max - min + 1) || max < min) return null;

        int[] numbers = new int[count];

        ResetList(numbers);

        while (currentIndex < count)
        {
            int number = Random.Range(min, max);
            if (!ArrayContains(numbers, number))
            {
                numbers[currentIndex++] = number;
            }
        }
        return numbers;
    }

    void ResetList(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = -1;
        }
    }

    bool ArrayContains(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
            if (array[i] == value) return true;
        return false;
    }
}
