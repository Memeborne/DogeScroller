using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DogeScroller
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand = new Random();
        SpriteFont scoreFont;

        Scroll scroll;
        Character character;

        SpeedBoost sb;
        DragonForm df;

        Bolt bolt;
        Fireball fireball;
        Blast blast;

        Sprite bg1, bg2;
        Texture2D bgTexture1, bgTexture2;
        Texture2D characterTexture;
        Texture2D pTexture, aTexture;

        Song music;
        SoundEffect sbSF, dfSF;
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {


            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            scoreFont = Content.Load<SpriteFont>("ScoreFont");

            #region Scroll
            bgTexture1 = Content.Load<Texture2D>("backgrounds/background1");
            bgTexture2 = Content.Load<Texture2D>("backgrounds/background2");

            bg1 = new Sprite(bgTexture1, new Vector2(0, 0), Color.White);
            bg2 = new Sprite(bgTexture2, new Vector2(0, 0), Color.White);

            scroll = new Scroll(bg2, bg1, GraphicsDevice.Viewport);
            #endregion
            #region Character
            characterTexture = Content.Load<Texture2D>("sprite sheets/doge sprite sheet");

            character = new Character(characterTexture, new Vector2(GraphicsDevice.Viewport.Width / 2, 375), Color.White);
            #endregion

            pTexture = Content.Load<Texture2D>("sprite sheets/power up sprite sheet");
            #region SpeedBoost
            sbSF = Content.Load<SoundEffect>("audio/Powerup23");

            sb = new SpeedBoost(pTexture, new Vector2(0, 0), Color.White, sbSF, rand.Next(1000, 3000));
            sb.Position = new Vector2(sb.Distance, 375);
            #endregion
            #region DragonForm
            dfSF = Content.Load<SoundEffect>("audio/roar");

            df = new DragonForm(pTexture, new Vector2(0, 0), Color.White, dfSF, rand.Next(5000, 7000));
            df.Position = new Vector2(df.Distance, 375);
            #endregion

            aTexture = Content.Load<Texture2D>("sprite sheets/lasers");
            #region Bolt
            bolt = new Bolt(aTexture, new Vector2(GraphicsDevice.Viewport.Width + 10, 0), Color.White);
            #endregion
            #region Fireball
            fireball = new Fireball(aTexture, new Vector2(GraphicsDevice.Viewport.Width + 10, 0), Color.White);
            #endregion
            #region Blast
            blast = new Blast(aTexture, new Vector2(GraphicsDevice.Viewport.Width + 10, 0), Color.White);
            #endregion

            #region Song
            music = Content.Load<Song>("audio/Crush 40- Fight The Knight 8 Bit Remix (850 Sub Special Part 7-10)");
            MediaPlayer.Play(music);
            MediaPlayer.Volume -= 0.6f;
            #endregion
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();




            scroll.Update(GraphicsDevice.Viewport);

            character.Update(gameTime);
            character.Boundaries(GraphicsDevice.Viewport);

           /* sb.Update(gameTime);
            sb.Hit(character);
            df.Update(gameTime);
            df.Hit(character);*/
            bolt.Update(gameTime, character, GraphicsDevice.Viewport);
            fireball.Update(gameTime, character, GraphicsDevice.Viewport);
            blast.Update(gameTime, character, GraphicsDevice.Viewport);
            foreach (Character item in sb.SpeedTrail)
            {
                item.Update(gameTime);
            }

            character.color = df.CColor;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            scroll.Draw(spriteBatch);
            sb.Draw(spriteBatch);
            df.Draw(spriteBatch);
            character.Draw(spriteBatch);
            bolt.Draw(spriteBatch);
            fireball.Draw(spriteBatch);
            blast.Draw(spriteBatch);
            blast.DebugDraw(spriteBatch, scoreFont);
            spriteBatch.DrawString(scoreFont, "Score:" + Scroll.score, new Vector2(10, 10), Color.SaddleBrown);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
