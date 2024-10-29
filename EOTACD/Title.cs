using EOTACD;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;



public class Title : screen
{
    private Game1 game;
    private Texture2D BG;
    private Texture2D StartButt, StartHoverButt;
    private Texture2D ExitButt, ExitHoverButt;
    private Texture2D OptionButt, OptionHoverButt;

    Song bgm1;

    int currentMenu = 1;
    bool keyActiveUp = false;
    bool keyActiveDown = false;
    
    private Rectangle startButtonRect, exitButtonRect, optionButtonRect;
    

    public Title(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
    {
        this.game = game;

        
        // กำหนดตำแหน่งและขนาดของปุ่ม
        startButtonRect = new Rectangle(770, 400, 300, 150);
        optionButtonRect = new Rectangle(770, 600, 300, 150);
        exitButtonRect = new Rectangle(770, 800, 300, 150);

        // โหลดพื้นหลังและปุ่ม
        BG = game.Content.Load<Texture2D>("BackGroundMenuWithText");
        StartButt = game.Content.Load<Texture2D>("start");
        StartHoverButt = game.Content.Load<Texture2D>("start_Hover");
        ExitButt = game.Content.Load<Texture2D>("Quit");
        ExitHoverButt = game.Content.Load<Texture2D>("Quit_Hover");
        OptionButt = game.Content.Load<Texture2D>("Option");
        OptionHoverButt = game.Content.Load<Texture2D>("Option_Hover");
        game.ChangePositionLight();

        //bgm1 = Content.Load<Song>("[NonCopyrightedMusic]Sappheiros-Falling(Ft.eSoreni)[Chill]");

       // MediaPlayer.Volume = 0.5f;
      // MediaPlayer.Play(bgm1);
       
    }

    public override void Update(GameTime gameTime)
    {
        
        KeyboardState keyboard = Keyboard.GetState();
       
        if (keyboard.IsKeyDown(Keys.Up))
        {
            if (keyActiveUp == true)
            {
                if (currentMenu > 1)
                {
                    currentMenu = currentMenu - 1;
                    keyActiveUp = false;
                }
            }
        }
        if (keyboard.IsKeyDown(Keys.Down))
        {
            if (keyActiveDown == true)
            {
                if (currentMenu < 3)
                {
                    currentMenu = currentMenu + 1;
                    keyActiveDown = false;
                }
            }
        }
        if (keyboard.IsKeyUp(Keys.Up))
        {
            keyActiveUp = true;
        }
        if (keyboard.IsKeyUp(Keys.Down))
        {
            keyActiveDown = true;
        }
        if (keyboard.IsKeyDown(Keys.Enter)&&currentMenu == 1)
        {
            //MediaPlayer.Pause();
            StartGame();
            
        }
        if (keyboard.IsKeyDown(Keys.Enter) && currentMenu == 2)
        {
            //MediaPlayer.Pause();
            OpenOptions();
        }
        if (keyboard.IsKeyDown(Keys.Enter) && currentMenu == 3)
        {
            //MediaPlayer.Pause();
            game.Exit();
        }







        base.Update(gameTime);
    }


    private void StartGame()
    {
        // เรียกใช้งานเหตุการณ์เริ่มเกม
        ScreenEvent.Invoke(game.mGameplayOpen, new EventArgs());
    }

    private void OpenOptions()
    {
        // โค้ดสำหรับการตั้งค่า
    }

    public override void Draw(SpriteBatch theBatch, GameTime gameTime)
    {



        // วาดพื้นหลังและปุ่ม
        theBatch.Draw(BG, new Rectangle(0, 0, 1920, 1080), Color.White);
     
        if (currentMenu == 1)
        {
            theBatch.Draw(StartHoverButt, new Rectangle(770, 400, 300, 150), Color.White);
        }
        else
        {
            theBatch.Draw(StartButt, new Rectangle(770, 400, 300, 150), Color.White);
        }

        if (currentMenu == 2)
        {
            theBatch.Draw(OptionHoverButt, new Rectangle(770, 600, 300, 150), Color.White);
        }
        else 
        { 
            theBatch.Draw(OptionButt, optionButtonRect, Color.White); 
        }

        if (currentMenu == 3)
        {
            theBatch.Draw(ExitHoverButt, exitButtonRect, Color.White);
        }
        else
        {
            theBatch.Draw(ExitButt, exitButtonRect, Color.White);
        }
      




        base.Draw(theBatch, gameTime);
    }




}
