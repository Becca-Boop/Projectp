using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Project
{
    public class Player : Thing
    {
        //int health; 
        int frames = 0;
        double totalElapsed;
        double jumpStartTime;
        Game Game;
        bool isjumping = false;
        const int gravity = 2;
        Vector2 velocity = Vector2.Zero;
        bool pausing = false;

        public Player(Texture2D _texture, Vector2 _position, Rectangle _boundingBox, Game _game) : base(_texture, _position, _boundingBox)
        {
            //health = 100; 
            Game = _game;
            Game.paused = false;
        }

        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            if (!Game.paused)
            {
                // Handle timing issues
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                totalElapsed += elapsed;
                long delay = (long)totalElapsed / 80;

                int heightOverFloor = GetHeightOverFloor(Game);
                frames = 6;

                // What does the player want to do?
                int dir = 0;                
                if (Keyboard.GetState().IsKeyDown(Keys.A)) dir--;
                if (Keyboard.GetState().IsKeyDown(Keys.D)) dir++;
                bool jump = Keyboard.GetState().IsKeyDown(Keys.Space) && heightOverFloor == 0;
                bool falling = !isjumping && heightOverFloor > 0;
                bool sliding = Keyboard.GetState().IsKeyDown(Keys.LeftShift) && heightOverFloor == 0;
                if (jump)
                {
                    isjumping = true;
                    jumpStartTime = totalElapsed;
                }
                else if (isjumping && jumpStartTime + 500 < totalElapsed)
                {
                    isjumping = false;
                }

                // Determine horizontal speed modifier
                int div = 7;
                if (sliding)
                {
                    div = 4;
                }

                // Vertical movement (ignores frames)
                if (falling)
                {
                    int inc = (int)elapsed / 3;
                    // Fall no further than floor
                    if (inc > heightOverFloor) inc = heightOverFloor;
                    BigBoundingBox.Y += inc;
                    Position.Y += inc;
                }
                if (isjumping)
                {
                    double jumpTime = totalElapsed - jumpStartTime;
                    int inc = (int)(elapsed * 20 / (jumpTime + 10));
                    BigBoundingBox.Y -= inc;
                    Position.Y -= inc;
                }

                if (dir != 0)
                {
                    // Select the frame
                    int frameStart = dir == -1 ? 3 : 7;

                    int frameCount = 3;
                    if (sliding)
                    {
                        frameStart = dir == -1 ? 0 : 12;
                        frameCount = 1;
                    }
                    frames = (isjumping || falling) ? frameStart : ((int)delay % frameCount) + frameStart;


                    // Horizontal movement
                    int inc = (int)elapsed / div * dir;
                    // Dip a toe in
                    BigBoundingBox.X += inc;
                    if (this.IsColliding(Game))
                    {
                        // Hit something, so undo the change and stop
                        BigBoundingBox.X -= inc;
                    }
                    else
                    {
                        Position.X += inc;
                        delay = (long)totalElapsed / 80;
                    }
                }
                // Now draw it
                sourceRect = new Rectangle(28 * frames, 0, 28, 40);
            }
            spriteBatch.Draw(Texture, Position, sourceRect, Color.White);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                pausing = true;
            }
            else
                if (pausing)
                {
                    Game.paused = !Game.paused;
                    pausing = false;
                }



        }
    }
}