using Sta.AutomationMacro.Macro;
using Sta.CaptureBoard;
using Sta.SwitchController;
using System;
using System.Collections.Generic;

namespace Sta.AutomationMacro
{
    public class MacroPool : IMacroPool
    {
        private IDictionary<Type, IMacro> Pool { get; } = new Dictionary<Type, IMacro>();

        public MacroPool(ISwitchClock clock, ISwitchController controller, ICancellationRequest cancelRequest, IGameCapture gameCapture)
        {
            var param = new MacroParameter() { Clock = clock, Controller = controller, CancellationRequest = cancelRequest, GameCapture = gameCapture, };
            Add<DrawLotoIdMacro>(param);
            Add<BattleMaxRaidMacro>(param);
            Add<GainWattsMacro>(param);
            //Add<SeekPokemonMacro>(param);
            Add<RapidTimeTravelMacro>(param);
            Add<ThreeDaysTravelMacro>(param);
            Add<MashAButtonMacro>(param);
        }

        private void Add<T>(MacroParameter param) where T : AbstractMacro, new()
        {
            Pool.Add(typeof(T), new T() { Param = param });
        }

        public IMacro Get<T>() where T : IMacro
        {
            return Pool[typeof(T)];
        }
    }
}
