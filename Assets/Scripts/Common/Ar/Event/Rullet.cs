using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rullet : MonoBehaviour
{
    /* 랜덤으로 고르는 알고리즘과 연출은 각각 따로 굴러간다.
     * 알이나 아이템 이미지를 담을 룰렛 슬롯이 필요하다.
     * 그 룰렛 슬롯에 이미지를 넣게할 함수가 필요하다.
     * 슬롯에 넣을 이미지를 들고있는 리스트 등을 이 스크립트가 가져야한다.
     * 렌덤으로 고른 후에 반환할 값은 알 또는 아이템의 2가지이므로 오버로딩을 사용해야한다.
     * 룰렛 회전이 끝났을 때 중간 칸에 있는 이미지의 번호와 맞는 알 또는 아이템을 반환한다.
     * 
     * 룰렛 슬롯의 이미지를 3개 만들어 세로로 늘어놓는다.
     * 맨 위의 이미지를 알 또는 아이템의 이미지로 바꾼다.
     * 3개의 이미지를 동시에 아래로 내린다.
     * 맨 밑에 있는 이미지는 일정 수치 이하로 내려가면 맨 위로 위치를 옮긴다.
     * 다시 맨 위의 이미지를 새로운 알 또는 아이템의 이미지로 바꾼다.
     * 멈출때까지 반복한다.
     */
    enum RollType
    {
        AR,
        ITEM
    }

    [SerializeField] ArSOArray arInven;
    [SerializeField] ItemDBSO itemInven;
    private List<Sprite> arSprites = new List<Sprite>();
    private List<Sprite> itemSprites = new List<Sprite>();

    [SerializeField] Transform rulletSlot;
    [SerializeField] Image[] slotImages;
    private int lastImage = 2;

    private bool rolling = false;
    private float rollingSpeed;
    private int rollCount;
    private RollType rollType;

    private void Start()
    {
        Setting();
    }

    private void Update()
    {
        Rolling();
    }

    private void Setting()
    {
        //for (int i = arSprites.Count; i < arInven.list.Count; i++)
        //{
        //    arSprites.Add(arInven.list[i].characterInfo.Image);
        //}

        for (int i = arSprites.Count; i < arInven.list.Count; i++)
        {
            arSprites.Add(arInven.list[i].characterInfo.Image);
        }

        for (int i = itemSprites.Count; i < itemInven.items.Count; i++)
        {
            itemSprites.Add(itemInven.items[i].itemIcon);
        }
    }

    public void Ar_RollStart()
    {
        rolling = true;
        rollType = RollType.AR;
        for (int i = 0; i < 3; i++)
        {
            slotImages[lastImage].sprite = arSprites[rollCount];
            if (++rollCount >= arSprites.Count) rollCount = 0;
        }
        rollingSpeed = 30;

        Invoke("Item_Stop", Random.Range(2.5f, 4f));
    }

    public void Item_RollStart()
    {
        rolling = true;
        rollType = RollType.ITEM;
        for(int i=0; i<3; i++)
        {
            slotImages[lastImage].sprite = itemSprites[rollCount];
            if (++rollCount >= itemSprites.Count) rollCount = 0;
        }
        rollingSpeed = 30;

        Invoke("Item_Stop", Random.Range(2.5f, 4f));
    }

    private void Rolling()
    {
        foreach (Image image in slotImages)
        {
            image.transform.Translate(Vector2.down * rollingSpeed * Time.deltaTime);
        }
        if (rolling)
        {
            if (slotImages[lastImage].transform.localPosition.y < -500)
            {
                var num = lastImage + 1;
                if (num > 2) num = 0;
                slotImages[lastImage].transform.localPosition = new Vector2(0, slotImages[num].transform.localPosition.y + 250);
                if (rollType == 0)
                {
                    slotImages[lastImage].sprite = arSprites[rollCount];
                    if (++rollCount >= arSprites.Count) rollCount = 0;
                }
                else
                {
                    slotImages[lastImage].sprite = itemSprites[rollCount];
                    if (++rollCount >= itemSprites.Count) rollCount = 0;
                }
                if (--lastImage < 0) lastImage = 2;
            }
        }
        else
        {
            int num = lastImage + 1 > 2 ? 0 : lastImage + 1;
            if (slotImages[lastImage - 1 < 0 ? 2 : lastImage - 1].transform.localPosition.y >= -250f && slotImages[lastImage].transform.localPosition.y < -249.5f) return;
            else
            {
                rollingSpeed = 0;
                slotImages[lastImage].transform.localPosition = new Vector2(0, slotImages[num].transform.localPosition.y + 250);
                if (--lastImage < 0) lastImage = 2;
            }
        }
    }

    private ArSO Ar_Stop()
    {
        int lengthCount;
        lengthCount = itemInven.items.Count;
        int num = lastImage + 1 > 2 ? 0 : lastImage + 1;
        rollCount = Random.Range(0, lengthCount);

        if (slotImages[lastImage < 0 ? 2 : lastImage].transform.localPosition.y > -249.5f)
        {
            slotImages[lastImage].sprite = arSprites[rollCount];
        }
        else
        {
            slotImages[num].sprite = arSprites[rollCount];
        }

        rolling = false;

        Debug.Log(arInven.list[rollCount].name);
        return arInven.list[rollCount];
    }

    private ItemSO Item_Stop()
    {
        int lengthCount;
        lengthCount = itemInven.items.Count;
        int num = lastImage + 1 > 2 ? 0 : lastImage + 1;
        rollCount = Random.Range(0, lengthCount);

        if (slotImages[lastImage < 0 ? 2 : lastImage].transform.localPosition.y > -249.5f)
        {
            slotImages[lastImage].sprite = itemSprites[rollCount];
        }
        else
        {
            slotImages[num].sprite = itemSprites[rollCount];
        }

        rolling = false;

        Debug.Log(itemInven.items[rollCount].name);
        return itemInven.items[rollCount];
    }
}
