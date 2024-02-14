using MCM.Abstractions.FluentBuilder;
using MCM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrexMaxBets.MenuVariables
{
    internal class BetSettings : IDisposable
    {
        private static BetSettings? _instance;
        public static BetSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BetSettings();
                }
                return _instance;
            }
        }
        public int MaxBet { get; set; } = 250;
        public void Settings()
        {
            var builder = BaseSettingsBuilder.Create(SubModule.ModName, "Orex Max Bets")!
                .SetFormat("xml")
                .SetFolderName(SubModule.ModuleFolderName)
                .SetSubFolder(String.Empty)
                .CreateGroup("Tournament Bet Amount", groupBuilder => groupBuilder
                .AddInteger("maxBest", "Max Bet", 250, 10000, new ProxyRef<int>(() => MaxBet, o => MaxBet = o), integerBuilder => integerBuilder
                    .SetHintText("This is the maximum bet value for tournaments")));

            var globalSettings = builder.BuildAsGlobal();
            globalSettings.Register();
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
