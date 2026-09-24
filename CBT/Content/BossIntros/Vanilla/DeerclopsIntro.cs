using CBT.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CBT.Content.BossIntros.Vanilla
{
    public class DeerclopsIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override Color TextColor =>
            new Color(150, 190, 220);

        public override bool ShouldBeActive()
        {
            return NPC.AnyNPCs(NPCID.Deerclops);
        }
    }
}