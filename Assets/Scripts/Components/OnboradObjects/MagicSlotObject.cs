using UnityEngine;
using System.Collections;

/// <summary>
/// 魔法槽位组件
/// </summary>
public class MagicSlotObject : MonoBehaviour
{
    #region 参数
    public int _id;
    public bool _isHostile;
    #endregion
    private GameObject carvedPrefab;
    private CarvedObject carvedObject;
    void Awake()
    {
        gameObject.layer = _isHostile ? LayerMask.NameToLayer("Pass") : LayerMask.NameToLayer("Placeable");
       
    }
    
    void Start()
    {
        carvedPrefab = ResourcesManager.Instance.CarvedPrefab;
    }
    
    #region 操作
    /// <summary>
    /// 放置刻石操作
    /// </summary>
    /// <param name="card">使用的卡牌</param>
    public void Place(CardObject card)
    {
        Transform cardTransform = card.transform;
        CardData data = card.CardData;
        GameManager.Instance.Hand.RemoveCard(card);
        
        Place(cardTransform.position, data);
    }
    
    /// <summary>
    /// 生成刻石
    /// </summary>
    /// <param name="appearPosition">出现位置</param>
    /// <param name="data">卡牌数据</param>
    public void Place(Vector3 appearPosition, CardData data)
    {
        GameObject newCarved = Instantiate(carvedPrefab);
        newCarved.transform.position = appearPosition;
        CarvedObject carved = newCarved.GetComponent<CarvedObject>();
        carved.Init(data);
        carved.MoveTo(transform.position);
        carvedObject = carved;
    }
    #endregion
}
