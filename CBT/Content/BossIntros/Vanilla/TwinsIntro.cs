using CBT.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CBT.Content.BossIntros.Vanilla
{
    public class TwinsIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(100, 255, 100),
                    new Color(255, 70, 70),
                    completionRatio
                )
            );
        public override bool ShouldBeActive()
        {
            return NPC.AnyNPCs(NPCID.Retinazer);
        }
    }
}