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
    class GoalSprite : Actor
    {

        public GoalSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, int timeSincelastFrame, int millisecondsPerFrame, Vector2 speed, Game inGame, SoundBank soundBank)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, timeSincelastFrame, millisecondsPerFrame, speed, inGame, soundBank)
        {
            // create the bounding sphere by taking half the framesize for x and y which gives the center and radius
            CollisionRectangle = new BoundingBox(new Vector3(frameSize.X, frameSize.Y, 0), new Vector3(frameSize.X, frameSize.Y, 0));
        }

        public override void update(GameTime gameTime, Actor actor)
        {
            return;
        }
    }
}
