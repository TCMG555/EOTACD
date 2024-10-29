using EOTACD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Media;

namespace EOTACD
{
    public class Instruction : screen
    {

        Game1 game;
        GraphicsDeviceManager graphics;
        Texture2D instucTex;
        Texture2D headphoneTex;
        
        

        

        public Instruction(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;

            instucTex = game.Content.Load<Texture2D>("IntructionalControl");
            headphoneTex = game.Content.Load<Texture2D>("HeadphoneUseFull");


        }

        public override void Update(GameTime gameTime)
        {

            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch theBatch,GameTime gameTime)
        {

            theBatch.Draw(instucTex, Vector2.Zero, Color.White);

            

           

        }

    }
}
