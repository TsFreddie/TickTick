using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 刻石组件
/// </summary>
public class CarvedObject : MonoBehaviour
{
    private int health; //兼职存放当前能量槽属性
    private int power;  //兼职存放最大能量槽属性
    private int agility;
    private int loss;
    private int booster;
    private CardData.ElementType elementType;
    private CardData.CardType cardType;
    private SlotObject.SlotType slotType;
    private int slotId;
    private bool dead;
    private bool initialized;

    private CarvedDisplay ui;

    #region 初始化
    void Awake()
    {
        ui = GetComponent<CarvedDisplay>();
        if (ui == null)
        {
            Debug.LogError("CarvedUIController not found");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        dead = false;
        if (!initialized)
        {
            Debug.LogError("Uninitialzed CarvedObject, Killing it.");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初始化卡牌参数
    /// </summary>
    /// <param name="data">卡牌数据</param>
    public void Init(CardData data)
    {
        initialized = true;
        elementType = data.Element;
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
        cardType = CardData.CardType.Melee;
        health = data.Health;
        power = data.Power;
        agility = data.Agility;
        loss = -1;
        UpdateUI();
    }

    /// <summary>
    /// 远程卡牌初始化
    /// </summary>
    /// <param name="data">远程卡牌数据</param>
    private void RangeInit(RangeCardData data)
    {
        cardType = CardData.CardType.Range;
        health = data.Health;
        power = data.Power;
        agility = data.Agility;
        loss = data.Loss;
        UpdateUI();
    }

    /// <summary>
    /// 巫师卡牌初始化
    /// </summary>
    /// <param name="data">巫师卡牌数据</param>
    private void WizardInit(WizardCardData data)
    {
        cardType = CardData.CardType.Wizard;
        health = -1;
        power = data.Power;
        agility = -1;
        loss = -1;
        UpdateUI();
    }

    /// <summary>
    /// 魔法卡牌初始化
    /// </summary>
    /// <param name="data">魔法卡牌数据</param>
    private void MagicInit(MagicCardData data)
    {
        cardType = CardData.CardType.Magic;
        health = -1;
        power = -1;
        agility = data.Agility;
        loss = -1;
        UpdateUI();
    }

    /// <summary>
    /// 召唤卡牌初始化,health存放能量,power存放能量槽上限
    /// </summary>
    /// <param name="data">召唤卡牌数据</param>
    private void SummonInit(SummonCardData data)
    {
        cardType = CardData.CardType.Summon;
        health = 0;
        power = data.Energy;
        agility = data.Agility;
        loss = -1;
        UpdateUI();
    }

    #endregion

    #region 操作
    /// <summary>
    /// 更新UI
    /// </summary>
    public void UpdateUI()
    {
        ui.SetHealth(health);
        ui.SetPower(power);
        ui.SetLoss(loss);
        ui.SetAgility(agility);
    }
    /// <summary>
    /// 扣血操作
    /// </summary>
    /// <param name="damage">伤害</param>
    public void TakeDamage(int damage)
    {
        if (!(cardType == CardData.CardType.Melee || cardType == CardData.CardType.Range))
        {
            Debug.LogError("TakeDamage performed on " + cardType.ToString());
            return;
        }

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Kill();
        }
        UpdateUI();
    }

    /// <summary>
    /// 杀死此刻石
    /// </summary>
    public void Kill()
    {
        //TODO: 交给UI组件处理动画
        dead = true;
        Destroy(gameObject);
    }

    #endregion

}
