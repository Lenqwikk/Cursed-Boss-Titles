using CBT.Common;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CBT.Content.BossIntros.Vanilla
{
    public class MoonLordIntro : BossIntroScreen
    {
        public override float TextScale => 0.9f;

        public override bool TextShouldBeCentered => true;

        public override Color TextColor =>
            new Color(100, 200, 255);

        private static LocalizedText FargoText;

        private static LocalizedText CalamityText;

        private static LocalizedText BothModsText;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            FargoText = Language.GetOrRegister(
                "Mods.CBT.BossIntros.MoonLordIntro.FargoText",
                () =>
                    Language.ActiveCulture.Name == "ru-RU"
                        ? "-\nЛунный Лорд"
                        : "-\nMoon Lord"
            );

            CalamityText = Language.GetOrRegister(
                "Mods.CBT.BossIntros.MoonLordIntro.CalamityText",
                () =>
                    Language.ActiveCulture.Name == "ru-RU"
                        ? "-\nЛунный Лорд"
                        : "-\nMoon Lord"
            );

            BothModsText = Language.GetOrRegister(
                "Mods.CBT.BossIntros.MoonLordIntro.BothModsText",
                () =>
                    Language.ActiveCulture.Name == "ru-RU"
                        ? "Ты явно ебнутый\nЛунный лорд"
                        : "You're clearly fucked in the head\nMoon Lord"
            );
        }

        public override LocalizedText GetIntroText()
        {
            bool fargoLoaded =
                ModLoader.HasMod("FargowiltasSouls");

            bool calamityLoaded =
                ModLoader.HasMod("CalamityMod");

            if (fargoLoaded && calamityLoaded)
                return BothModsText;

            if (fargoLoaded)
                return FargoText;

            if (calamityLoaded)
                return CalamityText;

            return TextToDisplay;
        }

        public override bool ShouldBeActive(){
            return NPC.AnyNPCs(NPCID.MoonLordCore);
        }
    }
}