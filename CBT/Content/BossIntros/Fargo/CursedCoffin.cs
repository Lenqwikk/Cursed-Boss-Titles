using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Fargo
{
    public class CursedCoffinIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(80, 35, 110),
                    new Color(190, 80, 190),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return FargoBossHelper.CursedCoffin;
        }
    }
}