/// <summary>
/// 远程卡牌类
/// </summary>
public class RangeCardData : CardData
{
    public int Power { get; private set; }
    public int Health { get; private set; }
    public int Loss { get; private set; }
    public int Agility { get; private set; }
    
    /// <summary>
    /// 构造远程卡牌数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cost"></param>
    /// <param name="booster"></param>
    /// <param name="element"></param>
    /// <param name="power">力量</param>
    /// <param name="health">生命</param>
    /// <param name="agility">敏捷</param>
    /// <param name="loss">损耗,攻击时远程士兵对自己的伤害值</param>
    public RangeCardData(int id, int cost, int booster, ElementType element, int power, int health, int agility, int loss)
        : base(id, cost, booster, element)
    {
        Power = power;
        Health = health;
        Loss = loss;
        Agility = agility;
    }
}
