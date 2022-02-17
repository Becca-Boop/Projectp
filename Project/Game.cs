using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project
{
    /// <summary> 
    /// This is the main type for your game.
    /// </summary> 
    /// 
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static bool paused = false;
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D PlayerSprite;
        public Texture2D BlockSprite;
        private Texture2D background;
        public Texture2D PauseOverlay;
        public List<Player> Players = new List<Player>();
        public List<Block> Blocks = new List<Block>();
        public int frameCount = 0;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1440;  
            // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 810;  
            // set this value to the desired height of your window 
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        public void Log(String s)
        {
            System.Diagnostics.Trace.WriteLine(s);
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
            PauseOverlay = Content.Load<Texture2D>("Pause Menu");

            Players.Add(new Player(PlayerSprite, new Vector2(100, 530), new Rectangle(2, 2, 24, 36), this));
            Blocks.Add(new Block(BlockSprite, new Vector2(600, 540), new Rectangle(0, 0, 28, 40)));
            Blocks.Add(new Block(BlockSprite, new Vector2(50, 460), new Rectangle(0, 0, 28, 40)));
            Blocks.Add(new Block(BlockSprite, new Vector2(28, 580), new Rectangle(0, 0, 600, 20)));
            Blocks.Add(new Block(BlockSprite, new Vector2(50, 500), new Rectangle(0, 0, 300, 20)));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F1))
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

            spriteBatch.Draw(background, new Rectangle(0, 0, 1440, 810), Color.White);

            foreach (var Block in Blocks)
            {
                Block.Update(gameTime, spriteBatch);
            }

            Players[0].Update(gameTime, spriteBatch);

            if (paused)
            spriteBatch.Draw(PauseOverlay, Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);

        }

    }

}