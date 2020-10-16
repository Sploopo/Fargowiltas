using Terraria;
using Terraria.ID;

namespace Fargowiltas.Items.Summons.Deviantt
{
    public class PirateFlag : BaseSummon
    {
        public override int NPCType => NPCID.PirateCaptain;

        public override string NPCName => "Pirate Captain";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pirate Flag");
            Tooltip.SetDefault("Summons Pirate Captain" +
                               "\nOnly usable at night or underground");
        }

        public override bool CanUseItem(Player player) => !Main.dayTime || player.ZoneRockLayerHeight || player.ZoneUnderworldHeight;
    }
}