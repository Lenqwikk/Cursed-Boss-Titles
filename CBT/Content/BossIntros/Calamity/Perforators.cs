using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Calamity
{
    public class Perforators : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(255, 70, 55),
                    new Color(130, 15, 20),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return CalamityBossHelper.Perforators;
        }
    }
}