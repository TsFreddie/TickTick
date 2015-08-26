using UnityEngine;
using System.Collections;

/// <summary>
/// 刻石组件
/// </summary>
public class CarvedObject : MonoBehaviour
{
    public enum Stage
    {
        Preparing,
        Ready,
        Processing,
        Recovering,
    }
    /// <summary>生命值,兼职存放召唤刻石最大能量</summary>
    private int health; 
    private int power;
    private int agility;
    /// <summary>损耗值,兼职存放召唤刻石当前能量</summary>
    private int loss;
    private int booster;
    private CardData.ElementType elementType;
    private CardData.CardType cardType;
    private Stage stage;
    
    private float dayScale;
    /// <summary>读条起始时间</summary>
    private System.DateTime startTime;
    private bool dead;
    private bool initialized;
    
    private bool activated;

    private CarvedDisplay ui;
    private Vector3 carvedPosition;

    #region 初始化
    void Awake()
    {
        // 使其可选
        gameObject.layer = LayerMask.NameToLayer("Selectable");
        
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
        activated = false;
        if (!initialized)
        {
            Debug.LogError("Uninitialzed CarvedObject, Killing it.");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Animation();
        Tick();
    }
    
    /// <summary>移动动画</summary>
    public void Animation()
    {
        if (carvedPosition != transform.position || activated)
        {
            Vector3 targetPosition = carvedPosition;
            if (activated)
                targetPosition.y += 0.5f;
            Vector3 displacement = targetPosition - transform.position;
            transform.position += displacement.normalized * (displacement.magnitude * 10f * Time.deltaTime);
            if (displacement.magnitude < 0.01f)
            {
                transform.position = targetPosition;
            }
        }   
    }
    
    /// <summary>卡牌读条逻辑</summary>
    public void Tick()
    {
        System.DateTime timeNow = System.DateTime.Now;
        float progressTime = (float)((timeNow - startTime).TotalSeconds);
        float totalProgressTime = dayScale * (100 - agility) / 100;
        if (progressTime >= totalProgressTime)
        {
            if (stage == Stage.Preparing || stage == Stage.Recovering)
            {
                stage = Stage.Ready;
                startTime = timeNow;
            }
           if (stage == Stage.Processing)
            {
                stage = Stage.Recovering;
                startTime = timeNow;
            }
        }
        ui.SetProgress(stage, progressTime / totalProgressTime);

    }
    
    /// <summary>
    /// 初始化卡牌参数
    /// </summary>
    /// <param name="data">卡牌数据</param>
    public void Init(CardData data)
    {
        dayScale = GameManager.instance.DayScale;
        initialized = true;
        elementType = data.Element;
        startTime = System.DateTime.Now;
        stage = Stage.Preparing;
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
        stage = Stage.Processing;
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
    /// 缓慢移动到
    /// </summary>
    public void MoveTo(Vector3 position)
    {
        carvedPosition = position;
    }
    
    /// <summary>
    /// 跳至指定位置
    /// </summary>
    public void SetPosition(Vector3 position)
    {
        carvedPosition = position;
        transform.position = position;
    }
    
    /// <summary>
    /// 捡起
    /// </summary>
    public void Activate()
    {
        activated = true;
        GameController.instance.MouseUp += Deactivate;
    }
    
    /// <summary>
    /// 放下
    /// </summary>
    public void Deactivate()
    {
        activated = false;
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
