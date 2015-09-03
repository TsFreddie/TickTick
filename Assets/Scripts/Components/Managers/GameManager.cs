using UnityEngine;
using System.Collections;
using TickTick;

/// <summary>
/// 游戏规则管理组件
/// </summary>
public class GameManager : MonoBehaviour
{
    public HandArranger Hand { get; private set; }
    public StandbySlotsArranger Standby { get; private set; }
    public Rule GameRule { get; private set; }

    public float DayScale { get; private set; }

    private GameDisplay display;
    private CardObject selectedCard;
    private CarvedObject selectedCarved;
    // Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
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
                Destroy(gameObject);
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
        if (GameRule == null)
            GameRule = new DuelRule(0,0,0,5,HandCallback,null,3,100);

        DayScale = GameRule.DayScale;
        // 挂载操作事件
        GameController.Instance.RegisterMouseMove(MousePosMove);
        GameController.Instance.RegisterMouseDown(MouseRayDown);
        GameController.Instance.RegisterMouseUp(MouseRayUp);
        GameController.Instance.RegisterMouseMove(MouseRayMove);

        // TODO: 改变开局条件
        GameRule.Start();
    }
    
    void Update()
    {
        GameRule.Tick();
        display.UpdateDisplay(GameRule);
    }

    public void HandCallback(bool add, int handID, int cardID)
    {
        if (add)
        {
            Hand.AddCard(handID, ResourcesManager.Instance.GetCard(cardID));
        }
        else
        {
            Hand.RemoveCard(handID);
        }
    }

    public void Init(Rule rule)
    {
        GameRule = rule;
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
                    GameRule.DoAction(selectedCard, magic);
                // 如果是待命区
                if (standby != null)
                    GameRule.DoAction(selectedCard, standby);
            }
            if (selectedCarved != null)
            {
                // 如果是场地
                if (site != null)
                    GameRule.DoAction(selectedCarved, site);
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
                    GameRule.DoAction(selectedCard, carved);
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
    /// 鼠标移动事件
    /// </summary>
    /// <param name="mousePosition"></param>
    void MousePosMove(Vector3 mousePosition)
    {
        // 鼠标zDepth = 摄像机与所需平面距离
        mousePosition.z = 9.2f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPos.y = 0.8f;
        if (selectedCarved != null)
        {
            display.ShowConnection(selectedCarved.transform.position, worldPos);
        }
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
                // 跳过: 避免报错
            }
            // 如果是场地
            if (site != null)
            {
                // 跳过: 避免报错
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
                    display.HideConnection();
                    standby.DragIn(selectedCarved);
                }
            }
        }
    }
}
