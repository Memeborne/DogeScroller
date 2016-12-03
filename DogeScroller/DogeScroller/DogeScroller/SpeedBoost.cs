using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    class SpeedBoost : PowerUp
    {

        TimeSpan duration;
        bool playOnce = false;

        public static bool intersected = false;

        private List<Character> speedTrail = new List<Character>();

        public List<Character> SpeedTrail
        {
            get { return speedTrail; }
            set { speedTrail = value; }
        }


        public SpeedBoost(Texture2D Texture, Vector2 Position, Color Color, SoundEffect SF, int Distance)
            : base(Texture, Position, Color, SF, Distance)
        {
            for (int i = 1; i < 6; i++)
            {
                frames.Add(new Rectangle(78, 548, 44, 44));
            }
            frames.Add(new Rectangle(211, 548, 44, 44));
            frames.Add(new Rectangle(278, 548, 44, 44));
            frames.Add(new Rectangle(345, 548, 44, 44));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


                hitbox = new Rectangle((int)Position.X, (int)Position.Y, frames[CurrentFrame].Width, frames[CurrentFrame].Height);

            


            if (intersected)
            {

                if (!playOnce)
                {
                    Scroll.speed *= 10;
                    revert += 1;
                    playOnce = true;
                }


                duration += gameTime.ElapsedGameTime;
                if (duration > new TimeSpan(0, 0, 0, 1, 500))
                {
                    Scroll.speed /= 10;
                    duration = TimeSpan.Zero;
                    intersected = false;
                    playOnce = false;
                }
            }

        }
        public override void Hit(Character character)
        {
            byte a = 200;


            if (hitbox.Intersects(character.Hitbox))
            {
                intersected = true;
            }

            if (intersected)
            {
                if (speedTrail.Count < 5)
                {
                    for (int i = 0, x = 15; i < 5; i++, x += 15)
                    {

                        Character newCharacter = new Character(character.texture, character.Position, Color.LightGreen);
                        newCharacter.CurrentMovement = Movement.Run;
                        newCharacter.color.A = a;
                        newCharacter.Position -= new Vector2(x, 0);
                        a -= 30;

                        speedTrail.Add(newCharacter);
                    }
                }
                for (int i = 0, x = 15; i < speedTrail.Count; i++, x += 15)
                {
                    speedTrail[i].Position = new Vector2(character.Position.X - x, character.Position.Y);
                }
            }
            else
            {
                speedTrail.Clear();
            }


            base.Hit(character);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {


            base.Draw(spriteBatch);

            if (speedTrail.Count > 0)
            {
                for (int i = speedTrail.Count - 1; i > -1; i--)
                {
                    speedTrail[i].Draw(spriteBatch);
                }
            }
        }


    }
}
