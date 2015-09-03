using TickTick.Action;
namespace TickTick
{
    /// <summary>
    /// 规则基类，为不同模式提供通用实现
    /// 新规则类即代表一盘游戏
    /// </summary>
    public class Rule
    {
        public float DayScale { get; private set; }
        protected Hand PlayerHand { get { return playerHand; } }
        protected Deck PlayerDeck { get { return playerDeck; } }

        private ulong gameID;
        private ulong hostID;
        private ulong guestID;

        private Hand playerHand;
        private Deck playerDeck;

        private bool running;

        protected Rule(ulong gameID, ulong hostID, ulong guestID, float dayScale, Hand.HandCallBack handCallback, Deck playerDeck)
        {
            this.gameID = gameID;
            this.hostID = hostID;
            this.guestID = guestID;
            if (playerDeck == null)
                this.playerDeck = new Deck();
            else
                this.playerDeck = playerDeck;
            this.playerDeck.Shuffle();
            DayScale = dayScale;
            playerHand = new Hand();
            playerHand.RegisterHandCallBack(handCallback);
        }
	
        /// <summary>操作: Card - Standby, 拖入待命区</summary>
        public virtual void DoAction(CardObject card, StandbySlotsArranger standby)
        {

        }
        /// <summary>操作: Card - Magic, 拖入魔法槽</summary>
        public virtual void DoAction(CardObject card, MagicSlotObject magic)
        {

        }
        /// <summary>操作: Card - Carved, 法师攻击或充能</summary>
        public virtual void DoAction(CardObject card, CarvedObject carved)
        {

        }
        /// <summary>操作: Carved - Carved, 近战或远程攻击</summary>
        public virtual void DoAction(CarvedObject attacker, CarvedObject victim)
        {

        }
        /// <summary>操作: Carved - Site, 近战上前线或应战</summary>
        public virtual void DoAction(CarvedObject carved, SiteSlotsArranger site)
        {


        }
	
        public virtual void Start()
        {
            running = true;
        }
	
        public virtual void Tick()
        {
        }

        public bool IsRunning()
        {
            return running;
        }
        /// <summary>
        /// 双方交换的网络消息日志，用于录制和保存demo
        /// </summary>
        protected void Record(byte[] msg)
        {
		
        }
    }
}