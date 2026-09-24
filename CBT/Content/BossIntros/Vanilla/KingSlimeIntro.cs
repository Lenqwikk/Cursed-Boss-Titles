using CBT.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CBT.Content.BossIntros.Vanilla
{
    public class KingSlimeIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override Color TextColor =>
            new Color(80, 170, 255);

        public override bool ShouldBeActive()
        {
            return NPC.AnyNPCs(NPCID.KingSlime);
        }
    }
}