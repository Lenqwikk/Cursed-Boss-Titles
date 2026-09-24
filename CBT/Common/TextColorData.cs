using System;
using Microsoft.Xna.Framework;

namespace CBT.Common
{
    public struct TextColorData
    {
        public Func<float, Color> ColorSelectionFunction;

        public TextColorData(Color color)
        {
            ColorSelectionFunction = _ => color;
        }

        public TextColorData(Func<float, Color> colorSelectionFunction)
        {
            ColorSelectionFunction = colorSelectionFunction;
        }

        public readonly Color Calculate(float completionRatio)
        {
            return ColorSelectionFunction(completionRatio);
        }

        public static implicit operator TextColorData(Color color)
        {
            return new TextColorData(color);
        }
    }
}