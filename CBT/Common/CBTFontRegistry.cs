using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CBT.Common
{
    public class CBTFontRegistry : ModSystem
    {
        private static DynamicSpriteFont BossIntroScreensFontEnglish;
        private static DynamicSpriteFont BossIntroScreensFontRussian;

        public static DynamicSpriteFont BossIntroScreensFont
        {
            get
            {
                if (Language.ActiveCulture.Name == "ru-RU")
                    return BossIntroScreensFontRussian;

                return BossIntroScreensFontEnglish;
            }
        }

        public override void Load()
        {
            BossIntroScreensFontEnglish = ModContent.Request<DynamicSpriteFont>(
                "CBT/Assets/Fonts/BossIntroScreensFontRussian",
                AssetRequestMode.ImmediateLoad
            ).Value;

            BossIntroScreensFontRussian = ModContent.Request<DynamicSpriteFont>(
                "CBT/Assets/Fonts/BossIntroScreensFontRussian",
                AssetRequestMode.ImmediateLoad
            ).Value;
        }

        public override void Unload()
        {
            BossIntroScreensFontEnglish = null;
            BossIntroScreensFontRussian = null;
        }
    }
}