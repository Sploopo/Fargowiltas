using Terraria;
using Terraria.ID;

namespace Fargowiltas.Items.Summons.Deviantt
{
    public class AttractiveOre : BaseSummon
    {
        public override int NPCType => NPCID.UndeadMiner;

        public override string NPCName => "Undead Miner";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Attractive Ore");
            Tooltip.SetDefault("Summons Undead Miner" +
                               "\nOnly usable at night or underground");
        }

        public override bool CanUseItem(Player player) => !Main.dayTime || player.ZoneRockLayerHeight || player.ZoneUnderworldHeight;
    }
}