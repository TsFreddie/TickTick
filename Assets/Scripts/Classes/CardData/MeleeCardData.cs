namespace TickTick
{
    /// <summary>
    /// 近战卡牌类
    /// </summary>
    public class MeleeCardData : CardData
    {
        public int Power { get; private set; }
        public int Health { get; private set; }
        public int Agility { get; private set; }

        /// <summary>
        /// 构造近战卡牌数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cost"></param>
        /// <param name="booster"></param>
        /// <param name="element"></param>
        /// <param name="power">力量</param>
        /// <param name="health">生命</param>
        /// <param name="agility">敏捷</param>
        public MeleeCardData(int id, int cost, int booster, ElementType element, int power, int health, int agility)
            : base(id, cost, booster, element)
        {
            Power = power;
            Health = health;
            Agility = agility;
        }
    }
    
}
