/// <summary>
/// 法术卡牌类
/// </summary>
public class MagicCardData : CardData
{
    public int Agility { get; private set; }

    /// <summary>
    /// 构造巫师卡牌类
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cost"></param>
    /// <param name="booster"></param>
    /// <param name="element"></param>
    /// <param name="agility">敏捷,-1为瞬发,100为次帧瞬发</param>
    public MagicCardData(int id, int cost, int booster, ElementType element, int agility)
        : base(id, cost, booster, element)
    {
        Agility = agility;
    }
}
