using UnityEngine;
using System.Collections;

/// <summary>
/// 待命区刻石排列组件
/// 将包含从左至右的多个SlotObject组件
/// </summary>
public class StandbySlotsArranger : MonoBehaviour
{
    public SlotObject[] Slots; //SlotObject组件,从左至右排序
    public bool IsHostile;

    void Awake()
    {
        //敌人的部分不可操作
        if (IsHostile)
        {
            Destroy(GetComponent<BoxCollider>());
        }
    }
}
