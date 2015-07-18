/// <summary>
/// 召唤卡牌类
/// </summary>
public class SummonCardData : CardData
{
    public int Energy { get; private set; }
    public int Agility { get; private set; }

    /// <summary>
    /// 构造召唤卡牌数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cost"></param>
    /// <param name="booster"></param>
    /// <param name="element"></param>
    /// <param name="energy">所需能量</param>
    /// <param name="agility">敏捷</param>
    protected SummonCardData(int id, int cost, int booster, ElementType element, int energy, int agility)
        : base(id, cost, booster, element)
    {
        Energy = energy;
        Agility = agility;
    }
}
