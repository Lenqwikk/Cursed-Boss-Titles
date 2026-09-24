using CBT.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CBT.Content.BossIntros.Vanilla
{
    public class EmpressOfLightIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;
            
        public override TextColorData CalculatedTextColor =>
            new(completionRatio =>
                Color.Lerp(
                    new Color(255, 80, 200),
                    new Color(255, 240, 80),
                    completionRatio
                )
            );

        public override bool ShouldBeActive()
        {
            return NPC.AnyNPCs(NPCID.HallowBoss);
        }
    }
}