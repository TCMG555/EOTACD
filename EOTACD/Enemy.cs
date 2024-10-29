using EOTACD;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Enemy
{
    public Vector2 Position { get; private set; }
    private Vector2 velocity;
    private float speed;

    private AnimatedTexture walkLeft;
    private AnimatedTexture walkRight;

    private AnimatedTexture attackLeft;
    private AnimatedTexture attackRight;
    private bool isAlive;
    public Color Color { get; private set; } 
    //spawn
    private float detectionRange = 400f; // ระยะที่ตรวจจับผู้เล่น
    private float spawnTimer; // ตัวจับเวลาในการแสดงแอนิเมชันการเกิด
    private const float spawnDuration = 2.0f; // ระยะเวลาแอนิเมชันการเกิด
    private bool isSpawning; // สถานะว่ากำลังเกิดอยู่หรือไม่
    //attack
    private float attackDamage = 20f; // ความเสียหายที่ทำได้
    public float AttackRange { get; private set; } 
    private float attackTimer; // ตัวจับเวลาในการโจมตี
    private const float attackCooldown = 1.0f; // ความหน่วงเวลาในการโจมตี

    public float Health { get; private set; }
    private const float MaxHealth = 100f; // Set max health for enemy

    public Enemy(Vector2 initialPosition, float speed)
    {
        this.Position = initialPosition;
        this.speed = speed;
        this.isAlive = true;
        this.isSpawning = false;
        AttackRange = 50f; // Set an appropriate attack range
        Health = MaxHealth; // Set initial health
        Color = Color.White; // Set the default color
    }

    public void LoadContent(ContentManager content, string walkLeftAsset, string walkRightAsset, string attackLeftAsset, string attackRightAsset)
    {
        walkLeft = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
        walkLeft.Load(content, walkLeftAsset, 6, 1, 15); // ตรวจสอบไฟล์ walkLeftAsset

        walkRight = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
        walkRight.Load(content, walkRightAsset, 6, 1, 15); // ตรวจสอบไฟล์ walkRightAsset

        attackLeft = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
        attackLeft.Load(content, attackLeftAsset, 10, 1, 15); // ตรวจสอบไฟล์ attackLeftAsset

        attackRight = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
        attackRight.Load(content, attackRightAsset, 10, 1, 15); // ตรวจสอบไฟล์ attackRightAsset

      
    }

    public void Update(GameTime gameTime, Player player)
    {
        if (!isAlive) return;

        // ตรวจสอบว่าศัตรูกำลังเกิดอยู่หรือไม่
        if (isSpawning)
        {
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds; // เพิ่มตัวจับเวลา
            if (spawnTimer >= spawnDuration)
            {
                isSpawning = false; // จบการเกิด
                detectionRange = 1000f; // ปรับระยะตรวจจับ
            }
            return; // หยุดการอัปเดตถ้ายังเกิดอยู่
        }

        float distanceToPlayer = Vector2.Distance(Position, player.Position);

        if (distanceToPlayer < detectionRange && !isSpawning)
        {
            // เดินเข้าหาผู้เล่น
            if (player.Position.X < Position.X)
            {
                velocity.X = -speed; // เดินซ้าย
                walkLeft.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (player.Position.X > Position.X)
            {
                velocity.X = speed; // เดินขวา
                walkRight.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;


        }
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        Color = Color.Red; // Change color to blue

        if (Health <= 0)
        {
            Health = 0; // Ensure health doesn't go below zero
            isAlive = false; // Change the state of the enemy
            
        }
    }

    private void Die()
    {
        isAlive = false; // Set enemy to dead

        // Optionally play death animation or sound
    }

    public void Attack(Player player)
    {
        if (Vector2.Distance(Position, player.Position) < AttackRange)
        {
            player.TakeDamage(attackDamage);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!isAlive) return;

        // ถ้ายังอยู่ในระหว่างการเกิดให้วาดแอนิเมชันการเกิด
        if (isSpawning)
        {
            walkLeft.DrawFrame(spriteBatch, Position);
            return; // หยุดการวาดถ้ายังเกิด
        }

        if (velocity.X < 0 && isSpawning == false)
        {
            walkLeft.DrawFrame(spriteBatch, Position);
        }
        else if (velocity.X > 0 && isSpawning == false )
        {
            walkRight.DrawFrame(spriteBatch, Position);
        }
        
        
    }

    public void StartSpawn()
    {
        isSpawning = true; // เริ่มต้นการเกิด
        spawnTimer = 0f; // รีเซ็ตตัวจับเวลา
    }

    public void TakeDamage()
    {
        if (Health < 0)
        {
            Die();
        }
        isAlive = false; // ตั้งค่าศัตรูให้ตาย
        
    }
}
