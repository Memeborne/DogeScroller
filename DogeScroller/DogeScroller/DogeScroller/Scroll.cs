using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogeScroller
{
    class Scroll
    {
        private Sprite image1;
        private Sprite image2;

        public static Vector2 speed = new Vector2(10, 0);
        bool added;

        KeyboardState ks;

        public static int score;




        public Scroll(Sprite Image1, Sprite Image2, Viewport viewport)
        {
            image1 = Image1;
            image2 = Image2;

            image1.Position = new Vector2(0, viewport.Height - 600);
            image2.Position = new Vector2(image1.Position.X + image1.Hitbox.Width, viewport.Height - 600);
        }

        public void Update(Viewport viewport)
        {
            ks = Keyboard.GetState();


            if (Character.currentMovement != Movement.Idle)
            {
                image1.Y = viewport.Height - 600;
                image2.Y = viewport.Height - 600;

                image1.X -= speed.X;
                image2.X -= speed.X;

                if (image1.Position.X < 0)
                {
                    image2.Position = new Vector2(image1.Position.X + image1.Hitbox.Width, viewport.Height - 600);

                    if (!added)
                    {
                        score++;
                        added = true;
                    }
                }
                if (image2.Position.X < 0)
                {
                    image1.Position = new Vector2(image2.Position.X + image2.Hitbox.Width, viewport.Height - 600);
                    added = false;

                }
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            image1.Draw(spriteBatch);
            image2.Draw(spriteBatch);
        }
    }
}

