using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArInventoryManager : MonoSingleton<ArInventoryManager>
{
    [SerializeField] ArSOArray inven;
    [SerializeField] ArSOArray holder;

    public List<ArSO> InvenList { get { return inven.list; } }
    public List<ArSO> HolderList { get { return holder.list; } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        if (SaveManager.Instance.GameData.IsPlayingGame) return;
        inven.ResetArlist();
        holder.LoadArlist();
        foreach (ArSO so in inven.list)
        {
            so.ResetAll();
        }
        foreach (ArSO so in holder.list)
        {
            so.ResetAll();
        }
    }

    public ArSO HolderToInven(int num)
    {
        var ar = holder.list[num];
        inven.list.Add(ar);
        ar.ResetAll();
        ar.isInGameTake = true;
        holder.list.RemoveAt(num);

        return ar;
    }

    public void HolderToInven(ArSO ar)
    {
        inven.list.Add(ar);
        ar.ResetAll();
        ar.isInGameTake = true;
        holder.list.Remove(ar);
        //ar.ResetAll();
    }
}
