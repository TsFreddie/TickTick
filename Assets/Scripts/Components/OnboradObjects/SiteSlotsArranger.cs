using UnityEngine;
using System.Collections;
using TickTick;

public class SiteSlotsArranger : MonoBehaviour
{
    public int _siteID;
    public int _id;

    public bool IsAvailable
    {
        get
        {
            return carved == null;
        }
    }

    private CarvedObject carved;
    private CarvedObject hostileCarved;
    void Awake()
    {
        // 使其可放置
        gameObject.layer = LayerMask.NameToLayer("Placeable");
    }
    
    void Update()
    {      
        if (carved != null)
        {
            if (!carved.IsProcessing())
            {
                carved = null;
            }
        }
        if (hostileCarved != null)
        {
            if (!hostileCarved.IsProcessing())
            {
                hostileCarved = null;
            }
        }
    }

    /// <summary>
    /// 放置刻石
    /// </summary>
    /// <param name="carved">刻石组件</param>
    public void Place(CarvedObject carved)
    {
        if (hostileCarved == null)
        {
            carved.Invade(this);
            carved.MoveTo(transform.position);
            this.carved = carved;
        }
    }

    /// <summary>
    /// 得分CallBack
    /// </summary>
    public void CallBackScore(Rule rule)
    {
        if (rule.GetType() == typeof(DuelRule))
        {
            ((DuelRule)rule).Score(_siteID, carved.Power);
        }
    }

}
