using System;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;





namespace Project

{

    /// <summary> 

    /// This is the main type for your game. 

    /// </summary> 

    public class Game : Microsoft.Xna.Framework.Game

    {

        GraphicsDeviceManager graphics;

        public SpriteBatch spriteBatch;



        public Texture2D PlayerSprite;

        public Texture2D BlockSprite;



        private Texture2D background;



        public List<Player> Players = new List<Player>();

        public List<Block> Blocks = new List<Block>();

        public List<Floor> Floors = new List<Floor>();

















        public Game()

        {

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1000;  // set this value to the desired width of your window 

            graphics.PreferredBackBufferHeight = 800;   // set this value to the desired height of your window 

            graphics.ApplyChanges();

            Content.RootDirectory = "Content";



        }





        protected override void Initialize()

        {



            base.Initialize();

        }





        protected override void LoadContent()

        {

            spriteBatch = new SpriteBatch(GraphicsDevice);



            PlayerSprite = Content.Load<Texture2D>("SPSS");

            BlockSprite = Content.Load<Texture2D>("block");



            background = Content.Load<Texture2D>("Background");



            Players.Add(new Player(PlayerSprite, new Vector2(100, 100), new Rectangle(2, 2, 24, 36), this));

            Blocks.Add(new Block(BlockSprite, new Vector2(600, 100), new Rectangle(2, 2, 24, 36)));

            Blocks.Add(new Block(BlockSprite, new Vector2(50, 100), new Rectangle(2, 2, 24, 36)));

            Floors.Add(new Floor(BlockSprite, new Vector2(50, 260), new Rectangle(2, 2, 600, 20)));

            Floors.Add(new Floor(BlockSprite, new Vector2(50, 130), new Rectangle(2, 2, 300, 20)));







        }



        /// <summary> 

        /// UnloadContent will be called once per game and is the place to unload 

        /// game-specific content. 

        /// </summary> 

        protected override void UnloadContent()

        {

        }

        protected override void Update(GameTime gameTime)

        {



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))

                Exit();







            base.Update(gameTime);

        }



        /// <summary> 

        /// This is called when the game should draw itself. 

        /// </summary> 

        /// <param name="gameTime">Provides a snapshot of timing values.</param> 

        protected override void Draw(GameTime gameTime)

        {

            GraphicsDevice.Clear(Color.CornflowerBlue);



            spriteBatch.Begin();





            spriteBatch.Draw(background, new Rectangle(0, 0, 1000, 800), Color.White);



            foreach (var Block in Blocks)

            {

                Block.Update(gameTime, spriteBatch);

            }

            foreach (var Floor in Floors)

            {

                Floor.Update(gameTime, spriteBatch);

            }

            Players[0].Update(gameTime, spriteBatch);



            spriteBatch.End();





            base.Draw(gameTime);

        }

    }

}