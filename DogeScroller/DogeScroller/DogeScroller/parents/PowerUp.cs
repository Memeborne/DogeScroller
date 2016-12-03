using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    class PowerUp : AnimatedSprite
    {
        SoundEffect sf;
        Random rand = new Random();

        private bool intersected = false;
        bool playOnce = false;
        protected int revert = 5;

        protected int distance;

        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public PowerUp(Texture2D Texture, Vector2 Position, Color Color, SoundEffect SF, int Distance)
            : base(Texture, Position, Color.White)
        {
            timeFrame = new TimeSpan(0, 0, 0, 0, 60);
            sf = SF;
            distance = Distance;
        }

        public override void Update(GameTime gameTime)
        {

            if (Character.currentMovement != Movement.Idle)
            {
                X -= revert;
            }
            base.Update(gameTime);
        }
        public virtual void Hit(Character character)
        {


            if (hitbox.Intersects(character.Hitbox))
            {
                if (!playOnce)
                {
                    sf.Play(1f, 0, 0);
                    playOnce = false;
                }

                distance = rand.Next(distance, distance * 2);
                X = distance;

                intersected = true;
            }

            if (X + frames[CurrentFrame].Width < 0)
            {
                distance = rand.Next(distance, distance * 2);
                X = distance;
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }

}
