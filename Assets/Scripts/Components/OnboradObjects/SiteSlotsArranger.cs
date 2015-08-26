using UnityEngine;
using System.Collections;

public class SiteSlotsArranger : MonoBehaviour
{
    public int _siteID;
    public int _id;
    private CarvedObject carved;
    private CarvedObject hostileCarved;
    void Awake()
    {
        // 使其可放置
        gameObject.layer = LayerMask.NameToLayer("Placeable");
    }
    
    public void Place(CarvedObject carved)
    {
        
    }

}
