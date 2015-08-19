/// <summary>
/// 召唤卡牌类
/// </summary>
public class SummonCardData : CardData
{
    public int Power { get; private set; }
    public int Energy { get; private set; }
    public int Agility { get; private set; }

    /// <summary>
    /// 构造召唤卡牌数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cost"></param>
    /// <param name="booster"></param>
    /// <param name="element"></param>
    /// <param name="power">召唤后的攻击力</param>
    /// <param name="energy">所需能量,召唤后的血量</param>
    /// <param name="agility">召唤后的敏捷</param>
    public SummonCardData(int id, int cost, int booster, ElementType element, int power, int energy, int agility)
        : base(id, cost, booster, element)
    {
        Power = power;
        Energy = energy;
        Agility = agility;
    }
}
