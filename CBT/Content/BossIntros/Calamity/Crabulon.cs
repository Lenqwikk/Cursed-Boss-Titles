using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Calamity
{
    public class Crabulon : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(150, 85, 45),
                    new Color(227, 75, 35),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return CalamityBossHelper.Crabulon;
        }
    }
}