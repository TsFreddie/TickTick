using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TickTick;

/// <summary>
/// 手牌组件
/// </summary>
public class HandArranger : MonoBehaviour {

    /// <summary>手牌组件宽度</summary>
    private float width = 10f;

    private GameObject cardPrefab;

    /// <summary>
    /// 手牌列表
    /// </summary>
    List<CardObject> cardObjectList;

    void Start () {
        cardObjectList = new List<CardObject>();
        // 获得碰撞箱宽度
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        width = boxCollider.size.x;
        
        // 获得卡牌Prefab
        cardPrefab = ResourcesManager.Instance.CardPrefab;
    }
    
    void Update ()
    {
        // 平铺卡牌
        float spacing = width / (cardObjectList.Count + 1);
        float left = transform.position.x - (width / 2f);
        // 使用向量height，让卡牌向镜头中间方向提升高度，避免视觉上造成偏移。
        Vector3 height = (Camera.main.transform.position - transform.position).normalized * 0.02f * cardObjectList.Count;
        for (int i = 0; i < cardObjectList.Count; i++)
        {
            left += spacing;
            if (!cardObjectList[i].InUse)
                cardObjectList[i].MoveTo(new Vector3(left, transform.position.y, transform.position.z) + height);
            height -= (Camera.main.transform.position - transform.position).normalized * 0.02f;
        }
    }

    public void AddCard(int handID, CardData data)
    {
        GameObject newCard = Instantiate(cardPrefab);
        CardObject card = newCard.GetComponent<CardObject>();
        if (card == null)
        {
            Debug.LogError("Can not find CardObject.");
            Destroy(newCard);
            return;
        }
        card.Init(handID, data);
        cardObjectList.Add(card);

    }
    
    public void RemoveCard(int handID)
    {
        CardObject card = cardObjectList.First(c => c.HandID == handID);
        if (!cardObjectList.Remove(card))
            Debug.LogError("Can not found the CardObject in cardObjectList when removing.");
        Destroy(card.gameObject);   
    }
}
