using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Thing
    {
        protected Vector2 Position;
        protected Texture2D Texture;
        protected Rectangle LittleBoundingBox;
        protected Rectangle BigBoundingBox;
        protected Rectangle sourceRect;

        public Thing(Texture2D _texture, Vector2 _position, Rectangle _boundingBox)
        {
            Texture = _texture;
            Position = _position;
            LittleBoundingBox = _boundingBox;
            BigBoundingBox = new Rectangle(LittleBoundingBox.X + (int)Position.X, LittleBoundingBox.Y + (int)Position.Y, LittleBoundingBox.Width, LittleBoundingBox.Height);
        }

        public bool IsColliding(Game game)
        {
            foreach (var Block in game.Blocks)
            {
                if (this.IsColliding(Block)) 
                    return true;
            }
            return false;
        }

        public bool IsColliding(Thing otherThing)
        {
            return BigBoundingBox.Intersects(otherThing.BigBoundingBox);
        }

        public int GetHeightOver(Thing otherThing)
        {
            // Is this over the other? If not, return -1
            if (BigBoundingBox.Left > otherThing.BigBoundingBox.Right) 
                return 9999;
            if (BigBoundingBox.Right < otherThing.BigBoundingBox.Left) 
                return 9999;

            // Is this under (or overlapping) the other? If so, return -1
            if (BigBoundingBox.Bottom > otherThing.BigBoundingBox.Top) 
                return 9999;

            return otherThing.BigBoundingBox.Top - BigBoundingBox.Bottom;
        }

        public int GetHeightOverFloor(Game game)
        {
            int result = 9999;
            foreach (var Block in game.Blocks)
            {
                int height = this.GetHeightOver(Block);
                if (height < result) result = height;
            }
            return result;
        }
    }
}

