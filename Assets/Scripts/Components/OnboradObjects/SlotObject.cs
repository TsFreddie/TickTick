using UnityEngine;
using System.Collections;

/// <summary>
/// 槽位组件
/// </summary>
public class SlotObject : MonoBehaviour
{
    public enum SlotType
    {
        Standby,      // 准备位
        Front,        // 前线位
        Wizard,       // 巫师位
        Magic,        // 魔法卡位
    }
    #region 参数
    public int ID;
    public SlotType Type;
    public bool IsHostile;
    #endregion

    void Awake()
    {
        //准备位交给StandbySlotsArranger处理, 前线位交给SiteSlotsArranger处理, 巫师位不需要操作, 敌人的部分不可操作
        if (Type != SlotType.Magic || IsHostile)
        {
            Destroy(GetComponent<BoxCollider>());
        }
    }
}
