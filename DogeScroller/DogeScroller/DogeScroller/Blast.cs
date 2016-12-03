using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    class Blast : Ability
    {

        bool use = false;
        bool playOnce;

        public Blast(Texture2D Texture, Vector2 Position, Color Color)
            : base(Texture, Position, Color)
        {
            frames.Add(new Rectangle(10, 301, 203, 43));
            frames.Add(new Rectangle(217, 289, 200, 66));
            frames.Add(new Rectangle(424, 279, 200, 85));

            timeFrame = new TimeSpan(0, 0, 0, 0, 120);
        }

        public override void Update(GameTime gameTime, Character character, Viewport viewport)
        {
       
            if (Scroll.score % 3 == 0 && Scroll.score > 0)
            {
                if (!playOnce)
                {
                    use = true;
                }
            }
            else
            {
                playOnce = false;
            }

            if (use && !playOnce)
            {
                if ((ks.IsKeyDown(Keys.Q) || ms.LeftButton == ButtonState.Pressed) && X >= viewport.Width && character.CurrentMovement != Movement.Fly)
                {
                    X = character.Position.X + character.frames[character.CurrentFrame].Width;
                    Y = character.Position.Y + character.frames[character.CurrentFrame].Height / 4;
                    currentFrame = 0;
                    use = false;
                    playOnce = true;
                }
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
        public void DebugDraw(SpriteBatch sb, SpriteFont font)
        {
            sb.DrawString(font, $"{use}", new Vector2(100, 0), Color.BurlyWood);
            sb.DrawString(font, $"{playOnce}", new Vector2(150, 0), Color.BurlyWood);
        }
    }
}
