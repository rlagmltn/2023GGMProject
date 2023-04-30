using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rullet : MonoBehaviour
{
    /* �������� ���� �˰���� ������ ���� ���� ��������.
     * ���̳� ������ �̹����� ���� �귿 ������ �ʿ��ϴ�.
     * �� �귿 ���Կ� �̹����� �ְ��� �Լ��� �ʿ��ϴ�.
     * ���Կ� ���� �̹����� ����ִ� ����Ʈ ���� �� ��ũ��Ʈ�� �������Ѵ�.
     * �������� �� �Ŀ� ��ȯ�� ���� �� �Ǵ� �������� 2�����̹Ƿ� �����ε��� ����ؾ��Ѵ�.
     * �귿 ȸ���� ������ �� �߰� ĭ�� �ִ� �̹����� ��ȣ�� �´� �� �Ǵ� �������� ��ȯ�Ѵ�.
     * 
     * �귿 ������ �̹����� 3�� ����� ���η� �þ���´�.
     * �� ���� �̹����� �� �Ǵ� �������� �̹����� �ٲ۴�.
     * 3���� �̹����� ���ÿ� �Ʒ��� ������.
     * �� �ؿ� �ִ� �̹����� ���� ��ġ ���Ϸ� �������� �� ���� ��ġ�� �ű��.
     * �ٽ� �� ���� �̹����� ���ο� �� �Ǵ� �������� �̹����� �ٲ۴�.
     * ���⶧���� �ݺ��Ѵ�.
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
