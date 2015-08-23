using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 待命区刻石排列组件
/// 将包含从左至右的多个刻石组件
/// </summary>
public class StandbySlotsArranger : MonoBehaviour
{
    public bool IsHostile;
    public int SlotsCount = 10;
    private List<CarvedObject> carved;
    /// <summary>待命区宽度</summary>
    private float width;
    void Awake()
    {
        //敌人的部分不可操作
        if (IsHostile)
        {
            gameObject.layer = LayerMask.NameToLayer("Pass");
        }
        else
        {
            // 使其可放置
            gameObject.layer = LayerMask.NameToLayer("Placeable");
        }
    }
    
    void Start()
    {
        // 获得碰撞箱大小用
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        width = boxCollider.size.x;
    }
    
    #region 操作
    /// <summary>
    /// 放置刻石操作
    /// </summary>
    /// <param name="card">使用的卡牌</param>
    void Place(CardObject card)
    {
        //TODO
    }
    
    /// <summary>
    /// 生成刻石
    /// </summary>
    /// <param name="data">卡牌数据</param>
    void Place(CardData data)
    {
        //TODO
    }
    #endregion
}
