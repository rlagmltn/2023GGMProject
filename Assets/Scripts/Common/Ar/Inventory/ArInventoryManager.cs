using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArInventoryManager : MonoSingleton<ArInventoryManager>
{
    [SerializeField] ArSOArray inven;
    [SerializeField] ArSOArray holder;

    public List<ArSO> InvenList { get { return inven.list; } }
    public List<ArSO> HolderList { get { return holder.list; } }

    private void Start()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        inven.ResetArlist();
        holder.LoadArlist();
    }

    public void HolderToInven(int num)
    {
        Debug.Log(holder.list[num].name);
        inven.list.Add(holder.list[num]);
        holder.list.RemoveAt(num);
    }

    public void HolderToInven(ArSO ar)
    {
        inven.list.Add(ar);
        holder.list.Remove(ar);
    }
}
