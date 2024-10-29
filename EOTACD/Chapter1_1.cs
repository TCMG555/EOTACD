using EOTACD;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;


public class Chapter1_1 : screen 
{
    Game1 game;
    Player player1;
    Player player2;
    Camera camera1;
    Camera camera2;
    GraphicsDeviceManager graphics;

    Rectangle player1Rectangle;
    Rectangle player2Rectangle;
    Rectangle barrRectangle;
    Color barrColor = Color.Transparent;
   public Rectangle ladderLRectangle;
  public  Color ladderLColor = Color.Transparent;
   public Rectangle ladderRRectangle;
   public Color ladderRColor = Color.Transparent;

    private bool isInteractingWithPuzzle = false;
    private bool isPuzzleSolved = false;
    private bool isPuzzleActive = false;  // To track if the puzzle is currently displayed
    private Rectangle interactionPoint = new Rectangle(100, 250, 50, 50);  // Define the interaction area
    Texture2D popupPuzzle;
    Texture2D puzzleButton;
    Texture2D bgLab;
    private int[] targetNumbers = { 0, 3, 4, 8 }; // ตัวเลขที่ผู้เล่นต้องเรียง
    private Texture2D[] numberTextures = new Texture2D[10];
    Rectangle[] puzzleSlots = new Rectangle[4];
    int[] currentNumbers = new int[4];
    private int currentInputIndex = 0; // ดัชนีสำหรับบันทึกตัวเลขที่ผู้เล่นกรอก
    private int[] correctAnswer = { 0, 3, 4, 8 }; // เปลี่ยนตามคำตอบที่ตั้งไว้
    private int[] playerAnswer = new int[4];
    private const int NumSize = 64;

    public MouseState previousMouse, currentMouse;
    public Vector2 mousePosition;
    Texture2D notecom,note1;
    private KeyboardState previousKeyboardState;

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
    public bool wall1 = false;
    const float Rotation = 0;
    const float Scale = 1.0f;
    const float Depth = 0.5f;

    bool isInteracting = false, isInteracting2 = false;
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

    Rectangle com;


    public Chapter1_1(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
    {
        this.game = game;


        com = new Rectangle(120, 700, 300, 300);
        
        // Initialize player1 and player2 for Chapter1_1 with different starting positions
        player1 = new Player(new Vector2(120, 720), 200f, Keys.I, Keys.K, Keys.J, Keys.L, 720f, 1920 , 1080);
        player2 = new Player(new Vector2(50, 720), 200f, Keys.W, Keys.S, Keys.A, Keys.D, 720f, 1920 , 1080);
        notecom = game.Content.Load<Texture2D>("NoteScreen-1");
        note1 = game.Content.Load<Texture2D>("Note2");
        // Initialize cameras for the split-screen
        camera1 = new Camera();
        camera2 = new Camera();

        // Load textures for the players
        player1.LoadContent(game.Content, "AliasWalkLeft800p", "AliasWalkRight800p", "AliasIdlelLeft800p", "AliasIdleRight800p" , "AliasClimbUp800p", "AliasIdlelLeft800p", "AliasIdleRight800p");
        player2.LoadContent(game.Content, "BeckerWalkLeft800p", "BeckerWalkRight800p", "BeckerIdleLeft800p", "BeckerIdleRight800p", "BeckerClimbUp800p", "BeckerPickaxeLeft800p", "BeckerPickaxeRight800p");
        

        bgLab = game.Content.Load<Texture2D>("Chapter1_01");


        enemy = new Enemy(new Vector2(520, 750), 100f);// สร้างศัตรูที่ตำแหน่งนี้
        enemy.LoadContent(game.Content, "WormWalkLeftt", "WormWalkRight", "WormAttackLeft", "WormAttackRight"); // โหลดเนื้อหาศัตรู
        enemySpawned = true; // เปลี่ยนสถานะการเกิดศัตรู

        popupPuzzle = game.Content.Load<Texture2D>("popupPuzzleChapter1");
        puzzleButton = game.Content.Load<Texture2D>("popupPuzzleChapterOkButton");

        
        
            for (int i = 0; i < 10; i++)
            {
                numberTextures[i] = game.Content.Load<Texture2D>($"{i}"); // Ensure your texture names match.
            }
        

        for (int i = 0; i < puzzleSlots.Length; i++)
        {
            puzzleSlots[i] = new Rectangle(200 + (i * 70), 320, 50, 50); // Position the slots with some spacing
            currentNumbers[i] = 0; // Initialize with no number selected
        }

        // Initialize button rectangle
        Rectangle puzzleButtonRect = new Rectangle(600, 200, 100, 50); // Adjust position and size as needed

       

        barrRectangle = new Rectangle(1100, 450, 10, 500);

        ladderLRectangle = new Rectangle(1050, 350, 120, 425);
        ladderRRectangle = new Rectangle(100, 250, 10, 425);
    }

    public override void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        currentMouse = Mouse.GetState();
        mousePosition = new Vector2(currentMouse.X, currentMouse.Y);

        if (isInteractingWithPuzzle)
        {

            // เลื่อนตัวเลขขึ้นลงต่อการกดทีละหนึ่ง
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                currentNumbers[currentInputIndex]++;
                if (currentNumbers[currentInputIndex] > 9) currentNumbers[currentInputIndex] = 1;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                currentNumbers[currentInputIndex]--;
                if (currentNumbers[currentInputIndex] < 1) currentNumbers[currentInputIndex] = 9;
            }

            // เปลี่ยนช่องการเลือก (เลื่อนไปซ้ายขวา)
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                currentInputIndex = (currentInputIndex - 1 + puzzleSlots.Length) % puzzleSlots.Length;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                currentInputIndex = (currentInputIndex + 1) % puzzleSlots.Length;
            }

