using EOTACD;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Reflection.Metadata;

public class Chapter2_2 : screen
{
    Game1 game;
    Player player1;
    Player player2;
    Camera camera1;
    Camera camera2;
    GraphicsDeviceManager graphics;

    Rectangle player1Rectangle;
    Rectangle player2Rectangle;
   
    Color barrColor = Color.Transparent;


    Texture2D bgLab;
    Texture2D button;

    AnimatedTexture AliasLeft;
    AnimatedTexture AliasRight;
    AnimatedTexture AliasIdleLeft;
    AnimatedTexture AliasIdleRight;
    AnimatedTexture BeckerLeft;
    AnimatedTexture BeckerRight;
    AnimatedTexture BeckerIdleLeft;
    AnimatedTexture BeckerIdleRight;
    AnimatedTexture BeckerAttackRight;
    AnimatedTexture BeckerAttackLeft;
    AnimatedTexture climb;

    private bool isAttacking = false;

    const float Rotation = 0;
    const float Scale = 1.0f;
    const float Depth = 0.5f;



    Enemy enemy; // ตัวแปรสำหรับศัตรู
    bool enemySpawned = false; // สถานะว่าศัตรูเกิดขึ้นหรือไม่
    const float spawnDistance = 200f; // ระยะห่างที่กำหนดในการเกิดศัตรู
    private float spawnTimer; // ตัวจับเวลาในการแสดงแอนิเมชันการเกิด
    private const float spawnDuration = 2.0f; // ระยะเวลาแอนิเมชันการเกิด
    private AnimatedTexture walkLeft;
    private AnimatedTexture walkRight;
    private AnimatedTexture attackLeft;
    private AnimatedTexture attackRight;
    private AnimatedTexture spawn; // แอนิเมชันการเกิด


    public Chapter2_2(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
    {
        this.game = game;



        // Initialize player1 and player2 for Chapter1_1 with different starting positions
        player1 = new Player(new Vector2(120, 910), 200f, Keys.I, Keys.K, Keys.J, Keys.L, 765f, 1920, 1080);
        player2 = new Player(new Vector2(50, 900), 200f, Keys.W, Keys.S, Keys.A, Keys.D, 765f, 1920 , 1080);

        // Initialize cameras for the split-screen
        camera1 = new Camera();
        camera2 = new Camera();

        // Load textures for the players
        player1.LoadContent(game.Content, "AliasWalkLeft800p", "AliasWalkRight800p", "AliasIdlelLeft800p", "AliasIdleRight800p", "AliasClimbUp800p", "AliasIdlelLeft800p", "AliasIdleRight800p");
        player2.LoadContent(game.Content, "BeckerWalkLeft800p", "BeckerWalkRight800p", "BeckerIdleLeft800p", "BeckerIdleRight800p", "BeckerClimbUp800p", "BeckerPickaxeLeft800p", "BeckerPickaxeRight800p");


        bgLab = game.Content.Load<Texture2D>("Chapter2_02");
        button = game.Content.Load<Texture2D>("Chapter202Button");



    }

    public override void Update(GameTime gameTime)
    {
        // Update players
        player1.Update(gameTime);
        player2.Update(gameTime);

        player1Rectangle = new Rectangle((int)player1.Position.X, (int)player1.Position.Y, 160, 160); // ขนาดของผู้เล่น
        player2Rectangle = new Rectangle((int)player2.Position.X, (int)player2.Position.Y, 160, 160); // ขนาดของผู้เล่น

        CheckCollision(player1);
        CheckCollision(player2);

        // Handle player attacks
        if (Keyboard.GetState().IsKeyDown(Keys.E))
        {
            isAttacking = true;
            isAttacking = false;
        }


        // Update cameras
        camera1.Update(player1.Position);
        camera2.Update(player2.Position);

        if (player1.Position.X > 1600 || player2.Position.X > 1600 && Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            // Transition to the next scene
            ScreenEvent.Invoke(game.mGameplayCT3_1, new EventArgs());
            return;
        }

        /// Enemy

        enemy = new Enemy(new Vector2(500, 800), 100f);
        enemy = new Enemy(new Vector2(550, 800), 100f);
        enemy = new Enemy(new Vector2(530, 800), 100f);
        enemy = new Enemy(new Vector2(600, 800), 100f);
        enemy = new Enemy(new Vector2(520, 800), 100f);// สร้างศัตรูที่ตำแหน่งนี้
        enemy.LoadContent(game.Content, "WormWalkLeftt", "WormWalkRight", "WormAttackLeft", "WormAttackRight"); // โหลดเนื้อหาศัตรู
        enemySpawned = true; // เปลี่ยนสถานะการเกิดศัตรู



        // อัปเดตศัตรู (ถ้ามี)
        if (enemySpawned)
        {
            if (spawnTimer < spawnDuration)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds; // เพิ่มตัวจับเวลา
            }
            else
            {
                enemy.Update(gameTime, player1);
            }
        }


        // อัปเดตศัตรู (ถ้ามี)
        if (enemySpawned)
        {
            if (spawnTimer < spawnDuration)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds; // เพิ่มตัวจับเวลา
            }
            else
            {
                enemy.Update(gameTime, player1);
            }
        }

        base.Update(gameTime);
    }

    private void CheckCollision(Player player)
    {
        // สร้าง Rectangle สำหรับผู้เล่น
      

        // ตรวจสอบการชนระหว่างผู้เล่นและ barrier
      
    }



    public override void Draw(SpriteBatch theBatch, GameTime gameTime)
    {
        // Set up viewports for split-screen
        //  Viewport leftViewport = new Viewport(0, 0, 1920 / 2, 1080);
        //  Viewport rightViewport = new Viewport(1920 , 0, 1920, 1080);

        // Draw player1 on the left screen
        //  game.GraphicsDevice.Viewport = leftViewport;

        //  theBatch.Draw(backgroundTextureL, new Rectangle(0, 0, leftViewport.Width, leftViewport.Height), Color.White);



        // Draw player2 on the right screen
        //    game.GraphicsDevice.Viewport = rightViewport;

        theBatch.Draw(bgLab, new Rectangle(0, 0, 1920, 1080), Color.White);
        player1.Draw(theBatch);
        player2.Draw(theBatch);



        Texture2D rectangleTexture = new Texture2D(game.GraphicsDevice, 1, 1);
        rectangleTexture.SetData(new Color[] { Color.White });

        // Reset viewport to the full screen
        //  game.GraphicsDevice.Viewport = new Viewport(0, 0, 1920, 1080);






    }
}