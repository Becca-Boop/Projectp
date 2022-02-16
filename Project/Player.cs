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



        public KeyboardState _currentKey;

        public KeyboardState _previousKey;

        //int health; 



        int frames = 0;

        double totalElapsed;

        Game Game;

        bool issliding = false;

        bool isjumping = false;

        bool FloorCollide = true;

        const int gravity = 2;

        Vector2 velocity = Vector2.Zero;





        public Player(Texture2D _texture, Vector2 _position, Rectangle _boundingBox, Game _game) : base(_texture, _position, _boundingBox)

        {

            //health = 100; 

            Game = _game;

        }





        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)

        {

            _previousKey = _currentKey;

            _currentKey = Keyboard.GetState();



            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            totalElapsed += elapsed;

            long delay = (long)totalElapsed / 80;



            if (FloorCollide == false)

            {

                Position.Y += 5;

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.A))

            {

                if (isjumping == false)

                {

                    issliding = false;

                    isjumping = true;

                    updatePosition(-1, 7, elapsed, 7);

                    if (Keyboard.GetState().IsKeyUp(Keys.Space))

                        isjumping = false;

                }

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.A))

            {

                issliding = false;

                isjumping = true;

                updatePosition(1, 5, elapsed, 7);

                if (Keyboard.GetState().IsKeyUp(Keys.Space))

                    isjumping = false;

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space))

            {

                issliding = false;

                isjumping = true;

                updatePosition(0, 6, elapsed, 7);

                if (Keyboard.GetState().IsKeyUp(Keys.Space))

                    isjumping = false;

            }





            if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))

            {

                issliding = true;

                if (isjumping == false)

                {

                    updatePosition(-1, 0, elapsed, 3);

                }

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.LeftShift))

            {

                issliding = true;

                if (isjumping == false)

                {

                    updatePosition(1, 12, elapsed, 3);

                }

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.A))

            {

                issliding = false;

                if (isjumping == false)

                {

                    updatePosition(-1, 3, elapsed, 7);

                }

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D))

            {

                issliding = false;

                if (isjumping == false)

                {

                    updatePosition(1, 7, elapsed, 7);

                }

            }

            else

            {

                frames = 6;

            }



            if (this.IsCollidingFloor(Game))

            {

                velocity.Y = 0;

            }

            else

            {

                velocity.Y += gravity * elapsed;

            }

            Position.Y += 5 * velocity.Y / elapsed;
            BigBoundingBox.Y += (int)(5 * velocity.Y / elapsed);






            sourceRect = new Rectangle(28 * frames, 0, 28, 40);



            spriteBatch.Draw(Texture, Position, sourceRect, Color.White);

        }





        private void updatePosition(int dir, int frameStart, float elapsed, int div)

        {



            if (this.IsCollidingFloor(Game))

            {

                FloorCollide = true;

                int inc = (int)elapsed / div * dir;

                BigBoundingBox.X += inc;



                if (this.IsColliding(Game))

                {

                    BigBoundingBox.X -= inc;

                }

                else

                {

                    Position.X += inc;

                    if (isjumping == true)

                    {

                        velocity.Y = -2;

                        for (int i = 0; i < 5; i++)
                        {

                            Position.Y += (int)velocity.Y * 5 / elapsed;
                            BigBoundingBox.Y += (int)(velocity.Y * 5 / elapsed);
                        }



                    }

                    else if (issliding == true)

                    {

                        frames = frameStart;

                    }

                    else

                    {

                        long delay = (long)totalElapsed / 80;

                        frames = ((int)delay % 3) + frameStart;

                    }

                }

            }

            else

            {

                FloorCollide = false;

            }





        }





    }

}