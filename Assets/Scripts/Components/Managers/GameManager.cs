using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏规则管理组件
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton 定义
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

#region 游戏参数
    // TODO: 添加规则所需参数
#endregion

    void Awake()
    {
        // Singleton 定义
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    CarvedObject[] carvedStones;
    void Start()
    {
        // 挂载操作事件
        GameController.instance.MouseRayDown += MouseRayDown;
        GameController.instance.MouseRayUp += MouseRayUp;
        GameController.instance.MouseRayMove += MouseRayMove;
        
        // TODO: 测试用卡牌，删了这群
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
    }

    /// <summary>
    /// 注册左键按下事件
    /// </summary>
    /// <param name="worldRay"></param>
    void MouseRayDown(Ray worldRay)
    {
        RaycastHit hit;
        // 判断可选物件
        if (Physics.Raycast(worldRay, out hit, 20f, LayerMask.GetMask("Selectable")))
        {
            CardObject card = hit.collider.GetComponent<CardObject>();
            CarvedObject carved = hit.collider.GetComponent<CarvedObject>();
            // 如果是卡牌
            if (card != null)
            {
                card.Pickup();
                return;
            }
            // 如果是刻石
            if (carved != null)
            {
                //TODO
                return;
            }
            Debug.LogError("Can not found selectable object on \"" + name + "\"objcet.");
        }
    }
    /// <summary>
    /// 注册左键抬起事件
    /// </summary>
    /// <param name="worldRay"></param>
    void MouseRayUp(Ray worldRay)
    {
        RaycastHit hit;
        // 判断槽位
        if (Physics.Raycast(worldRay, out hit, 20f, LayerMask.GetMask("Placeable")))
        {
            MagicSlotObject magic = hit.collider.GetComponent<MagicSlotObject>();
            SiteSlotsArranger site = hit.collider.GetComponent<SiteSlotsArranger>();
            StandbySlotsArranger standby = hit.collider.GetComponent<StandbySlotsArranger>();
            // 如果是魔法槽
            if (magic != null)
            {
                // TODO
                return;
            }
            // 如果是场地
            if (site != null)
            {
                //TODO
                return;
            }
            // 如果是待命区
            if (standby != null)
            {
                //TODO
                return;
            }
            Debug.LogError("Can not found placeable object on \"" + name + "\"objcet.");
        }

        // 判断被操作物件
        if (Physics.Raycast(worldRay, out hit, 20f, LayerMask.GetMask("Selectable")))
        {
            CardObject card = hit.collider.GetComponent<CardObject>();
            CarvedObject carved = hit.collider.GetComponent<CarvedObject>();
            // 如果是卡牌
            if (card != null)
            {
                // 跳过，避免报错
                return;
            }
            // 如果是刻石
            if (carved != null)
            {
                //TODO
                return;
            }
            Debug.LogError("Can not found selectable object on \"" + name + "\"objcet.");
        }
    }
    /// <summary>
    /// 注册鼠标移动事件
    /// </summary>
    /// <param name="worldRay"></param>
    void MouseRayMove(Ray worldRay)
    {
        
    }
}
