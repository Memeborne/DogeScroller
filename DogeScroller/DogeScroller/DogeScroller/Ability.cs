using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{


    class Ability : Sprite
    {
        protected KeyboardState ks;
        protected MouseState ms;
        TimeSpan elapsedTime = new TimeSpan();
        protected TimeSpan timeFrame;

        protected int currentFrame = 0;
        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }
        public List<Rectangle> frames;

        public Ability(Texture2D Texture, Vector2 Position, Color Color)
            : base(Texture, Position, Color)
        {
            frames = new List<Rectangle>();
        }

        public virtual void Update(GameTime gameTime, Character character, Viewport viewport)
        {
            ks = Keyboard.GetState();
            ms = Mouse.GetState();
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > timeFrame)
            {
                elapsedTime = new TimeSpan();
                currentFrame++;
            }
            if (currentFrame >= frames.Count)
            {
                currentFrame = frames.Count - 1;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, frames[currentFrame], color);
        }
    }

}
