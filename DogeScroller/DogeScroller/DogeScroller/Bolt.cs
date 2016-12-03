using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    class Bolt : Ability
    {

        

        public Bolt(Texture2D Texture, Vector2 Position, Color Color)
            : base(Texture, Position, Color)
        {
            frames.Add(new Rectangle(172, 170, 14, 43));
            frames.Add(new Rectangle(194, 174, 20, 35));
            frames.Add(new Rectangle(222, 177, 24, 29));
            frames.Add(new Rectangle(248, 178, 35, 27));
            frames.Add(new Rectangle(285, 179, 50, 24));
            frames.Add(new Rectangle(361, 175, 35, 32));

            timeFrame = new TimeSpan(0, 0, 0, 0, 60);
        }

        public override void Update(GameTime gameTime, Character character, Viewport viewport)
        {


            if ((ks.IsKeyDown(Keys.Q) || ms.LeftButton == ButtonState.Pressed) && X >= viewport.Width && character.CurrentMovement != Movement.Fly)
            {
                X = character.Position.X + character.frames[character.CurrentFrame].Width;
                Y = character.Position.Y + character.frames[character.CurrentFrame].Height / 4;
                currentFrame = 0;
            }

            if (X < viewport.Width)
            {
                X += 10;
            }



            base.Update(gameTime, character, viewport);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
