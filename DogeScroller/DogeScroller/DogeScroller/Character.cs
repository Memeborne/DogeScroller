using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    public enum Movement
    {
        Idle, Run, Fly
    }


    class Character : AnimatedSprite
    {

        Dictionary<Movement, List<Rectangle>> frameDict = new Dictionary<Movement, List<Rectangle>>();
        public static Movement currentMovement;
        public Movement CurrentMovement
        {
            get { return currentMovement; }
            set { currentMovement = value; }
        }



        KeyboardState ks;
        MouseState ms;

        List<Rectangle> idleFrames = new List<Rectangle>();
        List<Rectangle> runFrames = new List<Rectangle>();
        List<Rectangle> flyFrames = new List<Rectangle>();

        int speed = 10;


        public Character(Texture2D Texture, Vector2 Position, Color Color)
            : base(Texture, Position, Color)
        {
            CurrentMovement = Movement.Idle;

            #region Movement.Idle
            frameDict.Add(Movement.Idle, idleFrames);

            idleFrames.Add(new Rectangle(183, 31, 54, 65));
            idleFrames.Add(new Rectangle(171, 113, 50, 66));
            #endregion
            #region Movement.Run
            frameDict.Add(Movement.Run, runFrames);

            runFrames.Add(new Rectangle(259, 27, 63, 71));
            runFrames.Add(new Rectangle(337, 25, 63, 72));
            runFrames.Add(new Rectangle(413, 25, 68, 72));
            runFrames.Add(new Rectangle(492, 25, 68, 69));
            runFrames.Add(new Rectangle(572, 26, 70, 63));
            runFrames.Add(new Rectangle(654, 30, 66, 68));
            runFrames.Add(new Rectangle(741, 30, 62, 68));
            runFrames.Add(new Rectangle(819, 30, 62, 67));
            #endregion
            #region Movement.Fly
            frameDict.Add(Movement.Fly, flyFrames);

            flyFrames.Add(new Rectangle(11, 560, 251, 183));
            flyFrames.Add(new Rectangle(305, 537, 235, 205));
            flyFrames.Add(new Rectangle(600, 566, 264, 174));
            flyFrames.Add(new Rectangle(866, 651, 261, 136));
            flyFrames.Add(new Rectangle(1189, 678, 208, 168));
            flyFrames.Add(new Rectangle(1482, 675, 234, 151));
            flyFrames.Add(new Rectangle(1776, 640, 255, 130));
            #endregion
        }


        public override void Update(GameTime gameTime)
        {

            ks = Keyboard.GetState();
            ms = Mouse.GetState();





            frames = frameDict[CurrentMovement];
            if (CurrentFrame >= frameDict[CurrentMovement].Count)
            {
                CurrentFrame = 0;
            }


            hitbox = new Rectangle((int)Position.X, (int)Position.Y, frameDict[CurrentMovement][CurrentFrame].Width, frameDict[CurrentMovement][CurrentFrame].Height);





            #region Key Detection



            if (DragonForm.morf < 1)
            {
                if (ks.IsKeyDown(Keys.Right))
                {
                    CurrentMovement = Movement.Run;
                    timeFrame = new TimeSpan(0, 0, 0, 0, 50);
                    X += speed - (speed / 3);
                }
                else if (ks.IsKeyDown(Keys.Left))
                {
                    CurrentMovement = Movement.Run;
                    timeFrame = new TimeSpan(0, 0, 0, 0, 50);
                    X -= speed;
                }
                else if (SpeedBoost.intersected)
                {
                    CurrentMovement = Movement.Run;
                }
                else if (!DragonForm.morfing)
                {
                    CurrentMovement = Movement.Idle;
                    timeFrame = new TimeSpan(0, 0, 0, 0, 250);
                }
            }
            else
            {
                CurrentMovement = Movement.Fly;
                timeFrame = new TimeSpan(0, 0, 0, 0, 250);

                if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W))
                {
                    Y -= 10;
                }
                else if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S))
                {
                    Y += 10;
                }

                if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
                {
                    X += speed - (speed / 2);
                }
                else if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
                {
                    X -= speed;
                }
            }




            #region Moving
            if (CurrentMovement == Movement.Run)
            {
                timeFrame = new TimeSpan(0, 0, 0, 0, 50);
            }
            else if (CurrentMovement == Movement.Idle)
            {
                timeFrame = new TimeSpan(0, 0, 0, 0, 250);
            }
            #endregion
            #endregion





            base.Update(gameTime);
        }

        public void Boundaries(Viewport viewport)
        {

                if (Position.X + frames[CurrentFrame].Width >= viewport.Width - 200)
                {
                    X = viewport.Width - frames[CurrentFrame].Width - 200;
                }
                if (Position.X < 200)
                {
                    X = 200;
                }
                if (Position.Y < 75)
                {
                    Y = 75;
                }
           
            if (DragonForm.morf > 1)
            {
                if (Position.Y < 75)
                {
                    Y = 75;
                }
                if (Position.Y + frames[CurrentFrame].Height > viewport.Height - 75)
                {
                    Y = viewport.Height - frames[CurrentFrame].Height - 75;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, frames[CurrentFrame], color);
        }



    }
}
