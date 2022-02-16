using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    class Background
    {
        private Texture2D Texture;
        private Vector2 Offset;
        public Vector2 Speed;
        public float Zoom;

        private Viewport Viewport;

        private Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Offset.X), (int)(Offset.Y), (int)(Viewport.Width / Zoom), (int)(Viewport.Height / Zoom));
            }
        }

        public Background(Texture2D texture, Vector2 speed, float zoom)
        {
            Texture = texture;
            Offset = Vector2.Zero;
            Speed = speed;
            Zoom = zoom;
        }

        public void Update(GameTime gametime, Vector2 direction, Viewport viewport)
        {
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;

            Viewport = viewport;

            Vector2 distance = direction * Speed * elapsed;

            Offset += distance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(Viewport.X, Viewport.Y), Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
        }
    }
}
