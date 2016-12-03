using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    class Fireball : Ability
    {

        public Fireball(Texture2D Texture, Vector2 Position, Color Color)
            : base(Texture, Position, Color)
        {
            frames.Add(new Rectangle(680, 0, 151, 83));
            frames.Add(new Rectangle(856, 0, 171, 85));
            frames.Add(new Rectangle(1055, 2, 173, 87));
            frames.Add(new Rectangle(860, 142, 171, 87));
            frames.Add(new Rectangle(1050, 142, 171, 87));

            timeFrame = new TimeSpan(0, 0, 0, 0, 120);
        }
        public override void Update(GameTime gameTime, Character character, Viewport viewport)
        {
            if ((ks.IsKeyDown(Keys.Q) || ms.LeftButton == ButtonState.Pressed) && X >= viewport.Width && character.CurrentMovement == Movement.Fly)
            {
                X = character.Position.X + character.frames[character.CurrentFrame].Width;
                Y = character.Position.Y + character.frames[character.CurrentFrame].Height / 4;
                currentFrame = 0;
            }

            if (X < viewport.Width)
            {
                X += 7;
            }
            base.Update(gameTime, character, viewport);
        }
    }
}
