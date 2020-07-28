using Sta.AutomationMacro.Macro;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public class MacroPool : IMacroPool
    {
        private IDictionary<Type, IMacro> Pool { get; } = new Dictionary<Type, IMacro>();

        public MacroPool(ISwitchClock clock, ISwitchController controller, ICancellationRequest cancelRequest)
        {
            Add<DrawLotoIdMacro>(clock, controller, cancelRequest);
            Add<BattleMaxRaidMacro>(clock, controller, cancelRequest);
        }

        private void Add<T>(ISwitchClock clock, ISwitchController controller, ICancellationRequest cancelRequest) where T : AbstractMacro, new()
        {
            Pool.Add(typeof(T), new T() { Clock = clock, Controller = controller, CancellationRequest = cancelRequest });
        }

        public IMacro Get<T>() where T : IMacro
        {
            return Pool[typeof(T)];
        }
    }
}
