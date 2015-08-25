using UnityEngine;
using System.Collections;

public class SiteSlotsArranger : MonoBehaviour
{
    public int SiteID;
    public int ID;
    private CarvedObject carved;
    private CarvedObject hostileCarved;
    void Awake()
    {
        // 使其可放置
        gameObject.layer = LayerMask.NameToLayer("Placeable");
    }

}
