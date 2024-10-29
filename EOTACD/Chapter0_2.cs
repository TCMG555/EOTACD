using EOTACD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using Penumbra;


public class Chapter0_2 : screen 
{
    Game1 game;
    GraphicsDeviceManager graphics;
    Texture2D BG;

    Texture2D skeleton;
    Texture2D door;
    Texture2D doorlocker;
    Texture2D bag;
    Texture2D rock;

    Texture2D noteTexture;
    Rectangle note;
    bool isInteracting = false;

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

    public PenumbraComponent Penumbra02;


    Enemy enemy; // ตัวแปรสำหรับศัตรู
    bool enemySpawned = false; // สถานะว่าศัตรูเกิดขึ้นหรือไม่
    const float spawnDistance = 200f; // ระยะห่างที่กำหนดในการเกิดศัตรู
    private float spawnTimer; // ตัวจับเวลาในการแสดงแอนิเมชันการเกิด
    private const float spawnDuration = 2.0f; // ระยะเวลาแอนิเมชันการเกิด
    private AnimatedTexture walkLeft;
    bool isAttacking = false;



    public Chapter0_2(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
    {
        this.game = game;
        
       
        // โหลดพื้นหลังและปุ่ม
        BG = game.Content.Load<Texture2D>("Cave0_2");

        // Initialize player1 and player2 for the OpeningScene
        player1 = new Player(new Vector2(120, 700), 200f, Keys.I, Keys.K, Keys.J, Keys.L, 700f, 1920, 1080);
        player2 = new Player(new Vector2(50, 700), 200f, Keys.W, Keys.S, Keys.A, Keys.D, 700f, 1920, 1080);

       

        // Load textures for the players
        player1.LoadContent(game.Content, "AliasWalkLeft800p", "AliasWalkRight800p", "AliasIdlelLeft800p", "AliasIdleRight800p", "AliasClimbUp800p", "AliasIdlelLeft800p", "AliasIdleRight800p");
        player2.LoadContent(game.Content, "BeckerWalkLeft800p", "BeckerWalkRight800p", "BeckerIdleLeft800p", "BeckerIdleRight800p", "BeckerClimbUp800p", "BeckerPickaxeLeft800p", "BeckerPickaxeRight800p");
        
        skeleton = game.Content.Load<Texture2D>("Skeleton");
        doorlocker = game.Content.Load<Texture2D>("LockDoor");
        door = game.Content.Load<Texture2D>("DoorCave02");
        bag = game.Content.Load<Texture2D>("bag");
        rock = game.Content.Load<Texture2D>("Rock1");
        noteTexture = game.Content.Load<Texture2D>("Note0_1");

        note = new Rectangle(900,900,300,300);

        game.ChangePositionLight2();

        enemy = new Enemy(new Vector2(500, 700), 50f);

        enemy.LoadContent(game.Content, "WormWalkLeftt", "WormWalkRight", "WormAttackLeft", "WormAttackRight"); // โหลดเนื้อหาศัตรู
        enemySpawned = true; // เปลี่ยนสถานะการเกิดศัตรู
    }
    
    

    public override void Initialize()
    {
        Penumbra02.Initialize();
        game.LightSystem();

        base.Initialize(); 
    }

    public override void Update(GameTime gameTime)
    {
        player1.Update(gameTime);
        player2.Update(gameTime);

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

        if ((new Rectangle((int)player1.Position.X, (int)player1.Position.Y, 500, 500).Intersects(note) ||
             new Rectangle((int)player2.Position.X, (int)player2.Position.Y, 500, 500).Intersects(note)) &&
             Keyboard.GetState().IsKeyDown(Keys.F)||Keyboard.GetState().IsKeyDown(Keys.P))
        {
            isInteracting = true;
        }

        // Reset interaction state when players move away or after interaction
        if (!new Rectangle((int)player1.Position.X, (int)player1.Position.Y, 500, 500).Intersects(note) &&
            !new Rectangle((int)player2.Position.X, (int)player2.Position.Y, 500, 500).Intersects(note))
        {
            isInteracting = false;
        }

        // Add your scene transition logic here if necessary
        if (player1.Position.X > 1600 && player2.Position.X > 1600 && Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            // Transition to the next scene
            ScreenEvent.Invoke(game.mGameplayCT1_1, new EventArgs());
            return;
        }

        

        



        base.Update(gameTime);
    }
  
       
    public override void Draw(SpriteBatch theBatch,GameTime gameTime)
    {

      

        
        
        theBatch.Draw(BG, new Rectangle(0, 0, 1920, 1080), Color.White);

        theBatch.Draw(bag, new Rectangle(900, 900, 50, 50), Color.White);
        theBatch.Draw(door, new Rectangle(1880, 900, 50, 200), Color.White);
        theBatch.Draw(rock, new Rectangle(200, 950, 50, 50), Color.White);
        theBatch.Draw(doorlocker, new Rectangle(1900, 1000, 10, 10), Color.White);
        theBatch.Draw(skeleton, new Rectangle(1000, 800, 150, 150), Color.White);

        enemy?.Draw(theBatch); // วาดศัตรูถ้ามีอยู่

        player1.Draw(theBatch);
        player2.Draw(theBatch);

        if (isInteracting)
        {
            theBatch.Draw(noteTexture, new Rectangle(1920 / 2 - 450, 1080 / 2 - 450, 300, 300), Color.White);
        }



    }
    
}

