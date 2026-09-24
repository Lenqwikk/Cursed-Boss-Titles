using CBT.Common;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CBT.Content.BossIntros.Fargo
{
    public class MutantIntro : BossIntroScreen
    {
        public override float TextScale => 0.9f;

        public override bool TextShouldBeCentered => true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(190, 70, 255),
                    new Color(100, 230, 255),
                    completionRatio
                )
            );

        private static LocalizedText MasochistText;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            MasochistText = Language.GetOrRegister(
                "Mods.CBT.BossIntros.MutantIntro.MasochistText",
                () =>
                    Language.ActiveCulture.Name == "ru-RU"
                        ? "Тебе пизда\nМутант"
                        : "You're fucked\nMutant"
            );
        }

        public override LocalizedText GetIntroText()
        {
            if (FargoBossHelper.IsMasochistMode)
                return MasochistText;

            return TextToDisplay;
        }

        public override bool ShouldBeActive() =>
            FargoBossHelper.Mutant;
    }
}