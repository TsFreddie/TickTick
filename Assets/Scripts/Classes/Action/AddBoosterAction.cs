namespace TickTick.Action
{
    public struct AddBoosterAction : ITriggerAction
    {
    	private readonly Rule rule;
    	private readonly int booster;

        /// <summary>
        /// 得分
        /// </summary>
        /// <param name="rule">当前游戏规则类</param>
        /// <param name="booster">增量</param>
    	public AddBoosterAction(Rule rule, int booster)
    	{
    		this.rule = rule;
    		this.booster = booster;
    	}

		public Trigger GetTrigger()
		{
			return DoAction;
		}

		private void DoAction()
		{
			if (rule.GetType() == typeof(DuelRule))
			{
				((DuelRule)rule).AddBooster(booster);
			}
		}
    }
}