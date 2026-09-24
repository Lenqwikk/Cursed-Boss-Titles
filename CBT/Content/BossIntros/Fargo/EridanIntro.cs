using CBT.Common;
using Microsoft.Xna.Framework;

namespace CBT.Content.BossIntros.Fargo
{
    public class EridanIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.5f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(80, 255, 130),
                    new Color(230, 255, 100),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return FargoBossHelper.Eridanus;
        }
    }
}