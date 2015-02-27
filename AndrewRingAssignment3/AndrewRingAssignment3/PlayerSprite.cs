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
    class PlayerSprite : Actor
    {
        private bool hasBeenReleased;
        private Vector2 naturalSpeed;

        public Vector2 NaturalSpeed
        {
            get
            {
                return naturalSpeed;
            }
            private set
            {
                naturalSpeed = value;
            }
        }

        public PlayerSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, int timeSincelastFrame, int millisecondsPerFrame, Vector2 speed, Game inGame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, timeSincelastFrame, millisecondsPerFrame, speed, inGame)
        {
            hasBeenReleased = false;
            // create the bounding sphere by taking half the framesize for x and y which gives the center and radius
            base.CollisionSphere = new BoundingSphere(new Vector3((frameSize.X / 2) + Position.X, (frameSize.Y / 2) + Position.Y, 0), frameSize.X / 2);
            naturalSpeed = new Vector2(0.0f, 0.5f);
        }

        public override void update(GameTime gametime, Actor actor)
        {
            if (hasBeenReleased)
            {
                // if actor is a peg sprite check to see if the bounded spheres intersect
                if (actor is PegSprite)
                {
                    // player's collision sphere
                    BoundingSphere collisionSphere = CollisionSphere;
                    // actor's collision sphere
                    BoundingSphere sphere = actor.CollisionSphere;
                    if (Position.Y > inGame.Window.ClientBounds.Y)
                    {
                        inGame.Exit();
                    } 
                    // does the object collide with this sphere?
                    else if (sphere.Intersects(collisionSphere))
                    {
                        // if the center of the peg is to the right of the center of the ball
                        if (sphere.Center.X > collisionSphere.Center.X)
                        {
                            Speed = new Vector2(Speed.X - 0.1f, 0);
                            Position = Position + Speed;
                        }
                        // if the center of the peg is to the left of the center of the ball
                        else if (sphere.Center.X < collisionSphere.Center.X)
                        {
                            Speed = new Vector2(Speed.X + 0.1f, 0);
                            Position = Position + Speed;
                        }
                        // the center of the peg and the center of the ball have the same x coordinate
                        else
                        {
                            // determine which way the ball should fall
                            Random random = new Random();
                            int randomNumber = random.Next(0, 1);
                            if (randomNumber == 0)
                            {
                                Speed = new Vector2(Speed.X - 0.1f, 0);
                                Position = Position + Speed;
                            }
                            else
                            {
                                Speed = new Vector2(Speed.X + 0.1f, 0);
                                Position = Position + Speed;
                            }
                        }
                    }
                    else
                    {
                        // move the ball downwards in the same direction
                        if (Speed.X > 0)
                        {
                            // begin returning to the natural speed
                            Speed = new Vector2(Speed.X - 0.1f, Math.Max(Speed.Y + 0.1f, naturalSpeed.Y));
                            Position = Position + Speed;
                        }
                        else if (Speed.X < 0)
                        {
                            // begin returning to the natural speed
                            Speed = new Vector2(Speed.X + 0.1f, Math.Max(Speed.Y + 0.1f, naturalSpeed.Y));
                            Position = Position + Speed;
                        }
                        else
                        {
                            Position = Position + Speed;
                        }
                    }
                }
            }
            else
            {
                // allows for the ball to move left or right without dropping until enter is pressed
                KeyboardState keyboardState = Keyboard.GetState();
                Vector2 currentPosition = Position;
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    if (currentPosition.X > 0)
                    {
                        Position = new Vector2(currentPosition.X - 1.0f, currentPosition.Y);
                        Position = Position + Speed;
                    }
                    else
                    {
                        Position = new Vector2(0, currentPosition.Y);
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    if (currentPosition.X < inGame.Window.ClientBounds.Width - FrameSize.X)
                    {
                        Position = new Vector2(Position.X + 1.0f, Position.Y);
                        Position = Position + Speed;
                    }
                    else
                    {
                        Position = new Vector2(inGame.Window.ClientBounds.Width - FrameSize.X, currentPosition.Y);
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.R))
                {
                    hasBeenReleased = true;
                    // start moving the ball downwards
                    Speed = naturalSpeed;
                    Position = Position + Speed;                    
                }
            }
            this.CollisionSphere = new BoundingSphere(new Vector3(Position.X + (FrameSize.X / 2), Position.Y + (FrameSize.Y / 2), 0), FrameSize.X / 2);
        }
    }
}


// goal sprite collision include after peg sprite collision
/*
                else if (actor is GoalSprite)
                {
                    // actor's collision rectangle
                    BoundingBox collisionRectangle = actor.CollisionRectangle;
                    // player's collision sphere
                    BoundingSphere collisionSphere = CollisionSphere;
                    if (collisionRectangle.Intersects(collisionSphere))
                    {
                        Vector2 center = new Vector2(FrameSize.X / 2, FrameSize.Y / 2);
                        // if the center of the box is to the right of the center of the ball move left
                        if (center.X > collisionSphere.Center.X)
                        {
                            Speed = new Vector2(Speed.X - 0.1f, Speed.Y + 0.2f);
                            Position = Position + Speed;
                        }
                        // if the center of the box is to the left of hte center of the ball move right
                        else if (center.X < collisionSphere.Center.X)
                        {
                            Speed = new Vector2(Speed.X + 0.1f, Speed.Y + 0.2f);
                            Position = Position + Speed;
                        }
                        // if the center for the ball and box are the same choose which way to fall
                        else
                        {
                            // determine which way the ball should fall
                            Random random = new Random();
                            int randomNumber = random.Next(0, 1);
                            if (randomNumber == 0)
                            {
                                Speed = new Vector2(Speed.X - 0.1f, Speed.Y + 0.2f);
                                Position = Position + Speed;
                            }
                            else
                            {
                                Speed = new Vector2(Speed.X + 0.1f, Speed.Y + 0.2f);
                                Position = Position + Speed;
                            }
                        }
                    }
                    else
                    {
                        // move the ball downwards in the same direction
                        if (Speed.X > 0)
                        {
                            // begin returning to the natural speed
                            Speed = new Vector2(Speed.X - 0.1f, Speed.Y - 0.2f);
                            Position = Position + Speed;
                        }
                        else
                        {
                            // begin returning to the natural speed
                            Speed = new Vector2(Speed.X + 0.1f, Speed.Y - 0.2f);
                            Position = Position + Speed;
                        }
                    }
                }
                else if (Position.Y > inGame.Window.ClientBounds.Y)
                {
                    inGame.Exit();
                }
            }*/
