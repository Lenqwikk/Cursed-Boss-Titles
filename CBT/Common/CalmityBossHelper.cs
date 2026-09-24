using Terraria;
using Terraria.ModLoader;

namespace CBT.Common
{
    public static class CalamityBossHelper
    {
        private static Mod Calamity;

        public static bool IsCalamityLoaded
        {
            get
            {
                if (Calamity != null)
                    return true;

                return ModLoader.TryGetMod("CalamityMod", out Calamity);
            }
        }

        private static bool AnyBoss(string internalName)
        {
            if (!IsCalamityLoaded)
                return false;

            if (!Calamity.TryFind<ModNPC>(
                internalName,
                out ModNPC boss
            ))
            {
                return false;
            }

            return NPC.AnyNPCs(boss.Type);
        }

        public static bool DesertScourge =>
            AnyBoss("DesertScourgeHead");

        public static bool Crabulon =>
            AnyBoss("Crabulon");

        public static bool HiveMind =>
            AnyBoss("HiveMind");

        public static bool Perforators =>
            AnyBoss("PerforatorHive");

        public static bool SlimeGod =>
            AnyBoss("SlimeGodCore");

        public static bool Cryogen =>
            AnyBoss("Cryogen");

        public static bool AquaticScourge =>
            AnyBoss("AquaticScourgeHead");

        public static bool BrimstoneElemental =>
            AnyBoss("BrimstoneElemental");

        public static bool CalamitasClone =>
            AnyBoss("CalamitasClone");

        public static bool AnahitaAndLeviathan =>
            AnyBoss("Anahita");

        public static bool AstrumAureus =>
            AnyBoss("AstrumAureus");

        public static bool PlaguebringerGoliath =>
            AnyBoss("PlaguebringerGoliath");

        public static bool Ravager =>
            AnyBoss("RavagerBody");

        public static bool AstrumDeus =>
            AnyBoss("AstrumDeusHead");

        public static bool ProfanedGuardians =>
            AnyBoss("ProfanedGuardianCommander");

        public static bool Dragonfolly =>
            AnyBoss("Dragonfolly");

        public static bool Providence =>
            AnyBoss("Providence");

        public static bool CeaselessVoid =>
            AnyBoss("CeaselessVoid");

        public static bool StormWeaver =>
            AnyBoss("StormWeaverHead");

        public static bool Signus =>
            AnyBoss("Signus");

        public static bool Polterghast =>
            AnyBoss("Polterghast");

        public static bool OldDuke =>
            AnyBoss("OldDuke");

        public static bool DevourerOfGods =>
            AnyBoss("DevourerofGodsHead");

        public static bool Yharon =>
            AnyBoss("Yharon");

        public static bool SupremeCalamitas =>
            AnyBoss("SupremeCalamitas");

        public static bool GiantClam =>
            AnyBoss("GiantClam");
        public static bool ExoMechs =>
            AnyBoss("AresBody") ||
            AnyBoss("Apollo") ||
            AnyBoss("Artemis") ||
            AnyBoss("ThanatosHead");
    }
}