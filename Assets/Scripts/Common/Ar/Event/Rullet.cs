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
    private float rollingSpeed = 30;
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

        for (int i = itemSprites.Count; i < itemInven.items.Count; i++)
        {
            itemSprites.Add(itemInven.items[i].itemIcon);
        }
    }

    public void Ar_RollStart()
    {
        rolling = true;
        rollType = RollType.AR;
    }

    public void Item_RollStart()
    {
        rolling = true;
        rollType = RollType.ITEM;
        for(int i=0; i<3; i++)
        {
            slotImages[0].sprite = itemInven.items[0].itemIcon;
        }
        rollCount = 3;
    }

    private void Rolling()
    {
        if (!rolling) return;

        foreach(Image image in slotImages)
        {
            image.transform.Translate(Vector2.down * rollingSpeed * Time.deltaTime);
        }
        if(slotImages[lastImage].transform.localPosition.y<-600)
        {
            var num = lastImage + 1;
            if (num > 2) num = 0;
            slotImages[lastImage].transform.localPosition = new Vector2(0, slotImages[num].transform.localPosition.y + 300);
            if(rollType == 0)
            {
                slotImages[lastImage].sprite = arInven.list[rollCount].characterInfo.Image;
                if (++rollCount >= arInven.list.Count) rollCount = 0;
            }
            else
            {
                slotImages[lastImage].sprite = itemInven.items[rollCount].itemIcon;
                if (++rollCount >= itemInven.items.Count) rollCount = 0;
            }
            if (--lastImage<0) lastImage = 2;
        }
    }
}
