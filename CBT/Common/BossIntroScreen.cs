using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CBT.Common
{
    public abstract class BossIntroScreen : ModType, ILocalizedModType
    {
        public int AnimationTimer;

        // Полный таймер интро:
        // анимация -> показ -> затухание.
        public int TotalTimer;
        public bool HasCompleted;

        public float AnimationCompletion =>
            MathHelper.Clamp(
                AnimationTimer / (float)AnimationTime,
                0f,
                1f
            );

        public bool HasPlayedMainSound;
        public string CachedText = string.Empty;

        public virtual string LocalizationCategory =>
            "BossIntros";

        protected virtual Vector2 BaseDrawPosition
        {
            get
            {
                if (TextShouldBeCentered)
                {
                    return new Vector2(
                        Main.screenWidth * 0.5f,
                        Main.screenHeight * 0.30f
                    );
                }

                return new Vector2(
                    Main.screenWidth - 400f,
                    Main.screenHeight - 300f
                );
            }
        }

        public const float MinorBossTextScale = 1.1f;
        public const float MajorBossTextScale = 1.45f;
        public const float BottomTextScale = 2.1f;

        public static float AspectRatioFactor =>
            Main.screenHeight / 1440f;

        public Vector2 DrawPosition =>
            BaseDrawPosition;

        public virtual float TextScale =>
            MinorBossTextScale;

        public virtual float EnglishTextScaleMultiplier =>
            1.4f;

        public virtual Color TextColor =>
            Color.White;

        public virtual TextColorData CalculatedTextColor =>
            new TextColorData(TextColor);

        public virtual Color TextColorTop =>
            TextColor;

        public virtual Color TextColorBottom =>
            TextColor;

        public virtual Color ScreenCoverColor =>
            Color.Black;

        // Продолжительность основной анимации.
        public virtual int AnimationTime =>
            ShouldCoverScreen ? 115 : 150;

        // Дополнительное время полностью отображённого текста.
        // 60 кадров ≈ 1 секунда при 60 FPS.
        public virtual int DisplayTime =>
            60;

        // Длительность затухания.
        // 23% от старых 150 кадров ≈ 35 кадров.
        // Это сохраняет примерно ту же скорость fade-out.
        public virtual int FadeOutTime =>
            (int)(AnimationTime * 0.23f);

        // Полная продолжительность интро.
        public int TotalAnimationTime =>
            AnimationTime +
            DisplayTime +
            FadeOutTime;

        public virtual float TextDelayInterpolant =>
            ShouldCoverScreen ? 0.4f : 0.05f;

        public virtual bool TextShouldBeCentered =>
            false;

        public virtual bool ShouldCoverScreen =>
            false;

        public virtual bool CaresAboutBossEffectCondition =>
            true;

        public virtual SoundStyle? SoundToPlayWithLetterAddition =>
            null;

        public virtual bool CanPlaySound =>
            AnimationTimer >=
            (int)(AnimationTime * (TextDelayInterpolant + 0.05f));

        public virtual LocalizedText GetIntroText()
            {
                return TextToDisplay;
            }
        public virtual LocalizedText TextToDisplay =>
            this.GetLocalization(
                nameof(TextToDisplay),
                () => PrettyPrintName()
            );

        public abstract bool ShouldBeActive();

        protected sealed override void Register()
        {
            ModTypeLookup<BossIntroScreen>.Register(this);

            if (!IntroScreenManager.IntroScreens.Contains(this))
                IntroScreenManager.IntroScreens.Add(this);
        }

        public sealed override void SetupContent()
        {
            _ = TextToDisplay;
            SetStaticDefaults();
        }

        public virtual void DoCompletionEffects()
        {
        }

        public virtual float LetterDisplayCompletionRatio(int animationTimer)
        {
            return 1f;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (
                Main.netMode == NetmodeID.Server ||
                TotalTimer <= 0 ||
                TotalTimer >= TotalAnimationTime
            )
            {
                return;
            }

            if (ShouldCoverScreen)
            {
                bool isBright =
                    ScreenCoverColor.ToVector3().Length() / 1.414f > 0.8f;

                if (isBright)
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(
                        SpriteSortMode.Deferred,
                        BlendState.Additive
                    );
                }

                float coverScaleFactor =
                    Utils.GetLerpValue(
                        0f,
                        0.5f,
                        AnimationCompletion,
                        true
                    ) *
                    Utils.GetLerpValue(
                        1f,
                        0.84f,
                        AnimationCompletion,
                        true
                    );

                Texture2D coverTexture =
                    Terraria.GameContent.TextureAssets.MagicPixel.Value;

                Rectangle rectangle =
                    new Rectangle(
                        0,
                        0,
                        Main.screenWidth,
                        Main.screenHeight
                    );

                spriteBatch.Draw(
                    coverTexture,
                    rectangle,
                    ScreenCoverColor * coverScaleFactor
                );

                if (isBright)
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(
                        SpriteSortMode.Deferred,
                        null
                    );
                }
            }

            DrawText(spriteBatch);
        }

        internal Vector2 CalculateOffsetOfCharacter(string character)
        {
            float extraOffset =
                character.ToLowerInvariant() == "i"
                    ? -3f
                    : 0f;

            float scaleMultiplier =
                Language.ActiveCulture.Name == "ru-RU"
                    ? 1f
                    : EnglishTextScaleMultiplier;

            return Vector2.UnitX *
                (
                    CBTFontRegistry.BossIntroScreensFont
                        .MeasureString(character).X
                    + extraOffset
                    + 10f
                ) *
                AspectRatioFactor *
                TextScale *
                scaleMultiplier;
        }

        public virtual void DrawText(SpriteBatch spriteBatch)
        {
            // Обычная задержка появления текста.
            float appearOpacity =
                Utils.GetLerpValue(
                    TextDelayInterpolant,
                    TextDelayInterpolant + 0.05f,
                    AnimationCompletion,
                    true
                );

            // Отдельное затухание после DisplayTime.
            float fadeOutStart =
                AnimationTime +
                DisplayTime;

            float fadeOutCompletion =
                Utils.GetLerpValue(
                    fadeOutStart,
                    fadeOutStart + FadeOutTime,
                    TotalTimer,
                    true
                );

            float opacity =
                appearOpacity *
                (1f - fadeOutCompletion);

            if (
                CanPlaySound &&
                SoundToPlayWithTextCreation != null &&
                !HasPlayedMainSound
            )
            {
                SoundEngine.PlaySound(
                    SoundToPlayWithTextCreation.Value
                );

                HasPlayedMainSound = true;
            }

            int absoluteLetterCounter = 0;
            bool playedNewLetterSound = false;

            string[] splitTextInstances =
                CachedText.Split('\n');

            for (
                int lineIndex = 0;
                lineIndex < splitTextInstances.Length;
                lineIndex++
            )
            {
                string splitText =
                    splitTextInstances[lineIndex];

                bool useBigText =
                    lineIndex > 0 ||
                    splitTextInstances.Length == 1;

                Vector2 offset =
                    -Vector2.UnitX *
                    CalculateLineWidth(
                        splitText,
                        useBigText
                    ) *
                    0.5f;

                Vector2 textScale =
                    Vector2.One *
                        TextScale *
                        AspectRatioFactor *
                        (
                            Language.ActiveCulture.Name == "ru-RU"
                                ? 1f
                                : EnglishTextScaleMultiplier
                        );

                if (lineIndex > 0)
                {
                    offset.Y +=
                        BottomTextScale *
                        TextScale *
                        AspectRatioFactor *
                        lineIndex *
                        24f;
                }

                if (useBigText)
                {
                    textScale *= BottomTextScale;
                }

                for (
                    int characterIndex = 0;
                    characterIndex < splitText.Length;
                    characterIndex++
                )
                {
                    float individualLineLetterCompletionRatio =
                        splitText.Length <= 1
                            ? 1f
                            : characterIndex /
                              (float)(splitText.Length - 1f);

                    float absoluteLineLetterCompletionRatio =
                        CachedText.Length <= 1
                            ? 1f
                            : absoluteLetterCounter /
                              (float)(CachedText.Length - 1f);

                    int previousTotalLettersToDisplay =
                        (int)(
                            CachedText.Length *
                            LetterDisplayCompletionRatio(
                                AnimationTimer - 1
                            )
                        );

                    int totalLettersToDisplay =
                        (int)(
                            CachedText.Length *
                            LetterDisplayCompletionRatio(
                                AnimationTimer
                            )
                        );

                    if (
                        totalLettersToDisplay >
                        previousTotalLettersToDisplay &&
                        SoundToPlayWithLetterAddition != null &&
                        !playedNewLetterSound
                    )
                    {
                        SoundEngine.PlaySound(
                            SoundToPlayWithLetterAddition.Value
                        );

                        playedNewLetterSound = true;
                    }

                    if (
                        absoluteLineLetterCompletionRatio >=
                        LetterDisplayCompletionRatio(
                            AnimationTimer
                        )
                    )
                    {
                        break;
                    }

                    string character =
                        splitText[characterIndex].ToString();

                    offset +=
                        CalculateOffsetOfCharacter(character) *
                        (useBigText
                            ? BottomTextScale
                            : 1f);

                    float gradientProgress =
                        MathHelper.Clamp(
                            (
                                DrawPosition.Y +
                                offset.Y -
                                Main.screenHeight * 0.15f
                            ) /
                            (Main.screenHeight * 0.35f),
                            0f,
                            1f
                        );

                    Color gradientColor =
                        Color.Lerp(
                            TextColorTop,
                            TextColorBottom,
                            gradientProgress
                        );

                    Color textColor =
                        CalculatedTextColor.Calculate(
                            individualLineLetterCompletionRatio
                        ) * opacity;

                    Vector2 origin =
                        Vector2.UnitX *
                        CBTFontRegistry.BossIntroScreensFont
                            .MeasureString(character);

                    for (int k = 0; k < 4; k++)
                    {
                        float afterimageOpacityInterpolant =
                            Utils.GetLerpValue(
                                1f,
                                TextDelayInterpolant + 0.05f,
                                AnimationCompletion,
                                true
                            );

                        float afterimageOpacity =
                            MathF.Pow(
                                afterimageOpacityInterpolant,
                                2f
                            ) * 0.3f;

                        Color afterimageColor =
                            textColor *
                            afterimageOpacity;

                        Vector2 drawOffset =
                            (
                                MathHelper.TwoPi *
                                k /
                                4f
                            ).ToRotationVector2() *
                            (
                                1f -
                                afterimageOpacityInterpolant
                            ) *
                            30f;

                        ChatManager.DrawColorCodedStringShadow(
                            spriteBatch,
                            CBTFontRegistry.BossIntroScreensFont,
                            character,
                            DrawPosition +
                            drawOffset +
                            offset,
                            Color.Black *
                            afterimageOpacity *
                            opacity,
                            0f,
                            origin,
                            textScale,
                            -1,
                            1.5f
                        );

                        ChatManager.DrawColorCodedString(
                            spriteBatch,
                            CBTFontRegistry.BossIntroScreensFont,
                            character,
                            DrawPosition +
                            drawOffset +
                            offset,
                            afterimageColor,
                            0f,
                            origin,
                            textScale
                        );
                    }

                    ChatManager.DrawColorCodedStringShadow(
                        spriteBatch,
                        CBTFontRegistry.BossIntroScreensFont,
                        character,
                        DrawPosition + offset,
                        Color.Black * opacity,
                        0f,
                        origin,
                        textScale,
                        -1,
                        1.5f
                    );

                    ChatManager.DrawColorCodedString(
                        spriteBatch,
                        CBTFontRegistry.BossIntroScreensFont,
                        character,
                        DrawPosition + offset,
                        textColor,
                        0f,
                        origin,
                        textScale
                    );

                    absoluteLetterCounter++;
                }
            }
        }

        private float CalculateLineWidth(
            string text,
            bool useBigText
        )
        {
            float width = 0f;

            foreach (char character in text)
            {
                width +=
                    CalculateOffsetOfCharacter(
                        character.ToString()
                    ).X *
                    (useBigText
                        ? BottomTextScale
                        : 1f);
            }

            return width;
        }

        public virtual void Update()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            if (!ShouldBeActive())
            {
                AnimationTimer = 0;
                TotalTimer = 0;
                HasCompleted = false;
                HasPlayedMainSound = false;
                CachedText = string.Empty;
                return;
            }

            // Интро уже полностью проигралось для этого босса.
            // Ждём, пока босс исчезнет.
            if (HasCompleted)
                return;

            if (string.IsNullOrEmpty(CachedText))
                CachedText = GetIntroText().Value;

            TotalTimer++;

            if (AnimationTimer < AnimationTime)
            {
                AnimationTimer++;
                return;
            }

            if (TotalTimer >= TotalAnimationTime)
            {
                HasCompleted = true;
                DoCompletionEffects();
            }
        }

        public virtual SoundStyle? SoundToPlayWithTextCreation =>
            null;
    }
}