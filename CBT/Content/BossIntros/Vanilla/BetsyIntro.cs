using CBT.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CBT.Content.BossIntros.Vanilla
{
    public class BetsyIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(155, 39, 68),
                    new Color(255, 145, 57),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return NPC.AnyNPCs(NPCID.DD2Betsy);
        }
    }
}