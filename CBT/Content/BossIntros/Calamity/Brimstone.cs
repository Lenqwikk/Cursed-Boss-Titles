using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Calamity
{
    public class Brimstone : BossIntroScreen
    {
        public override float TextScale =>
            0.7f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(220, 35, 55),
                    new Color(255, 120, 25),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return CalamityBossHelper.BrimstoneElemental;
        }
    }
}