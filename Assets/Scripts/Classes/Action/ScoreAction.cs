namespace TickTick
{
    namespace Action
    {
        public struct ScoreAction
        {

            private readonly Rule rule;
            private readonly int site;
            private readonly int score;

            /// <summary>
            /// 得分
            /// </summary>
            /// <param name="rule">当前游戏规则类</param>
            /// <param name="site">场地ID</param>
            /// <param name="score">得分</param>
            public ScoreAction(Rule rule, int site, int score)
            {
                this.rule = rule;
                this.site = site;
                this.score = score;
            }

            public Trigger GetTrigger()
            {
                return DoAction;
            }

            private void DoAction()
            {
                if (rule.GetType() == typeof(DuelRule))
                {
                    ((DuelRule)rule).Score(site, score);
                }
            }
        }
    }
}