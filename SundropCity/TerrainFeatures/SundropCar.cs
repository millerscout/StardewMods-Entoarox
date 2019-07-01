using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley;
using StardewValley.TerrainFeatures;

namespace SundropCity.TerrainFeatures
{
    class SundropCar : LargeTerrainFeature
    {
        private readonly bool Mirror;
        private readonly Facing Facing;
        private readonly Rectangle RenderRect;
        private readonly Texture2D Texture;

        private static readonly Rectangle Up = new Rectangle(0, 0, 3 * 16, 5 * 16);
        private static readonly Rectangle Down = new Rectangle(3 * 16, 0, 3 * 16, 5 * 16);
        private static readonly Rectangle Sideways = new Rectangle(0, 5 * 16, 5 * 16, 3 * 16);
        private static readonly Random Rand = new Random();
        internal static List<Texture2D> Textures = new List<Texture2D>();

        public SundropCar(Vector2 tilePosition, Facing facing, Texture2D texture=null) : base(false)
        {
            this.tilePosition.Value = tilePosition;
            this.Facing = facing;
            this.Mirror = facing == Facing.Left;
            switch (facing)
            {
                case Facing.Down:
                    this.RenderRect = Down;
                    break;
                case Facing.Up:
                    this.RenderRect = Up;
                    break;
                case Facing.Left:
                case Facing.Right:
                    this.RenderRect = Sideways;
                    break;
            }
            this.Texture = texture ?? Textures[Rand.Next(0,Textures.Count)];
        }

        public override Rectangle getBoundingBox(Vector2 tileLocation)
        {
            switch (this.Facing)
            {
                case Facing.Up:
                case Facing.Down:
                    return new Rectangle((int)tileLocation.X * 64 - 64, (int)tileLocation.Y * 64 - 128, 3 * 64, 5 * 64);
                case Facing.Left:
                case Facing.Right:
                    return new Rectangle((int)tileLocation.X * 64 - 128, (int)tileLocation.Y * 64, 5 * 64, 2 * 64);
            }
            throw new NotImplementedException("Invalid codepoint reached, SundropCar.Facing has a invalid value.");
        }

        public override Rectangle getRenderBounds(Vector2 tileLocation)
        {
            switch (this.Facing)
            {
                case Facing.Up:
                case Facing.Down:
                    return new Rectangle((int)tileLocation.X * 64 - 64, (int)tileLocation.Y * 64 - 128, 3 * 64, 5 * 64);
                case Facing.Left:
                case Facing.Right:
                    return new Rectangle((int)tileLocation.X * 64 - 128, (int)tileLocation.Y * 64 - 64, 5 * 64, 3 * 64);
            }
            throw new NotImplementedException("Invalid codepoint reached, SundropCar.Facing has a invalid value.");
        }

        public override void draw(SpriteBatch b, Vector2 tileLocation)
        {
            Vector2 vector = new Vector2(tileLocation.X * 64, tileLocation.Y * 64);
            switch(this.Facing)
            {
                case Facing.Up:
                case Facing.Down:
                    vector.X -= 64;
                    vector.Y -= 128;
                    break;
                case Facing.Left:
                case Facing.Right:
                    vector.X -= 128;
                    vector.Y -= 64;
                    break;
            }
            b.Draw(this.Texture, Game1.GlobalToLocal(Game1.viewport, vector), this.RenderRect, Color.White, 0, Vector2.Zero, Game1.pixelZoom, this.Mirror ? SpriteEffects.FlipHorizontally : SpriteEffects.None, (this.RenderRect.Height * 4 + vector.Y) / 10000f);
        }
    }
}
