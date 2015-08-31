using UnityEngine;
using System.Collections;
using TickTick;

/// <summary>
/// 卡牌组件,卡牌组件本身不储存卡牌内容数据,只存储卡牌Data类
/// </summary>
public class CardObject : MonoBehaviour
{
    public CardData CardData
    {
        get { return cardData; }
    }
    public bool InUse { get; set; }

    private CardData cardData;
    private Vector3 cardPosition;
    
    private bool initialized;

    private CardDisplay ui;

    #region 初始化
    void Awake()
    {
        // 使其可选
        gameObject.layer = LayerMask.NameToLayer("Selectable");
        ui = GetComponent<CardDisplay>();
        if (ui == null)
        {
            Debug.LogError("CardUIController not found");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (!initialized)
        {
            Debug.LogError("Uninitialzed CardObject, Killing it.");
            Destroy(gameObject);
        }
        
        GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        
        InUse = false;
    }
    
    void Update()
    {
        // 移动物体
        if (cardPosition != transform.position)
        {
            Vector3 displacement = cardPosition - transform.position;
            transform.position += displacement.normalized * (displacement.magnitude * 10f * Time.deltaTime);
            if (displacement.magnitude < 0.01f)
            {
                transform.position = cardPosition;
            }
        }
    }
    
    /// <summary>
    /// 卡牌组件初始化
    /// </summary>
    /// <param name="data">卡牌数据</param>
    public void Init(CardData data)
    {
        cardData = data;
        initialized = true;
        if (data.GetType() == typeof(MeleeCardData))
            MeleeInit((MeleeCardData)data);
        if (data.GetType() == typeof(RangeCardData))
            RangeInit((RangeCardData)data);
        if (data.GetType() == typeof(WizardCardData))
            WizardInit((WizardCardData)data);
        if (data.GetType() == typeof(MagicCardData))
            MagicInit((MagicCardData)data);
        if (data.GetType() == typeof(SummonCardData))
            SummonInit((SummonCardData)data);
    }

    /// <summary>
    /// 近战卡牌初始化
    /// </summary>
    /// <param name="data">近战卡牌数据</param>
    private void MeleeInit(MeleeCardData data)
    {
        ui.SetType(CardType.Melee);
        ui.SetCost(data.Cost);
        ui.SetBooster(data.Booster);
        ui.SetHealth(data.Health);
        ui.SetPower(data.Power);
        ui.SetAgility(data.Agility);
    }

    /// <summary>
    /// 远程卡牌初始化
    /// </summary>
    /// <param name="data">远程卡牌数据</param>
    private void RangeInit(RangeCardData data)
    {
        ui.SetType(CardType.Range);
        ui.SetCost(data.Cost);
        ui.SetBooster(data.Booster);
        ui.SetHealth(data.Health);
        ui.SetPower(data.Power);
        ui.SetLoss(data.Loss);
        ui.SetAgility(data.Agility);
    }

    /// <summary>
    /// 巫师卡牌初始化
    /// </summary>
    /// <param name="data">巫师卡牌数据</param>
    private void WizardInit(WizardCardData data)
    {
        ui.SetType(CardType.Wizard);
        ui.SetCost(data.Cost);
        ui.SetBooster(data.Booster);
        ui.SetPower(data.Power);
    }

    /// <summary>
    /// 魔法卡牌初始化
    /// </summary>
    /// <param name="data">魔法卡牌数据</param>
    private void MagicInit(MagicCardData data)
    {
        ui.SetType(CardType.Magic);
        ui.SetCost(data.Cost);
        ui.SetBooster(data.Booster);
        ui.SetAgility(data.Agility);
    }

    /// <summary>
    /// 召唤卡牌初始化,health存放能量,power存放能量槽上限
    /// </summary>
    /// <param name="data">召唤卡牌数据</param>
    private void SummonInit(SummonCardData data)
    {
        ui.SetType(CardType.Summon);
        ui.SetCost(data.Cost);
        ui.SetBooster(data.Booster);
        ui.SetHealth(0);
        ui.SetPower(data.Energy);
        ui.SetAgility(data.Agility);
    }

    #endregion

    #region 操作
    /// <summary>
    /// 缓慢移动到
    /// </summary>
    public void MoveTo(Vector3 position)
    {
        cardPosition = position;
    }
    
    /// <summary>
    /// 跳至指定位置
    /// </summary>
    public void SetPosition(Vector3 position)
    {
        cardPosition = position;
        transform.position = position;
    }
    
    /// <summary>
    /// 捡起操作
    /// </summary>
    public void Pickup()
    {
        // 挂载事件
        GameController.Instance.RegisterMouseMove(MousePosMove);
        GameController.Instance.RegisterMouseUp(MouseUp);
        // 取消该物件的可选层，使其不参与鼠标判定
        gameObject.layer = LayerMask.NameToLayer("Pass");
        GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        InUse = true;
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    private void MouseUp()
    {
        GameController.Instance.UnregisterMouseMove(MousePosMove);
        GameController.Instance.UnregisterMouseUp(MouseUp);
        // 恢复该物件的可选层，使其重新参与鼠标判定
        gameObject.layer = LayerMask.NameToLayer("Selectable");
        GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        InUse = false;
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="mousePosition"></param>
    private void MousePosMove(Vector3 mousePosition)
    {
        // 鼠标zDepth = 摄像机与所需平面距离
        mousePosition.z = 9f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPos.y = 1f;
        cardPosition = worldPos;
    }
    #endregion
}
