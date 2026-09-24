using CBT.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CBT.Content.BossIntros.Vanilla
{
    public class EyeOfCthulhuIntro : BossIntroScreen
    {
        public override float TextScale =>
            0.9f;

        public override bool TextShouldBeCentered =>
            true;

        public override Color TextColor =>
            new Color(255, 80, 80);

        public override bool ShouldBeActive()
        {
            return NPC.AnyNPCs(NPCID.EyeofCthulhu);
        }
    }
}