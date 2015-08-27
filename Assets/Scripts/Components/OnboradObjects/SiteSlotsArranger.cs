using UnityEngine;
using System.Collections;

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
    /// <param name="carved"></param>
    public void Place(CarvedObject carved)
    {
        if (hostileCarved == null)
        {
            carved.Invade();
            carved.MoveTo(transform.position);
            this.carved = carved;
        }
    }

}
