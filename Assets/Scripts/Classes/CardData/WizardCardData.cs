/// <summary>
/// 巫师卡牌类
/// </summary>
public class WizardCardData : CardData 
{
    public int Power {get; private set; }

    /// <summary>
    /// 构造巫师卡牌数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cost"></param>
    /// <param name="booster"></param>
    /// <param name="element"></param>
    /// <param name="power">攻击力</param>
    public WizardCardData(int id, int cost, int booster, ElementType element, int power)
        : base(id, cost, booster, element)
    {
        Power = power;
    }
}
