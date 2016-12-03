using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    class AnimatedSprite : Sprite
    {
        public List<Rectangle> frames;

        TimeSpan elapsedTime;

        public TimeSpan timeFrame;

        protected int currentFrame;
        protected bool frameControl = false;

        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }

        public AnimatedSprite(Texture2D Texture, Vector2 Position, Color Color)
            : base(Texture, Position, Color)
        {
            frames = new List<Rectangle>();
        }
        public virtual void Update(GameTime gameTime)
        {

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > timeFrame)
            {
                elapsedTime = new TimeSpan();
                currentFrame++;
            }

            if (!frameControl)
            {
                if (currentFrame >= frames.Count)
                {
                    currentFrame = 0;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, frames[currentFrame], color);

        }
    }
}
