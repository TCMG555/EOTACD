using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Net.Http.Headers;
using Penumbra;


namespace EOTACD
{
    public class Player : Game1
    {
        private KeyboardState _keyboardState;
        private KeyboardState _previousState;

        public Vector2 Position { get; set; }
        public Vector2 velocity;
        public float speed;
        public float Health { get; private set; }
        private const float MaxHealth = 100f;
        public Spotlight spotA;
        private Keys upKey;
        private Keys downKey;
        private Keys leftKey;
        private Keys rightKey;

        private const int targetWidth = 160;
        private const int targetHeight = 160;

        // Add AnimatedTexture fields
        private AnimatedTexture walkLeft;
        private AnimatedTexture walkRight;
        private AnimatedTexture idleLeft;
        private AnimatedTexture idleRight;
        private AnimatedTexture attackLeft;
        private AnimatedTexture attackRight;
        private AnimatedTexture climb;

        public Vector2 LastPosition;

        // กำหนดทิศทางการหันหน้าของผู้เล่น (true ถ้าหันขวา)
        private bool facingRight = true;
        private bool isAttacking = false;
        private bool isClimbing = false; // Track if the player is currently climbing
        public bool isOnLadder = false; // Track if the player is on the ladder

        private float platformY; // ระดับความสูงของแพลตฟอร์มที่ต้องการจำกัด
        private int screenWidth;
        private int screenHeight;


        public Player(Vector2 initialPosition, float speed, Keys upKey, Keys downKey, Keys leftKey, Keys rightKey, float platformY, int screenWidth, int screenHeight)
        {
            Position = initialPosition;
            LastPosition = initialPosition;
            this.speed = speed;
            this.upKey = upKey;
            this.downKey = downKey;
            this.leftKey = leftKey;
            this.rightKey = rightKey;
            this.platformY = platformY;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            Health = MaxHealth; // Set initial health
        }

        // Method to load the animations
        public void LoadContent(ContentManager content, string walkLeftAsset, string walkRightAsset, string idleLeftAsset, string idleRightAsset, string climbAsset, string attackLeftAsset, string attackRightAsset)
        {
            walkLeft = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
            walkLeft.Load(content, walkLeftAsset, 10, 1, 10);

            walkRight = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
            walkRight.Load(content, walkRightAsset, 10, 1, 10);

            idleLeft = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
            idleLeft.Load(content, idleLeftAsset, 5, 1, 10);

            idleRight = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
            idleRight.Load(content, idleRightAsset, 5, 1, 10);

            climb = new AnimatedTexture(Vector2.Zero, 0 , 1.0f, 0.5f);
            climb.Load(content, climbAsset, 3, 1, 10);

            attackLeft = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
            attackLeft.Load(content, attackLeftAsset, 5, 1, 10);

            attackRight = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
            attackRight.Load(content, attackRightAsset, 5, 1, 10);

            spotA = new Spotlight()
            {
                Position = Vector2.Zero,
                Rotation = MathHelper.ToRadians(180f),
                Radius = 2f,
                ConeDecay = 2f,
                Color = Color.White,
                Intensity = 0.8f,
                Scale = new Vector2(800, 800)
            };
        }

        public void Update(GameTime gameTime)
        {
            HandleInput();
            UpdatePosition(gameTime);
            UpdateAnimation(gameTime);
            LastPosition = Position;

            // Reset isAttacking once the attack animation finishes
            if (isAttacking)
            {
                if ((facingRight && attackRight.IsAnimationComplete()) || (!facingRight && attackLeft.IsAnimationComplete()))
                {
                    isAttacking = false;
                }
            }

        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition; // การปรับตำแหน่งที่นี่
        }

        public void Attack(Enemy enemy)
        {
            if (Vector2.Distance(Position, enemy.Position) < enemy.AttackRange || Vector2.Distance(Position, enemy.Position) > enemy.AttackRange) // Check if enemy is in range
            {
                enemy.TakeDamage(3); // Damage amount
            }
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0; // Ensure health doesn't go below zero
        }

        private void HandleInput()
        {
            velocity = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState();

            // Climbing logic
            if (isOnLadder)
            {
                if (keyboardState.IsKeyDown(upKey))
                {
                    velocity.Y -= speed; // Move up the ladder
                    climb.Play();
                }
                else if (keyboardState.IsKeyDown(downKey))
                {
                    velocity.Y += speed; // Move down the ladder
                    climb.Play();
                }
            }
            else
            {
                // Existing movement code
                if (keyboardState.IsKeyDown(upKey) && Position.Y > platformY)
                {
                    velocity.Y -= speed;
                }
                if (keyboardState.IsKeyDown(downKey) && Position.Y < screenHeight - targetHeight)
                {
                    velocity.Y += speed;

                    if (Position.Y > platformY)
                    {
                        velocity.Y = 0;
                    }
                }
                if (keyboardState.IsKeyDown(leftKey) && Position.X > 0)
                {
                    velocity.X -= speed;    
                    facingRight = false;
                }
                if (keyboardState.IsKeyDown(rightKey) && Position.X < screenWidth - targetWidth)
                {
                    velocity.X += speed;
                    facingRight = true;
                }
            }

            if (keyboardState.IsKeyDown(Keys.E) && !isAttacking)
            {
                isAttacking = true;
                if (facingRight)
                {
                    attackRight.Play();
                }
                else
                {
                    attackLeft.Play();
                }
            }
          

        }

        public void StartClimbing()
        {
            isOnLadder = true; // Player is on the ladder
        }

        // Method to stop climbing
        public void StopClimbing()
        {
            isOnLadder = false; // Player is not on the ladder
        }

        public void UpdatePosition(GameTime gameTime)
        {
            

            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

          
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (isOnLadder)
            {

                climb.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

                return;
            }
            if (isAttacking)
            {
                if (facingRight)
                {
                    attackRight.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                    
                }
                else
                {
                    attackLeft.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                   
                }
                return; // Exit early to skip other animations
            }
            if (velocity != Vector2.Zero)
            {
                if (facingRight)
                {
                    walkRight.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    walkLeft.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
            else 
            {
                if (facingRight)
                {
                    idleRight.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    idleLeft.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
            

           
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (isOnLadder)
            {

                climb.DrawFrame(spriteBatch, Position);

                return;
            }
            if (isAttacking)
            {
                if (facingRight)
                {
                    attackRight.DrawFrame(spriteBatch, Position);
                }
                else
                {
                    attackLeft.DrawFrame(spriteBatch, Position);
                }
                return; // Exit early to skip other drawings
            }

            if (velocity != Vector2.Zero)
            {
                if (facingRight)
                {
                    walkRight.DrawFrame(spriteBatch, Position);
                }
                else
                {
                    walkLeft.DrawFrame(spriteBatch, Position);
                }
            }
            else
            {
                if (facingRight)
                {
                    idleRight.DrawFrame(spriteBatch, Position);
                }
                else
                {
                    idleLeft.DrawFrame(spriteBatch, Position);
                }
            }
            

        }
    }
}

