﻿using UnityEngine;
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
    
    public int CarvedCount
    {
        get
        {
            return carvedObjectList.Count;
        }
    }
    
    private List<CarvedObject> carvedObjectList;
    
    /// <summary>刻石Prefab</summary>
    private GameObject carvedPrefab;
    
    /// <summary>待命区宽度</summary>
    private float width;
    
    private int preferedSlot = -1;
    private int arrangingSlot = -1;
    private bool dragActive = false;
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
        carvedObjectList = new List<CarvedObject>();
        // 获得碰撞箱大小用
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        width = boxCollider.size.x;
        
        // 获得刻石Prefab
        carvedPrefab = ResourcesManager.instance.CarvedPrefab;
    }
    
    void Update()
    {
        // 拖出判定
        if (!dragActive)
        {
            DragOut();
            arrangingSlot = -1;
            preferedSlot = -1;
        }
        
        // 平铺刻石
        float spacing = (width - 1f) / (SlotsCount - 1);
        Vector3 carvedPosition = transform.position;
        float startX = -spacing * (carvedObjectList.Count - ((preferedSlot > -1 && arrangingSlot == -1) ? 0 : 1))/ 2f;
        carvedPosition.x = startX;
        int currentSlot = 0;
        for (int i = 0; i < carvedObjectList.Count; i++)
        {
            
            if (i == arrangingSlot)
            {
                if (arrangingSlot == preferedSlot) currentSlot += 1;
                carvedPosition.x = startX + spacing * preferedSlot;
                carvedObjectList[i].MoveTo(carvedPosition);
            }
            else
            {
                carvedPosition.x = startX + spacing * currentSlot;
                
                carvedObjectList[i].MoveTo(carvedPosition);
                if (i + 1 == preferedSlot) currentSlot += 1;
                currentSlot += 1;
            }
        }
        dragActive = false;
    }
    
    #region 操作
    /// <summary>
    /// 完成刻石整理
    /// </summary>
    public void FinishArrange()
    {
        if (arrangingSlot == -1) return;
        CarvedObject arrangingCarved = carvedObjectList[arrangingSlot];
        carvedObjectList.RemoveAt(arrangingSlot);
        carvedObjectList.Insert(preferedSlot, arrangingCarved);
    }
    
    /// <summary>
    /// 放置刻石操作
    /// </summary>
    /// <param name="card">使用的卡牌</param>
    public void Place(CardObject card)
    {
        Transform cardTransform = card.transform;
        CardData data = card.CardData;
        GameManager.instance.Hand.RemoveCard(card);
        
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
        if (preferedSlot == -1f || preferedSlot >= carvedObjectList.Count)
            carvedObjectList.Add(carved);
        else
            carvedObjectList.Insert(preferedSlot, carved);
    }
    
    /// <summary>
    /// 鼠标拖入
    /// </summary>
    public void DragIn(CarvedObject carved = null)
    {
        arrangingSlot = carvedObjectList.IndexOf(carved);
        GameController.instance.MousePosMove += MousePosMove;
        GameController.instance.MouseUp += MouseUp;
        dragActive = true;
    }
    
    /// <summary>
    /// 鼠标抬起
    /// </summary>
    void MouseUp()
    {
        FinishArrange();
        arrangingSlot = -1;
        DragOut();
    }
    
    /// <summary>
    /// 鼠标拖出
    /// </summary>
    public void DragOut()
    {
        GameController.instance.MousePosMove -= MousePosMove;
        GameController.instance.MouseUp -= DragOut;
        dragActive = false;
    }
    
    private void MousePosMove(Vector3 mousePosition)
    {
        // 鼠标zDepth = 摄像机与所需平面距离
        mousePosition.z = Camera.main.transform.position.y - transform.position.y;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        
        // 判断Prefer的位置
        float spacing = (width - 1f) / (SlotsCount - 1);
        Vector3 carvedPosition = transform.position;
        carvedPosition.x -= spacing * (carvedObjectList.Count + (arrangingSlot == -1 ? 1 : 0)) / 2f;
        preferedSlot = Mathf.Clamp((int)((mouseWorldPos.x - carvedPosition.x) / spacing), 0, carvedObjectList.Count - (arrangingSlot == -1 ? 0 : 1));
    }
    #endregion
}
