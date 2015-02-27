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
    abstract class Actor
    {
        private Texture2D textureImage;
        private Vector2 position;
        private Point frameSize;
        private int collisionOffset;
        private Point currentFrame;
        private Point sheetSize;
        private int timeSincelastFrame;
        private int millisecondsPerFrame;
        private Vector2 speed;
        private BoundingBox collisionRectangle;
        private BoundingSphere collisionSphere;
        protected Game inGame;

        public Texture2D TextureImage 
        {
            get
            {
                return textureImage;
            }
            protected set
            {
                textureImage = value;  
            }
        }
        public Vector2 Position
        {
            get
            {
                return position;
            }
            protected set
            {
                position = value;
            }
        }
        public Point FrameSize
        {
            get
            {
                return frameSize;
            }
            protected set
            {
                frameSize = value;
            }
        }
        public int CollisionOffset
        {
            get
            {
                return collisionOffset;
            }
            protected set
            {
                collisionOffset = value;
            }
        }
        public Point CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            protected set
            {
                currentFrame = value;
            }
        }
        public Point SheetSize
        {
            get
            {
                return sheetSize;
            }
            protected set
            {
                sheetSize = value;
            }
        }
        public int TimeSinceLastFrame 
        {
            get
            {
                return timeSincelastFrame;
            }
            protected set
            {
                timeSincelastFrame = value;
            }
        }
        public int MillisecondsPerFrame
        {
            get
            {
                return millisecondsPerFrame;
            }
            protected set
            {
                millisecondsPerFrame = value;
            }
        }
        public Vector2 Speed
        {
            get
            {
                return speed;
            }
            protected set
            {
                speed = value;
            }
        }
        public BoundingBox CollisionRectangle
        {
            get
            {
                return collisionRectangle;
            }
            protected set
            {
                collisionRectangle = value;
            }
        }
        public BoundingSphere CollisionSphere
        {
            get
            {
                return collisionSphere;
            }
            protected set
            {
                collisionSphere = value;
            }
        }
    
        public Actor(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, int timeSincelastFrame, int millisecondsPerFrame, Vector2 speed, Game inGame) 
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.timeSincelastFrame = timeSincelastFrame;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.speed = speed;
            this.inGame = inGame;
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage, position, new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }
        public virtual void update(GameTime gameTime, Actor actor) { }
    }
}
