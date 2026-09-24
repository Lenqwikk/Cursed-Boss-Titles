using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;

namespace CBT.Systems
{
    public class UIRenderingSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");

            if (mouseIndex != -1)
            {
                layers.Insert(mouseIndex, new LegacyGameInterfaceLayer(
                    "CBT: Boss Introduction",
                    () =>
                    {
                        IntroScreenManager.Draw();
                        return true;
                    },
                    InterfaceScaleType.None
                ));
            }
        }
    }
}