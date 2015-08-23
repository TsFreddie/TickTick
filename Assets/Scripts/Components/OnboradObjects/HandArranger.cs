using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 手牌组件
/// </summary>
public class HandArranger : MonoBehaviour {

    const float ARRANGER_WIDTH = 10f;

    public GameObject cardPrefab;

    /// <summary>
    /// 手牌列表
    /// </summary>
    List<CardObject> cardObjectList;

	void Start () {
        cardObjectList = new List<CardObject>();
    }
	
	void Update () {
        float interval = ARRANGER_WIDTH / (cardObjectList.Count + 1);
        float left = transform.position.x - (ARRANGER_WIDTH / 2f);
        // 使用向量height，让卡牌向镜头中间方向提升高度，避免视觉上造成偏移。
        Vector3 height = (Camera.main.transform.position - transform.position).normalized * 0.01f * cardObjectList.Count;
        for (int i = 0; i < cardObjectList.Count; i++)
        {
            left += interval;
            cardObjectList[i].MoveTo(new Vector3(left, transform.position.y, transform.position.z) + height);
            height -= (Camera.main.transform.position - transform.position).normalized * 0.01f;
        }
    }

    public void AddCard(CardData data)
    {
        GameObject newCard = Instantiate(cardPrefab);
        CardObject card = newCard.GetComponent<CardObject>();
        if (card == null)
        {
            Debug.LogError("Can not find CardObject.");
            Destroy(newCard);
            return;
        }
        card.Init(data);
        cardObjectList.Add(card);

    }
}
