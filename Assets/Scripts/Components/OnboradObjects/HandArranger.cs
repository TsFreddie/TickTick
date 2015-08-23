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
    List<GameObject> cardObjectList;

	void Start () {
        cardObjectList = new List<GameObject>();
    }
	
	void Update () {
        float interval = ARRANGER_WIDTH / (cardObjectList.Count + 1);
        float left = transform.position.x - (ARRANGER_WIDTH / 2f);
        float height = 0;
        for (int i = 0; i < cardObjectList.Count; i++)
        {
            left += interval;
            cardObjectList[i].transform.position = new Vector3(left, transform.position.y + height, transform.position.z);
            height += 0.01f;
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
        cardObjectList.Add(newCard);

    }
}
