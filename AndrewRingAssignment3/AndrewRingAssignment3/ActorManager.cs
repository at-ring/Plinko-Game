using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AndrewRingAssignment3
{
    class ActorManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private List<Actor> actorList;
        private Vector2 cameraPosition;
        private PlayerSprite playerSprite;
        private Game inGame;
        public ActorManager(Game game) : base(game) 
        {
            actorList = new List<Actor>();
            cameraPosition = Vector2.Zero;
            inGame = game;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            // add the spites to the list for the manager to look though
            playerSprite = new PlayerSprite(Game.Content.Load<Texture2D>(@"Images/PlayerCircle"),new Vector2(0,0), new Point(116,116), 0, new Point(1,1), new Point(1,1), 0, 0, Vector2.Zero, inGame);
            actorList.Add(new PegSprite(Game.Content.Load<Texture2D>(@"Images/Peg"), new Vector2(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height /2), new Point(54, 54), 0, new Point(1, 1), new Point(1, 1), 0, 0, Vector2.Zero, inGame));
            //actorList.Add(new GoalSprite());
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Game.Exit();

            // TODO: Add your update logic here
            foreach (Actor currentActor in actorList)
            {
                if (currentActor is PegSprite || currentActor is GoalSprite)
                {                    
                    playerSprite.update(gameTime, currentActor);
                }
            }
            playerSprite.update(gameTime, playerSprite);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Matrix screenMatrix = Matrix.CreateTranslation(new Vector3(-cameraPosition, 0));
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, screenMatrix);
                // draw each sprite
                playerSprite.draw(gameTime, spriteBatch);
                foreach (Actor currentActor in actorList)
                {
                    if (currentActor is PegSprite || currentActor is GoalSprite)
                    {
                        currentActor.draw(gameTime, spriteBatch);
                    }
                    else
                    {
                        Console.WriteLine("Unknown Actor Type");
                    }
                }
            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
