namespace TickTick
{
    namespace Action
    {
        public delegate void Trigger();
        public enum TriggerType
        {
            Advance = 0,
            AfterPreparing,
            BeforeProcessing,
            AfterProcessing,
            AfterRecovering,
            Damage,
            Charge,
            Summon,
            Ready,
            Death,
            Undefined
        }

        public class TriggerGroup
        {

            private readonly Trigger[] triggers;

            public TriggerGroup()
            {
                triggers = new Trigger[(int)TriggerType.Undefined];
            }

            public void TriggerRegister(TriggerType type, Trigger method)
            {
            	int index = (int)type;
            	triggers[index] += method;
            }

           	public void TriggerClear(TriggerType type)
           	{
           		int index = (int)type;
           		triggers[index] = null;
           	}
        }
    }
}
