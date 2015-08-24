using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏规则管理组件
/// </summary>
public class GameManager : MonoBehaviour
{
    public HandArranger Hand { get; private set; }
    public StandbySlotsArranger Standby { get; private set; }
    private CardObject selectedCard = null;
    private CarvedObject selectedCarved = null;
    private Rule rule;

    // Singleton
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        // Singleton
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }

        Hand = FindObjectOfType<HandArranger>();
        Standby = FindObjectOfType<StandbySlotsArranger>();
        if (Hand == null)
            Debug.LogError("Can not found HandArranger.");
        if (Standby == null)
            Debug.LogError("Can not found StandbySlotsArranger.");
    }
    void Start()
    {
        if (rule == null)
            rule = new DuelRule(0,0,0,3,100);
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
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
    }

    public void Init(Rule rule)
    {
        this.rule = rule;
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
                selectedCard = card;
            }
            // 如果是刻石
            if (carved != null)
            {
                //TODO
            }
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

            if (selectedCard != null)
            {
                // 如果是魔法槽
                if (magic != null)
                    rule.DoAction(selectedCard, magic);
                // 如果是待命区
                if (standby != null)
                    rule.DoAction(selectedCard, standby);
            }
            if (selectedCarved != null)
            {
                // 如果是场地
                if (site != null)
                    rule.DoAction(selectedCarved, site);     
            }
        }

        // 判断被操作物件
        if (Physics.Raycast(worldRay, out hit, 20f, LayerMask.GetMask("Selectable")))
        {
            CarvedObject carved = hit.collider.GetComponent<CarvedObject>();
            if (selectedCard != null)
            {
                // 如果是刻石
                if (carved != null)
                {
                    rule.DoAction(selectedCard, carved);
                }
            }
        }

        // 重置选择
        selectedCard = null;
    }
    /// <summary>
    /// 注册鼠标移动事件
    /// </summary>
    /// <param name="worldRay"></param>
    void MouseRayMove(Ray worldRay)
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
                // 跳过，避免报错
            }
            // 如果是场地
            if (site != null)
            {
                // 跳过，避免报错
            }
            // 如果是待命区
            if (standby != null)
            {
                if (selectedCard != null)
                    standby.DragIn();
            }
        }
    }
}
