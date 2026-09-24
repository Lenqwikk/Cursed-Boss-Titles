using CBT.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CBT.Content.BossIntros.Vanilla
{
    public class SkeletronPrime : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override Color TextColor =>
            new Color(210, 215, 225);
        public override bool ShouldBeActive()
        {
            return NPC.AnyNPCs(NPCID.SkeletronPrime);
        }
    }
}