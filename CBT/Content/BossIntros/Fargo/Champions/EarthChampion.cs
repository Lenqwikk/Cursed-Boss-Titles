using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Fargo.Champions.Champions
{
    public class EarthIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.7f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(80, 100, 190),
                    new Color(170, 210, 255),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return FargoBossHelper.ChampionOfTerra;
        }
    }
}