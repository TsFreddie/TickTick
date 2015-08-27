namespace TickTick
{
    /// <summary>
    /// 卡牌基类，定义卡牌的基本参数
    /// </summary>
    public class CardData
    {
        public enum ElementType
        {
            Fire,
            // 烈焰
            Life,
            // 生命
            Earth,
            // 大地
            Water,
            // 雨露
            Sorcery
            // 巫术
        }

        public enum CardType //勿删,未在CardData类中使用,为避免歧义而放置在这里,通过CardData.CardType可在任意外部类调用
        {
            Melee,
            // 近战
            Range,
            // 远程
            Wizard,
            // 巫师
            Magic,
            // 魔法
            Summon
            // 召唤
        }

        public int ID { get; private set; }
        public int Cost { get; private set; }
        public int Booster { get; private set; }
        public ElementType Element { get; private set; }


        /// <summary>
        /// 继承地创建卡牌数据,本类不能在继承类外声明。
        /// </summary>
        /// <param name="id">卡牌ID,值为唯一</param>
        /// <param name="cost">花费</param>
        /// <param name="booster">奖励</param>
        /// <param name="element">元素</param>
        protected CardData(int id, int cost, int booster, ElementType element)
        {
            ID = id;
            Cost = cost;
            Booster = booster;
            Element = element;
        }

    }
    
}