            // ตรวจสอบการกดยืนยัน
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                ConfirmPuzzleInput();
                isInteractingWithPuzzle = false; // ปิดการ interact เมื่อกดยืนยัน
            }
        }
        else
        {
            // อัปเดตผู้เล่นตามปกติหากไม่ได้ interact กับปริศนา
            player1.Update(gameTime);
            player2.Update(gameTime);
        }


        // Check boundaries for player1 (left boundary)
        if (player1.Position.X < 0)
        {
            player1.SetPosition(new Vector2(0, player1.Position.Y)); // Prevent going past the left edge
        }

        // Check boundaries for player2 (right boundary)
        if (player2.Position.X  > game.GraphicsDevice.Viewport.Width)
        {
            player2.SetPosition(new Vector2 (player2.Position.X, player2.Position.Y)); // Prevent going past the right edge
        }

        

        player1Rectangle = new Rectangle((int)player1.Position.X, (int)player1.Position.Y, 160, 160); // ขนาดของผู้เล่น
        player2Rectangle = new Rectangle((int)player2.Position.X, (int)player2.Position.Y, 160, 160); // ขนาดของผู้เล่น

        CheckCollision(player1);
        CheckCollision(player2);

        // Handle player attacks
        if (Keyboard.GetState().IsKeyDown(Keys.E)) 
        {
            isAttacking = true;
            player2.Attack(enemy);
            isAttacking = false;
        }

        if (!isPuzzleActive && !isPuzzleSolved &&
        (player1Rectangle.Intersects(interactionPoint) || player2Rectangle.Intersects(interactionPoint)) &&
        Keyboard.GetState().IsKeyDown(Keys.Enter))  // Assuming 'Enter' is the interact key
        {
            isPuzzleActive = true;  // Activate the puzzle popup
        }

        // Handle puzzle input if active
        if (isPuzzleActive)
        {
            HandlePuzzleInput(keyboardState);
        }
        else
        {
            // ตรวจสอบการเคลื่อนไหวของผู้เล่นเท่านั้นเมื่อไม่ได้แก้ปริศนา
            HandlePlayerMovement(keyboardState);
        }

        // Update cameras
        camera1.Update(player1.Position);
        camera2.Update(player2.Position);

        if (player1.Position.X > 1600 && player2.Position.X > 1600 && Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            // Transition to the next scene
            ScreenEvent.Invoke(game.mGameplayCT1_2, new EventArgs());
            return;
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

        if ((new Rectangle((int)player1.Position.X, (int)player1.Position.Y, 500, 500).Intersects(com) ||
             new Rectangle((int)player2.Position.X, (int)player2.Position.Y, 500, 500).Intersects(com)) &&
             Keyboard.GetState().IsKeyDown(Keys.F) || Keyboard.GetState().IsKeyDown(Keys.P))
        {
            isInteracting = true;
        }

        // Reset interaction state when players move away or after interaction
        if (!new Rectangle((int)player1.Position.X, (int)player1.Position.Y, 500, 500).Intersects(com) &&
            !new Rectangle((int)player2.Position.X, (int)player2.Position.Y, 500, 500).Intersects(com))
        {
            isInteracting = false;
        }
       
       

        previousKeyboardState = keyboardState;
        previousMouse = currentMouse; // อัปเดตสถานะเมาส์
        game.c = wall1;
        base.Update(gameTime);
    }
    private void HandlePlayerMovement(KeyboardState keyboardState)
    {
        
    }

    public void ShowPuzzlePopup()
    {
        isInteractingWithPuzzle = true;
        currentInputIndex = 0; // รีเซ็ตช่องการเลือก
    }
    private void ConfirmPuzzleInput()
    {
        bool isCorrect = true;
        for (int i = 0; i < targetNumbers.Length; i++)
        {
            if (currentNumbers[i] != targetNumbers[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            isPuzzleSolved = true;
            isPuzzleActive = false;
            // Additional actions for solving the puzzle, like opening a door or displaying a message
        }
        else
        {
            // Feedback for incorrect solution (e.g., flashing the slots, resetting numbers, etc.)
            for (int i = 0; i < currentNumbers.Length; i++)
            {
                currentNumbers[i] = 0; // Reset or provide some feedback
            }
            currentInputIndex = 0; // Reset current input index
        }
    }


    public void CheckCollision(Player player)
    {
        // สร้าง Rectangle สำหรับผู้เล่น
        Rectangle playerRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, 160, 160); // ขนาดของผู้เล่น

        if (playerRectangle.Intersects(ladderLRectangle))
        {

            player.StartClimbing(); // เริ่มแอนิเมชันปีน
        }
        else
        {
            player.StopClimbing(); // หยุดปีนถ้าไม่ชนบันได
        }



        if (playerRectangle.Intersects(barrRectangle))
        {
            // ตรวจสอบว่าผู้เล่นอยู่ด้านซ้ายหรือขวาของ barrier
            if (player.Position.X < barrRectangle.X) // ผู้เล่นอยู่ด้านซ้ายของ barrier
            {
                player.Position = new Vector2(barrRectangle.X - playerRectangle.Width - 10, player.Position.Y); // ถอยออก 10 หน่วย
                player.StartClimbing();
            }
            else if (player.Position.X + playerRectangle.Width > barrRectangle.Right) // ผู้เล่นอยู่ด้านขวาของ barrier
            {
                player.Position = new Vector2(barrRectangle.Right + 10, player.Position.Y); // ถอยออก 10 หน่วย
                player.StartClimbing();
            }
        }

        // ตรวจสอบการชนกับบันได
        
    }

    private void HandlePuzzleInput(KeyboardState currentState)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
        {
            CheckAnswer();
        }
        // Logic for handling up/down arrow keys to change numbers and navigating slots
        if (currentState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
        {
            currentNumbers[currentInputIndex] = (currentNumbers[currentInputIndex] + 1) % 10;  // Increment with wrap-around
        }
        if (currentState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
        {
            currentNumbers[currentInputIndex] = (currentNumbers[currentInputIndex] + 9) % 10;  // Decrement with wrap-around
        }
        if (currentState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left) && currentInputIndex > 0)
        {
            currentInputIndex--;  // Move to previous slot
        }
        if (currentState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right) && currentInputIndex < puzzleSlots.Length - 1)
        {
            currentInputIndex++;  // Move to next slot
        }
    }
    private void CheckAnswer()
{
    bool isCorrect = true;

    for (int i = 0; i < correctAnswer.Length; i++)
    {
        if (playerAnswer[i] != correctAnswer[i])
        {
            isCorrect = false;
            break;
        }
    }

    if (isCorrect)
    {
        // คำตอบถูกต้อง
        Console.WriteLine("คำตอบถูกต้อง!");
        
        // เปลี่ยนสกรีนไปยัง mGameplayCT1_2
        ScreenEvent.Invoke(game.mGameplayCT1_2, new EventArgs());
        return;
    }
    else
    {
        // คำตอบผิด
        Console.WriteLine("คำตอบผิด! ลองอีกครั้ง.");
        // อาจจะคืนค่าช่องให้ว่างหรือรีเซ็ตคำตอบ
    }
}
    private void CheckPuzzleSolution()
    {
        bool isCorrect = true;
        for (int i = 0; i < targetNumbers.Length; i++)
        {
            if (currentNumbers[i] != targetNumbers[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            isPuzzleSolved = true;  // Set puzzle as solved
            isPuzzleActive = false;  // Close the puzzle popup
                                     // Additional actions can be added here (e.g., open a door, change the game state)
        }
    }

    public override void Draw(SpriteBatch theBatch,GameTime gameTime)
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


        // วาดศัตรู (ถ้ามี)
      enemy?.Draw(theBatch); // วาดศัตรูถ้ามีอยู่

        Texture2D rectangleTexture = new Texture2D(game.GraphicsDevice, 1, 1);
        rectangleTexture.SetData(new Color[] { Color.White });

        theBatch.Draw(rectangleTexture, barrRectangle, barrColor);
        theBatch.Draw(rectangleTexture, ladderLRectangle, ladderLColor);
        theBatch.Draw(rectangleTexture, ladderRRectangle, ladderRColor);
        // Reset viewport to the full screen
        //  game.GraphicsDevice.Viewport = new Viewport(0, 0, 1920, 1080);

        // Draw puzzle popup if active
        
            if (isPuzzleActive)
            {
                theBatch.Draw(popupPuzzle, new Vector2(150, 150), Color.White); // Draw the puzzle background
                for (int i = 0; i < puzzleSlots.Length; i++)
                {
                    int number = currentNumbers[i];
                    theBatch.Draw(numberTextures[number], puzzleSlots[i], Color.White); // Use the number texture
                }
                theBatch.Draw(puzzleButton, new Rectangle(600, 200, 100, 50), Color.White); // Draw the confirm button
            }
        
        if (isInteracting)
        {
            theBatch.Draw(notecom, new Rectangle(1920/2-450,1080/2-450, 900, 900), Color.White);
        }
        




    }
}
