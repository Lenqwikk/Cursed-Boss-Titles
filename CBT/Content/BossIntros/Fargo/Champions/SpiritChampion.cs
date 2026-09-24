using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Fargo.Champions.Champions
{
    public class SpiritIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.7f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(120, 50, 180),
                    new Color(230, 120, 255),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return FargoBossHelper.ChampionOfSpirit;
        }
    }
}