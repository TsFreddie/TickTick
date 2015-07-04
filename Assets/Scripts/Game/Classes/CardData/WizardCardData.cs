/// <summary>
/// 巫师卡牌类
/// </summary>
public class WizardCardData : CardData 
{
    public int Health {get; private set; }

    /// <summary>
    /// 构造巫师卡牌数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cost"></param>
    /// <param name="booster"></param>
    /// <param name="element"></param>
    /// <param name="health">生命</param>
    public WizardCardData(int id, int cost, int booster, ElementType element, int health)
        : base(id, cost, booster, element)
    {
        Health = health;
    }
}
