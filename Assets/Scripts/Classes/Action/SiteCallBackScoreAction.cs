namespace TickTick.Action
{
	public struct SiteCallBackScoreAction : ITriggerAction
	{
		private readonly Rule rule;
		private readonly SiteSlotsArranger site;

		/// <summary>
		/// 场地回调触发行为
		/// </summary>
		/// <param name="rule">规则</param>
		/// <param name="site">场地组件</param>
		public SiteCallBackScoreAction(Rule rule, SiteSlotsArranger site)
		{
			this.rule = rule;
			this.site = site;
		}

		public Trigger GetTrigger()
		{
			return DoAction;
		}

		private void DoAction()
		{
			site.CallBackScore(rule);
		}
	}
}