using System.Reflection;

using Terraria;
using Terraria.ModLoader;

namespace CBT.Common
{
    public static class FargoBossHelper
    {
        private static Mod Fargo;

        public static bool IsFargoLoaded
        {
            get
            {
                if (Fargo != null)
                    return true;

                return ModLoader.TryGetMod("FargowiltasSouls", out Fargo);
            }
        }

        private static bool AnyBoss(string internalName)
        {
            if (!IsFargoLoaded)
                return false;

            if (!Fargo.TryFind<ModNPC>(internalName, out ModNPC boss))
                return false;

            return NPC.AnyNPCs(boss.Type);
        }

        public static bool IsMasochistMode
        {
            get
            {
                if (!IsFargoLoaded)
                    return false;

                if (!Fargo.TryFind<ModNPC>("MutantBoss", out ModNPC mutant))
                    return false;

                System.Type fargoAssemblyType = mutant.GetType();

                System.Reflection.Assembly fargoAssembly =
                    fargoAssemblyType.Assembly;

                System.Type worldSavingSystemType =
                    fargoAssembly.GetType(
                        "FargowiltasSouls.Core.Systems.WorldSavingSystem"
                    );

                if (worldSavingSystemType == null)
                    return false;

                PropertyInfo property =
                    worldSavingSystemType.GetProperty(
                        "MasochistModeReal",
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Static
                    );

                if (property == null)
                    return false;

                return property.GetValue(null) is bool value && value;
            }
        }

        public static bool TrojanSquirrel =>
            AnyBoss("TrojanSquirrel");

        public static bool CursedCoffin =>
            AnyBoss("CursedCoffin");

        public static bool Deviantt =>
            AnyBoss("DeviBoss");

        public static bool BanishedBaron =>
            AnyBoss("BanishedBaron");

        public static bool Lifelight =>
            AnyBoss("LifeChallenger");

        public static bool Eridanus =>
            AnyBoss("CosmosChampion");

        public static bool Abominationn =>
            AnyBoss("AbomBoss");

        public static bool Mutant =>
            AnyBoss("MutantBoss");
        public static bool ChampionOfTimber =>
    AnyBoss("TimberChampion");

        public static bool ChampionOfTerra =>
            AnyBoss("TerraChampion");

        public static bool ChampionOfEarth =>
            AnyBoss("EarthChampion");

        public static bool ChampionOfNature =>
            AnyBoss("NatureChampion");

        public static bool ChampionOfLife =>
            AnyBoss("LifeChampion");

        public static bool ChampionOfDeath =>
            AnyBoss("ShadowChampion");

        public static bool ChampionOfSpirit =>
            AnyBoss("SpiritChampion");

        public static bool ChampionOfWill =>
            AnyBoss("WillChampion");
    }
}