using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Fargo.Champions.Champions
{
    public class ForestIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.7f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(55, 120, 45),
                    new Color(150, 220, 70),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return FargoBossHelper.ChampionOfTimber;
        }
    }
}