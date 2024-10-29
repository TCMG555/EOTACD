using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Media;
using System;

using Penumbra;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;


namespace EOTACD
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
    

        public screen mCurrentScreen;
        public OpeningScene mGameplayOpen;
        public Chapter1_1 mGameplayCT1_1;
        public Title mMainmenu;
        public Instruction mInstruction;
        public Chapter0 mGameplayCT_0;
        public Chapter0_2 mGameplayCT0_2;
        public Menu mMenu;
        public Chapter2_1 mGameplayCT2_1;
        public Chapter2_2 mGameplayCT2_2;
        public Chapter3_1 mGameplayCT3_1;
        public Chapter3_2 mGameplayCT3_2;
        public Chapter1_2 mGameplayCT1_2;
        public screen previousScreen;

        public SpriteFont font;
        public Texture2D batteryUi, bloodLine, beckerUi, aliasUi,drone;
        KeyboardState ks, oks;

        Player player1;
        Player player2;

        Camera camera;
        Camera camera1;
        Camera camera2;

        Enemy enemy;

        public bool isPaused = false;

        public PenumbraComponent Penumbra02;
        public Spotlight playerSpot, spot1, spot2, spot3, spot4;
        private PointLight point1, point2, point3, point4, point5, point6, point7, point8, point9, point10,point11;

        public int flashBatery = 100, flashCooldown, flashBatery2;
        public int randomLight,R,G,B;
  
        public Vector2 spotPos;

        public bool c01 = false, c02 = false, c11 = false, c12 = false, c21 = false, c22 = false, c31 = false, c32 = false, c = false;

        private KeyboardState _keyboardState;
        private KeyboardState _previousState;
        
        Random r;

        public Rectangle blueRec, redRec;

        //public Song fx1, fx2, fx3, fx4, fx5, bgm1, bgm2, bgm3, bgm4, bgm5;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Penumbra02 = new PenumbraComponent(this);
            Components.Add(Penumbra02);
            r = new Random();

        }


        protected override void Initialize()
        {
            // Adjust screen width and height
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            // Initialize cameras for each player
            camera = new Camera();
            camera1 = new Camera();
            camera2 = new Camera();

            float platformY = 780f; // Platform height
            int screenWidth = 1920;
            int screenHeight = 1080;

            player1 = new Player(new Vector2(120, 920), 200f, Keys.W, Keys.S, Keys.A, Keys.D, platformY, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            player2 = new Player(new Vector2(50, 900), 200f, Keys.Up, Keys.Down, Keys.Left, Keys.Right, platformY, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Penumbra02.Initialize();
            // Initialize Penumbra

            // Add Penumbra to game components
            // In your Game1 class or wherever you initialize the player

            _keyboardState = new KeyboardState();
            _previousState = new KeyboardState();


            // Initialize lighting after Penumbra has been added


            // Initialize enemy
            enemy = new Enemy(new Vector2(500, 910), 100f); // Set initial enemy position
            LightSystem();
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //ux/ui
            batteryUi = Content.Load<Texture2D>("BatteryUsageUi");
            bloodLine = Content.Load<Texture2D>("Bloodline-export");
            beckerUi = Content.Load<Texture2D>("BeckerUi");
            aliasUi = Content.Load<Texture2D>("AliasUi-export");
            font = Content.Load<SpriteFont>("DePixel");
            drone = Content.Load<Texture2D>("DroneEye");
            // เริ่มต้นการประกาศหน้าจอต่าง ๆ
            mMainmenu = new Title(this, new EventHandler(GameplayScreenEvent));
            mGameplayOpen = new OpeningScene(this, new EventHandler(GameplayScreenEvent));
            mGameplayCT_0 = new Chapter0(this, new EventHandler(GameplayScreenEvent));
            mGameplayCT0_2 = new Chapter0_2(this, new EventHandler(GameplayScreenEvent));
            mGameplayCT1_1 = new Chapter1_1(this, new EventHandler(GameplayScreenEvent));
            mGameplayCT1_2 = new Chapter1_2(this, new EventHandler(GameplayScreenEvent));
            mGameplayCT2_1 = new Chapter2_1(this, new EventHandler(GameplayScreenEvent));
            mGameplayCT2_2 = new Chapter2_2(this, new EventHandler(GameplayScreenEvent));
            mGameplayCT3_1 = new Chapter3_1(this, new EventHandler(GameplayScreenEvent));
            mGameplayCT3_2 = new Chapter3_2(this, new EventHandler(GameplayScreenEvent));

            mInstruction = new Instruction(this, new EventHandler(GameplayScreenEvent));
            mMenu = new Menu(this, new EventHandler(GameplayScreenEvent));
            // เริ่มต้นด้วย Opening Scene
            mCurrentScreen = mMainmenu;


            //this.fx1 = Content.Load<Song>("cave-monster-43826");
            //this.fx2 = Content.Load<Song>("deep-cave-159876");
            //this.fx3 = Content.Load<Song>("inside-a-cave-effect-240264");
            
           // MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            // โหลดเนื้อหาของศัตรู
            enemy.LoadContent(Content, "WormWalkLeftt", "WormWalkRight", "WormAttackLeft", "WormAttackRight");

            

            // โหลดเนื้อหาของเกมเพิ่มเติม
        }
        //public void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
       // {
            // 0.0f is silent, 1.0f is full volume
         //   MediaPlayer.Volume = 1.0f;
            /*MediaPlayer.Play(song);
            MediaPlayer.Play(song2);*/
       // }
        protected override void Update(GameTime gameTime)
        {



            _keyboardState = Keyboard.GetState();




            mCurrentScreen.Update(gameTime);


            if (mCurrentScreen == mMainmenu)
            {
                ChangePositionLight();

            }
            if (mCurrentScreen == mGameplayOpen)
            {
                ChangePositionLight();
            }
            if (mCurrentScreen == mGameplayCT_0)
            {
                ChangePositionLight1();
                if (!c01)
                {
                    spotPos = new Vector2(250, 760);
                    c01 = true;
                }

            }
            if (mCurrentScreen == mGameplayCT0_2)
            {
                ChangePositionLight2();
                if (!c02)
                {
                    spotPos = new Vector2(250, 760);
                    c02 = true;
                }
            }
            if (mCurrentScreen == mGameplayCT1_1)
            {
                ChangePositionLight3();
                if (!c11)
                {
                    spotPos = new Vector2(250, 780);
                    c11 = true;
                }
            }
            if (mCurrentScreen == mGameplayCT1_2)
            {
                ChangePositionLight4();
                if (!c12)
                {
                    spotPos = new Vector2(250, 970);
                    c12 = true;
                }
            }
            if (mCurrentScreen == mGameplayCT2_1)
            {
                ChangePositionLight5();
                if (!c21)
                {
                    spotPos = new Vector2(250, 970);
                    c21 = true;
                }
            }
            if (mCurrentScreen == mGameplayCT2_2)
            {
                ChangePositionLight6();
                if (!c22)
                {
                    spotPos = new Vector2(250, 970);
                    c22 = true;
                }
            }
            if (mCurrentScreen == mGameplayCT3_1)
            {
                ChangePositionLight7();
                if (!c31)
                {
                    spotPos = new Vector2(250, 970);
                    c31 = true;
                }


            }
            if (mCurrentScreen == mGameplayCT3_2)
            {
                ChangePositionLight8();
                if (!c32)
                {
                    spotPos = new Vector2(250, 970);
                    c32 = true;
                }
            }




            // Toggle pause state
            if (_keyboardState.IsKeyDown(Keys.Escape) && _previousState.IsKeyUp(Keys.Escape))
            {
                isPaused = !isPaused;

                if (isPaused)
                {
                    previousScreen = mCurrentScreen;
                    mCurrentScreen = mMenu; // Show menu when paused
                                            // Hide Penumbra in menu
                }
                else
                {
                    mCurrentScreen = previousScreen;

                }
            }

            // Update based on pause state
            if (isPaused)
            {
                mMenu.Update(gameTime); // Update menu when paused
            }
            else
            {
                mCurrentScreen.Update(gameTime);
                enemy.Update(gameTime, player1);
                enemy.Update(gameTime, player2);
            }

            // Update enemy
            enemy.Update(gameTime, player1); // Send the first player to the enemy
            enemy.Update(gameTime, player2);

            if (_keyboardState.IsKeyDown(Keys.Left) )
            {
                spotPos.X -= 6.7f;
                playerSpot.Rotation = MathHelper.ToRadians(180f);
            }
            if (_keyboardState.IsKeyDown(Keys.Right) )
            {
                spotPos.X += 6.7f;
                playerSpot.Rotation = MathHelper.ToRadians(0f);
            }
            if (_keyboardState.IsKeyDown(Keys.Up)  )
            {
                spotPos.Y -= 6.7f;

            }
            if (_keyboardState.IsKeyDown(Keys.Down) )
            {
                spotPos.Y += 6.7f;

            }
            if (_keyboardState.IsKeyDown(Keys.Left) && _keyboardState.IsKeyDown(Keys.Up))
            {
                spotPos.X -= 6.7f;
                spotPos.Y -= 6.7f;
                playerSpot.Rotation = MathHelper.ToRadians(180f);
            }
            if (_keyboardState.IsKeyDown(Keys.Right) && _keyboardState.IsKeyDown(Keys.Up))
            {
                spotPos.X += 6.7f;
                spotPos.Y -= 6.7f;
                playerSpot.Rotation = MathHelper.ToRadians(0f);
            }
            if (_keyboardState.IsKeyDown(Keys.Right) && _keyboardState.IsKeyDown(Keys.Down))
            {
                spotPos.X += 6.7f;
                spotPos.Y += 6.7f;
                playerSpot.Rotation = MathHelper.ToRadians(0f);
            }
            if (_keyboardState.IsKeyDown(Keys.Left) && _keyboardState.IsKeyDown(Keys.Down))
            {
                spotPos.X -= 6.7f;
                spotPos.Y += 6.7f;
                playerSpot.Rotation = MathHelper.ToRadians(0f);
            }
            //if ({ })
            UseStuning();
            playerSpot.Position = new Vector2(spotPos.X, spotPos.Y);
            point11.Position = new Vector2 (spotPos.X+20,spotPos.Y);
             

            
            
            // Store the current keyboard state for the next frame
            _previousState = _keyboardState;

            // Store the current mouse state for the next frame


            Console.WriteLine(playerSpot.Position);
            Console.WriteLine(player1.isOnLadder);
         


            base.Update(gameTime);
        }
        public void LightSystem()
        {
            Penumbra02.AmbientColor = new Color(15, 15, 15);
            point1 = new PointLight
            {
                Position = new Vector2(155, 230),
                Color = Color.Red,
                Scale = new Vector2(90),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point2 = new PointLight
            {
                Position = new Vector2(175, 230),
                Color = Color.Green,
                Scale = new Vector2(90),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point3 = new PointLight
            {
                Position = new Vector2(195, 230),
                Color = Color.Blue,
                Scale = new Vector2(90),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point4 = new PointLight
            {
                Position = new Vector2(215, 230),
                Color = Color.Orange,
                Scale = new Vector2(90),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point5 = new PointLight
            {
                Position = new Vector2(360, 145),
                Color = Color.Red,
                Scale = new Vector2(120, 120),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point6 = new PointLight
            {
                Position = new Vector2(410, 145),
                Color = Color.Green,
                Scale = new Vector2(120, 120),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point7 = new PointLight
            {
                Position = new Vector2(460, 145),
                Color = Color.Blue,
                Scale = new Vector2(120, 120),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point8 = new PointLight
            {
                Position = new Vector2(510, 145),
                Color = Color.Orange,
                Scale = new Vector2(120, 120),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };


            point9 = new PointLight
            {
                Position = new Vector2(660, 750),
                Color = Color.Orange,
                Scale = new Vector2(300, 300),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point10 = new PointLight
            {
                Position = new Vector2(660, 750),
                Color = Color.Orange,
                Scale = new Vector2(300, 300),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };
            point11 = new PointLight
            {
                Position = new Vector2(spotPos.X, spotPos.Y),
                Color = Color.Red,
                Scale = new Vector2(90, 90),
                Intensity = 0.8f,
                ShadowType = ShadowType.Solid
            };

            playerSpot = new Spotlight
            {
                Position = new Vector2(spotPos.X, spotPos.Y),
                Rotation = MathHelper.ToRadians(0f),
                Radius = 2f,
                ConeDecay = 2f,
                Color = Color.LightGoldenrodYellow,
                Intensity = 0.8f,
                Scale = new Vector2(800, 800)
            };

            spot1 = new Spotlight
            {
                Position = new Vector2(1900, -150),
                Rotation = MathHelper.ToRadians(90f),
                Radius = 2f,
                ConeDecay = 2f,
                Color = Color.LightGoldenrodYellow,
                Intensity = 0.8f,
                Scale = new Vector2(600, 400)
            };
            spot2 = new Spotlight
            {
                Position = new Vector2(0, 0),
                Rotation = MathHelper.ToRadians(90f),
                Radius = 2f,
                ConeDecay = 2f,
                Color = Color.LightGoldenrodYellow,
                Intensity = 0.8f,
                Scale = new Vector2(500, 500)
            };

            spot3 = new Spotlight
            {
                Position = new Vector2(0, 0),
                Rotation = MathHelper.ToRadians(90f),
                Radius = 0.8f,
                ConeDecay = 5f,
                Color = Color.LightGoldenrodYellow,
                Intensity = 0.8f,
                Scale = new Vector2(500, 500)
            };
            spot4 = new Spotlight
            {
                Position = new Vector2(0, 0),
                Rotation = MathHelper.ToRadians(90f),
                Radius = 0.8f,
                ConeDecay = 3f,
                Color = Color.White,
                Intensity = 0.8f,
                Scale = new Vector2(55, 55)
            };
            Penumbra02.Lights.Add(playerSpot);
            Penumbra02.Lights.Add(spot1);
            Penumbra02.Lights.Add(spot2);
            Penumbra02.Lights.Add(spot3);
            Penumbra02.Lights.Add(spot4);

            Penumbra02.Lights.Add(point1);
            Penumbra02.Lights.Add(point2);
            Penumbra02.Lights.Add(point3);
            Penumbra02.Lights.Add(point4);
            Penumbra02.Lights.Add(point5);
            Penumbra02.Lights.Add(point6);
            Penumbra02.Lights.Add(point7);
            Penumbra02.Lights.Add(point8);
            Penumbra02.Lights.Add(point9);
            Penumbra02.Lights.Add(point10);
            Penumbra02.Lights.Add(point11);
        }

        protected override void Draw(GameTime gameTime)
        {

            string str;

            if (isPaused)
            {

                _spriteBatch.Begin();
                mMenu.Draw(_spriteBatch, gameTime); // วาดเมนูเมื่อหยุดเกม
                _spriteBatch.End();

            }
            else
            {

                Penumbra02.BeginDraw();
                _spriteBatch.Begin();
                mCurrentScreen.Draw(_spriteBatch, gameTime); // วาดหน้าจอหลักเมื่อไม่หยุดเกม
                _spriteBatch.End();
                Penumbra02.Draw(gameTime);

                if (mCurrentScreen != mMainmenu && mCurrentScreen != mGameplayOpen)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(batteryUi, new Vector2(1920 - 550, 1015), Color.Wheat);
                    _spriteBatch.Draw(bloodLine, new Vector2(1920 - 559, 970), new Rectangle(0, 0, 409, 40), Color.White);
                    _spriteBatch.Draw(bloodLine, new Vector2(0 + 150, 970), new Rectangle(0, 0, 409, 40), Color.White);
                    _spriteBatch.Draw(beckerUi, new Vector2(20, 960), Color.White);
                    _spriteBatch.Draw(aliasUi, new Vector2(1920 - 20 - 120, 960), Color.White);
                    str = "Becker";
                    _spriteBatch.DrawString(font, str, new Vector2(150, 1020), Color.White);
                    str = "Alias";
                    _spriteBatch.DrawString(font, str, new Vector2(1920 - 270, 1020), Color.White);
                    str = flashBatery.ToString();
                    _spriteBatch.DrawString(font, str, new Vector2(1920 - 380, 1025), Color.Wheat);
                    _spriteBatch.Draw(drone, new Vector2(spotPos.X,spotPos.Y-10), Color.White);
                    _spriteBatch.End();
                }

            }



        }

        public void GameplayScreenEvent(object obj, EventArgs e)
        {
            mCurrentScreen = (screen)obj;
        }
        public void ChangePositionLight()
        {
            //Chapter000
            Penumbra02.AmbientColor = Color.White;

            point1.Scale = new Vector2(0);
            point2.Scale = new Vector2(0);
            point3.Scale = new Vector2(0);
            point4.Scale = new Vector2(0);
            point5.Scale = new Vector2(0, 0);
            point6.Scale = new Vector2(0, 0);
            point7.Scale = new Vector2(0, 0);
            point8.Scale = new Vector2(0, 0);
            point9.Scale = new Vector2(0, 0);
            point10.Scale = new Vector2(0);
            playerSpot.ConeDecay = 0f;
           
            spot1.ConeDecay = 0f;
            spot2.ConeDecay = 0f;
            spot3.ConeDecay = 0f;
            spot4.ConeDecay = 0f;
        }
        public void ChangePositionLight1()
        {
            
            //Chapter001
            Penumbra02.AmbientColor = new Color(15, 15, 15);

            point1.Scale = new Vector2(0);
            point2.Scale = new Vector2(0);
            point3.Scale = new Vector2(0);
            point4.Scale = new Vector2(0);
            point5.Scale = new Vector2(0, 0);
            point6.Scale = new Vector2(0, 0);
            point7.Scale = new Vector2(0, 0);
            point8.Scale = new Vector2(0, 0);
            point9.Scale = new Vector2(0, 0);
            point10.Scale = new Vector2(0);

            playerSpot.ConeDecay = 2f;



            spot1.ConeDecay = 0f;
            spot2.ConeDecay = 0f;
            spot3.ConeDecay = 0f;
            spot4.ConeDecay = 0f;
            
          
        }
        public void ChangePositionLight2()
        {
            //Chapter002
            point1.Scale = new Vector2(0);
            point2.Scale = new Vector2(0);
            point3.Scale = new Vector2(0);
            point4.Scale = new Vector2(0);
            point5.Scale = new Vector2(0, 0);
            point6.Scale = new Vector2(0, 0);
            point7.Scale = new Vector2(0, 0);
            point8.Scale = new Vector2(0, 0);
            point9.Scale = new Vector2(0, 0);
            point10.Scale = new Vector2(0);

            //playerSpot.ConeDecay = 2f;

            //ไฟตรงประตู
            spot1.ConeDecay = 5f;
            spot1.Position = new Vector2(1850, 450);
            spot1.Scale = new Vector2(500, 550);
            spot1.Color = Color.LightGoldenrodYellow;

            spot2.ConeDecay = 0f;
            spot3.ConeDecay = 0f;
            spot4.ConeDecay = 0f;

            if (r.Next(20) == 5)
            {
                spot1.ConeDecay = 0f;
            }
            else
            {
                spot1.ConeDecay = 5f;
            }
        }
        public void ChangePositionLight3()
        {
            //Chapter101
            point1.Scale = new Vector2(90);
            point2.Scale = new Vector2(90);
            point3.Scale = new Vector2(90);
            point4.Scale = new Vector2(90);
            point5.Scale = new Vector2(120, 120);
            point6.Scale = new Vector2(120, 120);
            point7.Scale = new Vector2(120, 120);
            point8.Scale = new Vector2(120, 120);
            point9.Scale = new Vector2(300, 300);


            spot1.ConeDecay = 5f;
            spot1.Position = new Vector2(1900, -150);
            spot1.Scale = new Vector2(600, 400);


            //ไฟเพดาน
            spot2.ConeDecay = 2f;
            spot2.Position = new Vector2(500, -150);
            spot2.Scale = new Vector2(900, 1800);

            spot3.ConeDecay = 2f;
            spot3.Position = new Vector2(1300, -150);
            spot3.Scale = new Vector2(900, 1800);
            spot4.ConeDecay = 0f;
           
            if (r.Next(20) == 5)
            {
                spot1.ConeDecay = 0f;
            }
            else
            {
                spot1.ConeDecay = 5f;
            }
        }
        public void ChangePositionLight4()
        {
            //Chapter102
            //Chapter102
            point1.Scale = new Vector2(90);
            point1.Position = new Vector2(1015, 140);
            point1.Color = Color.Red;

            point2.Scale = new Vector2(90);
            point2.Position = new Vector2(1125, 140);
            point1.Color = Color.Red;

            //ไฟปุ่ม
            point3.Scale = new Vector2(100, 100);
            point3.Position = new Vector2(1360, 310);
            point3.Color = Color.LightSkyBlue;

            point4.Scale = new Vector2(100, 100);
            point4.Position = new Vector2(1570, 310);
            point4.Color = Color.LightSkyBlue;

            //ไฟหน้าจอ
            point5.Scale = new Vector2(500, 600);
            point5.Position = new Vector2(1490, 750);
            point5.Color = Color.LightSkyBlue;

            point6.Scale = new Vector2(200, 200);
            point6.Position = new Vector2(1728, 645);
            point6.Color = Color.LightSkyBlue;

            point7.Scale = new Vector2(200, 200);
            point7.Position = new Vector2(1870, 645);
            point7.Color = Color.LightSkyBlue;

            point8.Scale = new Vector2(200, 200);
            point8.Position = new Vector2(1870, 745);
            point8.Color = Color.LightSkyBlue;

            point9.Scale = new Vector2(200, 200);
            point9.Position = new Vector2(1728, 745);
            point9.Color = Color.LightSkyBlue;
            



            //ไฟตรงประตู
            spot1.ConeDecay = 5f;

            //ไฟเพดาน
            spot2.ConeDecay = 5f;
            spot2.Position = new Vector2(500, -150);
            spot2.Scale = new Vector2(900, 1800);

            spot3.ConeDecay = 5f;
            spot3.Position = new Vector2(1300, -150);
            spot3.Scale = new Vector2(900, 1800);

            spot4.ConeDecay = 0f;

            //ไฟ securx
            point10.Scale = new Vector2(600, 200);
            point10.Position = new Vector2(1500, 590);
            point10.Color = Color.Red;

            if (r.Next(20) == 5)
            {
                point3.Scale = Vector2.Zero;
                point4.Scale = Vector2.Zero;
                point8.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 10)
            {
                spot1.ConeDecay = 0f;
            }
            else if (r.Next(20) == 15)
            {
                point5.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 20)
            {
                point6.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 1)
            {
                point7.Scale = Vector2.Zero;
            }
            else
            {
                point3.Scale = new Vector2(100, 100);
                point4.Scale = new Vector2(100, 100);
                point5.Scale = new Vector2(100, 100);
                point6.Scale = new Vector2(100, 100);
                point7.Scale = new Vector2(100, 100);
                point8.Scale = new Vector2(100, 100);
                spot1.ConeDecay = 5f;
            }


        }
        public void ChangePositionLight5()
        {
            //Chapter201

            //ป้าย
            point1.Scale = new Vector2(1400, 300);
            point1.Position = new Vector2(1000, 270);
            point1.Color = Color.Violet;

            //คอม
            point2.Scale = new Vector2(90);
            point2.Position = new Vector2(425, 805);
            point2.Color = Color.Green;

            point3.Scale = new Vector2(90);
            point3.Position = new Vector2(425, 775);
            point3.Color = Color.Red;

            point4.Scale = new Vector2(200, 200);
            point4.Position = new Vector2(325, 765);
            point4.Color = Color.LightSkyBlue;

            //ป้ายประตูทางออก
            point5.Scale = new Vector2(300, 150);
            point5.Position = new Vector2(1355, 680);
            point5.Color = Color.LimeGreen;

            point6.Scale = new Vector2(150, 100);
            point6.Position = new Vector2(1355, 680);
            point6.Color = Color.White;

            //ป้ายกล่องเล่นเพลง
            point7.Scale = new Vector2(100, 50);
            point7.Position = new Vector2(1045, 835);
            point7.Color = Color.Red;

            point8.Scale = new Vector2(0, 0);
            point9.Scale = new Vector2(0, 0);
            point10.Scale = new Vector2(0);




            spot1.ConeDecay = 0f;
            spot2.ConeDecay = 0f;
            spot3.ConeDecay = 0f;
            spot4.ConeDecay = 0f;
            if (r.Next(20) == 5)
            {
                point5.Scale = Vector2.Zero;
                point6.Scale = Vector2.Zero;

            }
            else if (r.Next(20) == 10)
            {
                point2.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 15)
            {
                point3.Scale = Vector2.Zero;
            }
            else
            {
                point5.Scale = new Vector2(300, 150);
                point6.Scale = new Vector2(150, 100);
                point2.Scale = new Vector2(90, 90);
                point3.Scale = new Vector2(90, 90);
            }
        }
        public void ChangePositionLight6()
        {
            //Chapter202

            //ปุ่ม
            point1.Scale = new Vector2(100);
            point1.Position = new Vector2(1250, 820);
            point1.Color = Color.Red;

            point2.Scale = new Vector2(0);
            point3.Scale = new Vector2(0);
            point4.Scale = new Vector2(0);
            point5.Scale = new Vector2(0, 0);
            point6.Scale = new Vector2(0, 0);
            point7.Scale = new Vector2(0, 0);
            point8.Scale = new Vector2(0, 0);
            point9.Scale = new Vector2(0, 0);
            point10.Scale = new Vector2(0);

            spot1.ConeDecay = 5f;
            spot1.Position = new Vector2(10, -150);
            spot1.Scale = new Vector2(1200, 550);

            spot2.ConeDecay = 5f;
            spot2.Position = new Vector2(1750, -150);
            spot2.Scale = new Vector2(600, 400);

            spot3.ConeDecay = 0f;
            spot4.ConeDecay = 0f;

            if (r.Next(20) == 5)
            {
                spot2.ConeDecay = 0f;

            }
            else if(r.Next(20) == 6)
            {
                spot1.ConeDecay = 0f;
            }
            else
            {
                spot2.ConeDecay = 5f;
                spot1.ConeDecay = 5f;
            }

        }
        public void ChangePositionLight7()
        {
            //Chapter301
            point1.Scale = new Vector2(100);
            point1.Position = new Vector2(1165, 795);
            point1.Color = Color.Red;


            point2.Scale = new Vector2(100);
            point2.Position = new Vector2(1165, 835);
            point2.Color = Color.Green;

            point3.Scale = new Vector2(100);
            point3.Position = new Vector2(235, 700);
            point3.Color = Color.Red;

            point4.Scale = new Vector2(1700, 600);
            point4.Position = new Vector2(1000, 450);
            point4.Color = Color.SkyBlue;

            point5.Scale = new Vector2(800, 300);
            point5.Position = new Vector2(1000, 430);
            point5.Color = Color.Red;

            point6.Scale = new Vector2(180, 130);
            point6.Position = new Vector2(1290, 790);
            point6.Color = Color.LightSkyBlue;

            point7.Scale = new Vector2(100);
            point7.Position = new Vector2(400, 770);
            point7.Color = Color.Blue;

            point8.Scale = new Vector2(100);
            point8.Position = new Vector2(565, 840);
            point8.Color = Color.Blue;

            point9.Scale = new Vector2(100, 100);
            point9.Position = new Vector2(740, 900);
            point9.Color = Color.Red;

            point10.Scale = new Vector2(100, 100);
            point10.Position = new Vector2(900, 700);
            point10.Color = Color.Red;
            


            spot1.ConeDecay = 5f;
            spot1.Position = new Vector2(200, -250);
            spot1.Color = Color.White;
            spot1.Scale = new Vector2(1000, 1500);

            spot2.ConeDecay = 5f;
            spot2.Color = Color.White;
            spot2.Scale = new Vector2(1000, 1500);
            spot2.Position = new Vector2(1850, -250);

            spot3.ConeDecay = 5f;
            spot3.Color = Color.White;
            spot3.Scale = new Vector2(1000, 1500);
            spot3.Position = new Vector2(1000, -250);

            spot4.ConeDecay = 0f;
            if (r.Next(20) == 5)
            {
                point1.Scale = Vector2.Zero;

            }
            else if (r.Next(20) == 6)
            {
                point2.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 7)
            {
                point3.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 8)
            {
                point7.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 9)
            {
                point8.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 10)
            {
                point9.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 11)
            {
                point10.Scale = Vector2.Zero;
            }
            else
            {
                point1.Scale = new Vector2(100,100);
                point2.Scale = new Vector2(100, 100);
                point3.Scale = new Vector2(100, 100);
                point7.Scale = new Vector2(100, 100);
                point8.Scale = new Vector2(100, 100);
                point9.Scale = new Vector2(100, 100);
                point10.Scale = new Vector2(100, 100);
            }
        }
        public void ChangePositionLight8() 
        {
            point1.Scale = new Vector2(700);
            point1.Color = Color.Violet;
            point1.Intensity = 1f;
            point1.Position = new Vector2(1920 / 2, 1080 / 2);

            point2.Scale = new Vector2(90);
            point2.Color = Color.Red;
            point2.Intensity = 0.8f;
            point2.Position = new Vector2(905,860);

            point3.Scale = new Vector2(90);
            point3.Color = Color.Green;
            point3.Intensity = 1f;
            point3.Position = new Vector2(1095/860);

            point4.Scale = new Vector2(0);
            point5.Scale = new Vector2(0, 0);
            point6.Scale = new Vector2(0, 0);
            point7.Scale = new Vector2(0, 0);
            point8.Scale = new Vector2(0, 0);
            point9.Scale = new Vector2(0, 0);
            point10.Scale = new Vector2(0);

            //playerSpot.ConeDecay = 2f;

        
            spot1.ConeDecay = 5f;
            spot1.Position = new Vector2(30, -150);
            spot1.Scale = new Vector2(1200, 500);
            spot1.Color = Color.White;

            spot2.ConeDecay = 5f;
            spot2.Position = new Vector2(1920 / 2, -150);
            spot2.Scale = new Vector2(900, 900);
            spot2.Color = Color.White;

            spot3.ConeDecay = 5f;
            spot3.Position = new Vector2(1900, -150);
            spot3.Scale = new Vector2(1200, 500);
            spot3.Color = Color.White;

            spot4.ConeDecay = 0f;
            
            if (r.Next(20) == 5)
            {
                point2.Scale = Vector2.Zero;

            }
            else if (r.Next(20) == 6)
            {
                point3.Scale = Vector2.Zero;
            }
            else if (r.Next(20) == 7)
            {
                spot1.ConeDecay = 0f;
            }
            else if (r.Next(20) == 8)
            {
                spot3.ConeDecay = 0f;
            }
            else
            {
                point2.Scale = new Vector2(90, 90);
                point3.Scale = new Vector2(90, 90);
                spot1.ConeDecay = 5f;
                spot3.ConeDecay = 5f;
            }
        }

        public void UseStuning()
        {
     

            // Check for left or right key presses to adjust the spotlight's rotation
            ks = Keyboard.GetState();
       
          

            //ใช้ไฟฉายสตั้น
            if (ks.IsKeyDown(Keys.U) && (oks.IsKeyDown(Keys.U)) && flashBatery2 <= 499)
            {
                blueRec = new Rectangle((int)point11.Position.X,(int)point11.Position.Y, 1000, 1000);

                playerSpot.Color = Color.Blue;
                playerSpot.Intensity = 2f;
                playerSpot.Radius = 1f;
                playerSpot.Scale = new Vector2(1000, 1000);
                flashBatery2++;
                flashBatery = 100 - flashBatery2 / 5;
            }
            else if (ks.IsKeyDown(Keys.O) && (oks.IsKeyDown(Keys.O)) && flashBatery2 <= 499)
            {
                redRec = new Rectangle((int)point11.Position.X,(int)point11.Position.Y, 1500, 1500);
                playerSpot.Color = Color.Red;
                playerSpot.Intensity = 3f;
                playerSpot.Radius = 1f;
                playerSpot.Scale = new Vector2(1500, 1500);
                flashBatery2 += 5;
                flashBatery = 100 - flashBatery2 / 5;
            }
            else
            {
                
                if (flashBatery2 >= 499)
                {
                    flashCooldown++;
                    flashBatery = 0 + flashCooldown / 5;
                    if (flashCooldown >= 500)
                    {
                        flashBatery2 = 0;
                        flashCooldown = 0;
                    }

                    
                }
                playerSpot.Intensity = 1f;
                playerSpot.Color = Color.White;
                playerSpot.Scale = new Vector2(800, 800);
                blueRec = new Rectangle(0, 0, 0, 0);
                redRec = new Rectangle(0, 0, 0, 0);
            }

            oks = ks;
        }
        

    }
}
