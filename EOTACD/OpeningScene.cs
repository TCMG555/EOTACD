using EOTACD;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
public class OpeningScene : screen
{
    Texture2D openingTexture, note1, note2, note3, instrutional, headP, logo1,logo2,logo3,logo4,logo5,logo6 ;
    Game1 game;
    GraphicsDeviceManager graphics;

    Player player1;
    Player player2;

    AnimatedTexture AliasLeft;
    AnimatedTexture AliasRight;
    AnimatedTexture AliasIdleLeft;
    AnimatedTexture AliasIdleRight;
    AnimatedTexture BeckerLeft;
    AnimatedTexture BeckerRight;
    AnimatedTexture BeckerIdleLeft;
    AnimatedTexture BeckerIdleRight;
    AnimatedTexture climb;


    const float Rotation = 0;
    const float Scale = 1.0f;
    const float Depth = 0.5f;
    
    int  timer1;

    

    public OpeningScene(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
    {
        this.game = game;
        
        
        // Initialize player1 and player2 for the OpeningScene
        player1 = new Player(new Vector2(120, 765), 200f, Keys.I, Keys.K, Keys.J, Keys.L, 765f, 1920 , 1080);
        player2 = new Player(new Vector2(50, 765), 200f, Keys.W, Keys.S, Keys.A, Keys.D, 765f, 1920, 1080);

        note1 = game.Content.Load<Texture2D>("cut1");
        note2 = game.Content.Load<Texture2D>("cut2");
        note3 = game.Content.Load<Texture2D>("cut3");
        instrutional = game.Content.Load<Texture2D>("IntructionalControl");
        headP = game.Content.Load<Texture2D>("HeadphoneUseFull");
        logo1 = game.Content.Load<Texture2D>("logo1");
        logo2 = game.Content.Load<Texture2D>("logo2");
        logo3 = game.Content.Load<Texture2D>("logo3");
        logo4 = game.Content.Load<Texture2D>("logo4");
        logo5 = game.Content.Load<Texture2D>("logo5");
        logo6 = game.Content.Load<Texture2D>("logo6");

        // Load textures for the players
        player1.LoadContent(game.Content, "AliasWalkLeft", "AliasWalkRight", "AliasIdlelLeft", "AliasIdleRight", "AliasClimbUp", "AliasIdlelLeft", "AliasIdleRight");
        player2.LoadContent(game.Content, "BeckerWalkLeft", "BeckerWalkRight", "BeckerIdleLeft", "BeckerIdleRight", "BeckerClimbUp", "BeckerPickaxeLeft", "BeckerPickaxeRightt");
       
        

       

        // Load the background texture for the OpeningScene
        openingTexture = game.Content.Load<Texture2D>("OpeningScene");
    }

    

    public override void Update(GameTime gameTime)
    {
        
        player1.Update(gameTime);
        player2.Update(gameTime);
        timer1++;
        // Add your scene transition logic here if necessary
        if (player1.Position.X > 1600 && player2.Position.X > 1600 && Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            // Transition to the next scene
            ScreenEvent.Invoke(game.mGameplayCT_0, new EventArgs());
            return;
        }
       // if (timer1 >= 100) { MediaPlayer.Play(game.fx1); }
            
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            timer1 += 4000;
        }
     
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch theBatch,GameTime gameTime)
    {
        string str;
       
        if (timer1 >= 0 && timer1 <= 50)
        {
            theBatch.Draw(logo6, Vector2.Zero, Color.White);
        }
        if (timer1 >=50 && timer1 <= 100)
        {
            
            theBatch.Draw(logo5, Vector2.Zero, Color.White);
        }
        if (timer1 >=100 && timer1 <= 150)
        {
            theBatch.Draw(logo4, Vector2.Zero, Color.White);
        }
        if (timer1 >= 150 && timer1 <= 200)
        {
            theBatch.Draw(logo3, Vector2.Zero, Color.White);
        }
        if (timer1 >= 200 && timer1 <= 250)
        {
            theBatch.Draw(logo2, Vector2.Zero, Color.White);
        }
        if (timer1 >= 250 && timer1 <= 500)
        {
            theBatch.Draw(logo1, Vector2.Zero, Color.White);
        }
        if (timer1 >= 500 && timer1 <= 550)
        {
            theBatch.Draw(logo2, Vector2.Zero, Color.White);
        }
        if (timer1 >= 550 && timer1 <= 600)
        {
            theBatch.Draw(logo3, Vector2.Zero, Color.White);
        
        }
        if (timer1 >= 600 && timer1 <= 650)
        {
            theBatch.Draw(logo4, Vector2.Zero, Color.White);
        }
        if (timer1 >= 650 && timer1 <= 700)
        {
            theBatch.Draw(logo5, Vector2.Zero, Color.White);
        }
        if (timer1 >= 700 &&  timer1 <= 750)
        {
            theBatch.Draw(logo6, Vector2.Zero, Color.White);
        }
        if(timer1 > 750 && timer1 <= 1000)
        {
            theBatch.Draw(headP, Vector2.Zero, Color.White);
        }
        if (timer1 > 1000 && timer1 <= 1500)
        {
            theBatch.Draw(instrutional, Vector2.Zero, Color.White);
        }
        if (timer1 > 1500 && timer1 <= 2000)
        {
            theBatch.Draw(note1, Vector2.Zero, Color.White);
        }
        if (timer1 > 2000 && timer1 <= 2500)
        {
            theBatch.Draw(note2, Vector2.Zero, Color.White);
        }
        if (timer1 > 2500 && timer1 <= 3500)
        {
            theBatch.Draw(note3, Vector2.Zero, Color.White);
        }
        if (timer1 > 3500 )
        {
            theBatch.Draw(openingTexture, new Rectangle(0, 0, 1920, 1080), Color.White);
            player1.Draw(theBatch);
            player2.Draw(theBatch);
        }
    }
}
