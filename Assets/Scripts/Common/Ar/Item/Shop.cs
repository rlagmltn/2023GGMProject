using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ItemDBSO itemDB;
    private int maxShopItemCount = 5;
    public ItemInfo[] GetRandomItems(int count)
    {
        int[] result = GetUniqueRandomNumbers(count, 0, itemDB.items.Length); // 랜덤 값 배열에 저장
        ItemInfo[] items = new ItemInfo[count];

        foreach(int num in result)
        {
            items[num] = itemDB.items[result[num]];
        }
        return items;
    }

    private int[] GetUniqueRandomNumbers(int count, int min, int max)
    {
        if (count > (max - min + 1) || max < min)
        {
            return null;
        }

        int[] numbers = new int[count];
        int currentIndex = 0;
        while (currentIndex < count)
        {
            int number = Random.Range(min, max + 1);
            if (!ArrayContains(numbers, number))
            {
                numbers[currentIndex] = number;
                currentIndex++;
            }
        }

        return numbers;
    }

    bool ArrayContains(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return true;
            }
        }
        return false;
    }
    private void OnEnable()
    {
        ItemInfo[] itemInfos = GetRandomItems(maxShopItemCount);
        ShowItems(itemInfos);
    }
    void ShowItems(ItemInfo[] itemInfos)
    {

    }
}
