using UnityEngine;
using System.Collections;

/// <summary>
/// 卡牌组件
/// </summary>
public class CardObject : MonoBehaviour {

    private CardData cardData;

	void Start () {
	
	}

    /// <summary>
    /// 卡牌组件初始化
    /// </summary>
    /// <param name="cardData">卡牌数据</param>
    void Init(CardData cardData)
    {
        this.cardData = cardData;
    }
	
	void Update () {
	
	}
}
