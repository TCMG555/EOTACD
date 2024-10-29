using EOTACD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Reflection.Metadata;
using Penumbra;



    public class Chapter0 : screen
    {
    Game1 game;
    Texture2D BG;

    

    Song song;
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

    bool isAttacking = false;

        const float Rotation = 0;
        const float Scale = 1.0f;
        const float Depth = 0.5f;
    Enemy enemy; // ตัวแปรสำหรับศัตรู
    bool enemySpawned = false; // สถานะว่าศัตรูเกิดขึ้นหรือไม่
    const float spawnDistance = 200f; // ระยะห่างที่กำหนดในการเกิดศัตรู
    private float spawnTimer; // ตัวจับเวลาในการแสดงแอนิเมชันการเกิด
    private const float spawnDuration = 2.0f; // ระยะเวลาแอนิเมชันการเกิด
    private AnimatedTexture walkLeft;
    public Chapter0(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            this.game = game;

            // โหลดพื้นหลังและปุ่ม
            BG = game.Content.Load<Texture2D>("Cave0_1");
        
        
        // Initialize player1 and player2 for the OpeningScene
         player1 = new Player(new Vector2(120, 700), 200f, Keys.I, Keys.K, Keys.J, Keys.L, 700f, 1920, 1080);
            player2 = new Player(new Vector2(50, 700), 200f, Keys.W, Keys.S, Keys.A, Keys.D  , 700f, 1920, 1080);
        
        
      

        // Load textures for the players
        player1.LoadContent(game.Content, "AliasWalkLeft800p", "AliasWalkRight800p", "AliasIdlelLeft800p", "AliasIdleRight800p", "AliasClimbUp800p", "AliasIdlelLeft800p", "AliasIdleRight800p");
            player2.LoadContent(game.Content, "BeckerWalkLeft800p", "BeckerWalkRight800p", "BeckerIdleLeft800p", "BeckerIdleRight800p", "BeckerClimbUp", "BeckerPickaxeLeft800p", "BeckerPickaxeRight800p");

        // Load Song
        //   this.song = game.Content.Load<Song>("breeze");
        // Uncomment the following line will also loop the song
        //   MediaPlayer.IsRepeating = true;
        //  MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

        enemy = new Enemy(new Vector2(500, 700), 50f);
        enemy.LoadContent(game.Content, "WormWalkLeftt", "WormWalkRight", "WormAttackLeft", "WormAttackRight"); // โหลดเนื้อหาศัตรู
        enemySpawned = true; // เปลี่ยนสถานะการเกิดศัตรู



    }



    public override void Update(GameTime gameTime)
        {
            player1.Update(gameTime);
            player2.Update(gameTime);

            // Add your scene transition logic here if necessary
            if (player1.Position.X > 1600 && player2.Position.X > 1600 && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // Transition to the next scene
                ScreenEvent.Invoke(game.mGameplayCT0_2, new EventArgs());
                return;
            }

        /// Enemy

        enemy.Update(gameTime, player1);
        enemy.Update(gameTime, player2);

        if (Keyboard.GetState().IsKeyDown(Keys.E))
        {
            isAttacking = true;
            player2.Attack(enemy);
            isAttacking = false;
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
                enemy.Update(gameTime, player2);
                enemy.Update(gameTime, player1);
            }
        }



        base.Update(gameTime);
        }

        public override void Draw(SpriteBatch theBatch,GameTime gameTime)
        {

            
       
         theBatch.Draw(BG, new Rectangle(0, 0, 1920, 1080), Color.White);


        enemy?.Draw(theBatch); // วาดศัตรูถ้ามีอยู่

        player1.Draw(theBatch);
            player2.Draw(theBatch);

            

          
        }

    void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
    {
        // 0.0f is silent, 1.0f is full volume
        MediaPlayer.Volume = 0.4f;

    }
}

