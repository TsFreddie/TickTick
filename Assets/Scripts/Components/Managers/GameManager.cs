using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏规则管理组件
/// </summary>
public class GameManager : MonoBehaviour
{
    public HandArranger Hand { get; private set; }
    public StandbySlotsArranger Standby { get; private set; }
    public float DayScale { get; private set; }
    
    private GameDisplay display = null;
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
            
        display = GetComponent<GameDisplay>();
        if (display == null)
            Debug.LogError("Can not found GameDisplay");
    }
    void Start()
    {
        if (rule == null)
            rule = new DuelRule(0,0,0,60,3,100);
        
        DayScale = rule.DayScale;
        // 挂载操作事件
        GameController.instance.MouseRayDown += MouseRayDown;
        GameController.instance.MouseRayUp += MouseRayUp;
        GameController.instance.MouseRayMove += MouseRayMove;

        // TODO: 测试用卡牌，删了这群
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 1, 5, CardData.ElementType.Earth, 5, 5, 50));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 2, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 3, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 4, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 5, 5, CardData.ElementType.Earth, 5, 5, 5));
        FindObjectOfType<HandArranger>().AddCard(new MeleeCardData(1, 1, 5, CardData.ElementType.Earth, 5, 5, 100));

        
        // TODO: 改变开局条件
        rule.Start();
    }
    
    void Update()
    {
        float deltaTime = Time.deltaTime;
        rule.Tick();
        display.UpdateDisplay(rule);
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
                carved.Activate();
                selectedCarved = carved;
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
                if (standby != null)
                    standby.FinishArrange();   
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
        
        // 取消所有指向
        display.HideConnection();
        // 重置选择
        selectedCard = null;
        selectedCarved = null;
    }
    
    /// <summary>
    /// 鼠标碰撞判定事件
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
                {
                    standby.DragIn();
                }
                if (selectedCarved != null)
                {
                    standby.DragIn(selectedCarved);
                }
            }
        }
    }
}
