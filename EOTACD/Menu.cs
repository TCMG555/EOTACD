using EOTACD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

using System.Diagnostics;

namespace EOTACD
{
    public class Menu : screen
    {
        
        private Game1 game;
        private Texture2D BG;
        private Texture2D RestartButt, RestartHoverButt;
        private Texture2D ExitButt, ExitHoverButt;
        private Texture2D ReturnButt, ReturnHoverButt;

        int currentMenu = 1;
        bool keyActiveUp = false;
        bool keyActiveDown = false;

        private Rectangle RestartButtonRect, ExitButtonRect, ReturnButtonRect;
        private MouseState currentMouse, previousMouse;
        private Vector2 mousePosition;
        private KeyboardState keyboardState, previousState;

        public Menu(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;

            // กำหนดตำแหน่งและขนาดของปุ่ม
            RestartButtonRect = new Rectangle(810, 450, 250, 100);
            ReturnButtonRect = new Rectangle(720, 550, 480, 100);
            ExitButtonRect = new Rectangle(810, 650, 250, 100);

            // โหลดพื้นหลังและปุ่ม
            BG = game.Content.Load<Texture2D>("MenuPopUpBackground");
            RestartButt = game.Content.Load<Texture2D>("Restart");
            RestartHoverButt = game.Content.Load<Texture2D>("Restart_Hover");
            ExitButt = game.Content.Load<Texture2D>("Quit");
            ExitHoverButt = game.Content.Load<Texture2D>("Quit_Hover");
            ReturnButt = game.Content.Load<Texture2D>("Return_To_Title_Screen_Mouse");
            ReturnHoverButt = game.Content.Load<Texture2D>("Return_Hover");

            game.ChangePositionLight();

        }
        public override void Initialize()
        {
            

            base.Initialize();
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
            if (keyboard.IsKeyDown(Keys.Enter) && currentMenu == 1)
            {
                RestartGame();
            }
            if (keyboard.IsKeyDown(Keys.Enter) && currentMenu == 2)
            {
               ReturnToTitleScreen();
            }
            if (keyboard.IsKeyDown(Keys.Enter) && currentMenu == 3)
            {
                game.Exit();
            }

            base.Update(gameTime);
        }

        private void RestartGame()
        {
            // Logic for in-game restart
            ScreenEvent.Invoke(game.mGameplayOpen, EventArgs.Empty);
            return;
        }

        private void ReturnToTitleScreen()
        {
            // Logic to return to main menu
            ScreenEvent.Invoke(game.mMainmenu, EventArgs.Empty);
            return;
        }

        public override void Draw(SpriteBatch theBatch,GameTime gameTime)
        {
         

            // วาดพื้นหลังและปุ่ม
           
            theBatch.Draw(BG, new Rectangle(710, 290, 500, 500), Color.White);
            if (currentMenu == 1)
            {
                theBatch.Draw(RestartHoverButt,RestartButtonRect, Color.White);
            }
            else
            {
                theBatch.Draw(RestartButt,RestartButtonRect, Color.White);
            }

            if (currentMenu == 2)
            {
                theBatch.Draw(ReturnHoverButt, ReturnButtonRect, Color.White);
            }
            else
            {
                theBatch.Draw(ReturnButt, ReturnButtonRect, Color.White);
            }

            if (currentMenu == 3)
            {
                theBatch.Draw(ExitHoverButt, ExitButtonRect, Color.White);
            }
            else
            {
                theBatch.Draw(ExitButt, ExitButtonRect, Color.White);
            }


        }

    }
}
