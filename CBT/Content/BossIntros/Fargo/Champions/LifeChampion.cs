using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Fargo.Champions.Champions
{
    public class LifeIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.7f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(220, 180, 45),
                    new Color(255, 245, 110),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return FargoBossHelper.ChampionOfLife;
        }
    }
}