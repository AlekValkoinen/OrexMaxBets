using JetBrains.Annotations;
using SandBox.Tournaments;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using HarmonyLib;
using OrexMaxBets.MenuVariables;

namespace OrexMaxBets
{
    public class SubModule : MBSubModuleBase
    {
        //MCM things
        public static readonly string ModuleFolderName = "OrexMaxBets";
        public static readonly string ModName = "Orex Max Bets";
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            Harmony harm = new Harmony("OrexMaxBets");
            harm.PatchAll();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }
        
        
        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);
            
        }
        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);
        }
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            BetSettings.Instance.Settings();
        }
    }
    /*
     * Upon inspection of the decompiled source Tournament behaviors are not handled like normal game behaviors, so overriding the class doesn't work, Leaving only the option of patching the method with Harmony, or doing the real bad and modifying the actual TaleWorlds Binaries
     * I have no intention on modifying the Binaries to make this work, so patching with Harmony it is.
     * */

    [HarmonyPatch(typeof(TournamentBehavior), "GetMaximumBet")]
    class patchBets
    {
        int MaxBet = 10000;
        public static bool Prefix(ref int __result)
        {
            int num = BetSettings.Instance.MaxBet; 
            if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
            {
                num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
            }
            __result = num;
            return false;
        }
    }

    //public class ModdedTournamentBehavior : TournamentBehavior
    //{
    //    public int MaxBetSetting = 10000;
    //    public ModdedTournamentBehavior(TournamentGame tournamentGame, Settlement settlement, ITournamentGameBehavior gameBehavior, bool isPlayerParticipating) : base(tournamentGame, settlement, gameBehavior, isPlayerParticipating)
    //    {

    //    }
    //    public new int GetMaximumBet()
    //    {
    //        int maxBet = MaxBetSetting;
    //        if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
    //        {
    //            maxBet *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
    //        }
    //        return maxBet;
    //    }
    //}
}