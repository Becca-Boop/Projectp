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

                if (this.IsColliding(Block)) return true;

            }

            return false;

        }

        public bool IsCollidingFloor(Game game)

        {

            foreach (var Floor in game.Floors)

            {

                if (this.IsColliding(Floor)) return true;

            }

            return false;

        }



        public bool IsColliding(Thing otherThing)

        {

            Console.WriteLine(

                BigBoundingBox.X + "," + BigBoundingBox.Y + " - "

                + Position.X + "," + Position.Y + " - "

                + otherThing.BigBoundingBox.X + "," + otherThing.BigBoundingBox.Y

            );

            return BigBoundingBox.Intersects(otherThing.BigBoundingBox);

        }

    }







}

