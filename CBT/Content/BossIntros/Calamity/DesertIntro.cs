using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Calamity
{
    public class Desert : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(210, 175, 100),
                    new Color(110, 65, 35),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return CalamityBossHelper.DesertScourge;
        }
    }
}