using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    class DragonForm : PowerUp
    {
        TimeSpan duration;
        public static float morf;
        bool playOnce = false;

        public static bool morfing = false;


        private Color cColor;

        public Color CColor
        {
            get { return cColor; }
            set { cColor = value; }
        }


        public static bool intersected = false;


        public DragonForm(Texture2D Texture, Vector2 Position, Color Color, SoundEffect SF, int Distance)
            : base(Texture, Position, Color, SF, Distance)
        {
            for (int i = 1; i < 6; i++)
            {
                frames.Add(new Rectangle(78, 430, 44, 38));
            }
            frames.Add(new Rectangle(145, 430, 44, 44));
            frames.Add(new Rectangle(212, 430, 44, 44));
            frames.Add(new Rectangle(279, 430, 44, 44));
            morf = 0;
            cColor = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            hitbox = new Rectangle((int)Position.X, (int)Position.Y, frames[CurrentFrame].Width, frames[CurrentFrame].Height);

            if (morfing)
            {
                if (morf < 1)
                {
                    cColor = Color.Lerp(Color.White, Color.Red, morf);
                    morf += 0.01f;
                }
                else
                {
                    if (!playOnce)
                    {
                        Scroll.speed *= 3;
                        playOnce = true;
                    }
                }
            }
            else
            {
                morf = 0;
                cColor = Color.White;
            }


            if (intersected)
            {
                morfing = true;

                duration += gameTime.ElapsedGameTime;
                if (duration > new TimeSpan(0, 0, 0, 7))
                {
                    Scroll.speed /= 3;
                    duration = TimeSpan.Zero;
                    intersected = false;
                    morfing = false;
                    playOnce = false;
                }
            }

        }

        public override void Hit(Character character)
        {



            if (hitbox.Intersects(character.Hitbox))
            {
                intersected = true;
            }


            if (intersected)
            {

                base.Hit(character);
            }
            
            if (morf < 1)
            {
                character.Y = 375;
            }




        }
    }
}
