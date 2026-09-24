using System.Collections.Generic;
using CBT.Common;
using Terraria;

namespace CBT
{
    public static class IntroScreenManager
    {
        internal static List<BossIntroScreen> IntroScreens = new();

        public static void UpdateScreens()
        {
            foreach (BossIntroScreen introScreen in IntroScreens)
                introScreen.Update();
        }

        public static void Draw()
        {
            UpdateScreens();

            foreach (BossIntroScreen introScreen in IntroScreens)
            {
                if (introScreen.ShouldBeActive())
                {
                    if (introScreen.TotalTimer < introScreen.TotalAnimationTime)
                    {
                        introScreen.Draw(Main.spriteBatch);
                        break;
                    }
                }
            }
        }
    }
}