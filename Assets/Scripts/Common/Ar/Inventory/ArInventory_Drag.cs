using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArInventory_Drag : MonoBehaviour
{
    [SerializeField] private Image GhostImage;

    private ArSO AR;
    private bool isFind = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnMouseButtonDown();
        if (Input.GetMouseButtonUp(0)) OnMouseButtonUp();
        GhostImageMove();
    }

    void OnMouseButtonDown()
    {
        Vector2 MousePosition = GetMousePos();
        Collider2D[] hit = Physics2D.OverlapBoxAll(MousePosition, new Vector2(1, 1), 0);
        if (hit.Length <= 0) return;

        foreach (var Obj in hit)
        {
            if (Obj.GetComponent<ArSOHolder>() != null)
            {
                AR = Obj.GetComponent<ArSOHolder>().GetArSO();
                GhostImage.sprite = AR.characterInfo.Image;
                isFind = true;
                break;
            }
        }
    }

    void OnMouseButtonUp()
    {
        if (!isFind) return;
        isFind = false;

        Vector2 MousePosition = GetMousePos();
        Collider2D[] hit = Physics2D.OverlapBoxAll(MousePosition, new Vector2(1.75f, 1.75f), 0);

        if (hit.Length <= 0) return;

        foreach (var Obj in hit)
        {
            if (Obj.GetComponent<InfoButton>() != null)
            {
                int tempNum = Obj.GetComponent<InfoButton>().GetInfoNum();
                if(ArInventorySelecter.Instance.CanSelect(tempNum)) ArInventorySelecter.Instance.SelectArSO(AR, tempNum);
                break;
            }
        }

        AR = null;
    }

    void GhostImageMove()
    {
        if (!isFind)
        {
            GhostImage.gameObject.SetActive(false);
            return;
        }

        GhostImage.gameObject.SetActive(true);
        Vector3 vec = GetMousePos();
        vec.z = 1f;
        GhostImage.transform.position = vec;
    }

    Vector2 GetMousePos()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        return point;
    }
}
