using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ItemDBSO itemDB;
    private int maxShopItemCount = 5;

    public ItemSO[] GetRandomItems(int count)
    {
        int[] result = GetUniqueRandomNumbers(count, 0, itemDB.items.Length); // ·£´ý °ª ¹è¿­¿¡ ÀúÀå
        ItemSO[] items = new ItemSO[count];

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
        for(int i=0; i<numbers.Length; i++)
        {
            numbers[i] = -1;
        }
        int currentIndex = 0;
        int loop = 0;
        while (currentIndex < count)
        {
            int number = Random.Range(min, max);
            if (!ArrayContains(numbers, number))
            {
                numbers[currentIndex] = number;
                currentIndex++;
                Debug.Log(number);
            }
            loop++;
            if (loop > 10000)
            {
                Debug.LogError("¹÷À¸");
                break;
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
    /*private void OnEnable()
    {
        ItemSO[] itemInfos = GetRandomItems(maxShopItemCount);
        ShowItems(itemInfos);
    }
    void ShowItems(ItemSO[] itemInfos)
    {

    }*/
}
