﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Fargowiltas.Projectiles.Explosives
{
    public class ShurikenProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shuriken");
        }

        public override void SetDefaults()
        {
            Projectile.width = 11;
            Projectile.height = 11;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 600;
            AIType = 48;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.Kill();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Explosion>(), 0, Projectile.knockBack, Projectile.owner);

            Vector2 position = Projectile.Center;
            SoundEngine.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 16;     // bigger = boomer

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (xPosition < 0 || xPosition >= Main.maxTilesX || yPosition < 0 || yPosition >= Main.maxTilesY)
                        continue;

                    Tile tile = Main.tile[xPosition, yPosition];
                    Player player = Main.player[Projectile.owner];
                    Item bestPickaxe = player.GetBestPickaxe();

                    // Circle
                    if ((x * x + y * y) <= radius)
                    {
                        // Hit the tile 6 times, most tiles that you can break will break in 1-3 hits.
                        for (int i = 0; i < 6; i++)
                        {
                            if (tile.IsActuated)
                                break;

                            player.PickTile(xPosition, yPosition, bestPickaxe != null ? bestPickaxe.pick : 35);
                        }

                        Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120);
                    }
                }
            }
        }
    }
}