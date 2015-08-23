using UnityEngine;
using System.Collections;

/// <summary>
/// 魔法槽位组件
/// </summary>
public class MagicSlotObject : MonoBehaviour
{
    #region 参数
    public int ID;
    public bool IsHostile;
    #endregion

    void Awake()
    {
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
}
