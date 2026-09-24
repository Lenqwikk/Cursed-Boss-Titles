using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Calamity
{
    public class Aquatic : BossIntroScreen
    {
        public override float TextScale =>
            0.8f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(40, 220, 200),
                    new Color(35, 70, 180),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return CalamityBossHelper.AquaticScourge;
        }
    }
}