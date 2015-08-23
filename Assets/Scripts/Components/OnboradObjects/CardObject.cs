using UnityEngine;
using System.Collections;

/// <summary>
/// 卡牌组件,卡牌组件本身不储存卡牌内容数据,只存储卡牌Data类
/// </summary>
public class CardObject : MonoBehaviour
{
    public CardData CardData
    {
        get { return cardData; }
    }

    private CardData cardData;
    private bool initialized;

    private CardDisplay ui;

    #region 初始化
    void Awake()
    {
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
    }
    /// <summary>
    /// 卡牌组件初始化
    /// </summary>
    /// <param name="cardData">卡牌数据</param>
    public void Init(CardData data)
    {
        Debug.Log("init");
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
        ui.SetType(CardData.CardType.Melee);
        ui.SetCost(data.Cost);
        ui.SetBooster(data.Booster);
        ui.SetHealth(data.Health);
        ui.SetPower(data.Power);
        ui.SetAgility(data.Agility);
        Debug.Log("init melee");
    }

    /// <summary>
    /// 远程卡牌初始化
    /// </summary>
    /// <param name="data">远程卡牌数据</param>
    private void RangeInit(RangeCardData data)
    {
        ui.SetType(CardData.CardType.Range);
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
        ui.SetType(CardData.CardType.Wizard);
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
        ui.SetType(CardData.CardType.Magic);
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
        ui.SetType(CardData.CardType.Summon);
        ui.SetCost(data.Cost);
        ui.SetBooster(data.Booster);
        ui.SetHealth(0);
        ui.SetPower(data.Energy);
        ui.SetAgility(data.Agility);
    }

    #endregion

    #region 操作
    /// <summary>
    /// 捡起操作
    /// </summary>
    public void Pickup()
    {
        // 挂载事件
        GameController.instance.MouseMove += MouseMove;
        GameController.instance.MouseUp += MouseUp;
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    private void MouseUp()
    {
        GameController.instance.MouseMove -= MouseMove;
        GameController.instance.MouseUp -= MouseUp;
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="mousePosition"></param>
    private void MouseMove(Vector3 mousePosition)
    {
        mousePosition.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPos.y = 1f;
        gameObject.transform.position = worldPos;
    }
    #endregion
}
