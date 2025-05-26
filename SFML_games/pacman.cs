using System;
using SFML.Learning;
using SFML.Graphics;
using SFML.Window;

internal class Program : Game
{
    // Player properties
    static float playerX = 300;
    static float playerY = 220;
    static float playerSpeed = 400f;
    static int playerDirection = 1;
    const float playerRadius = 32;
    static bool isGameOver = false;
    static string backgroundTexture = LoadTexture("background.png");
    static string playerTexture = LoadTexture("char_1.png");
    static string alienTexture = LoadTexture("alien.png");
    const int spriteSize = 128;
    static int highScore = 0;



    // Pickup object
    static float pickupX;
    static float pickupY;
    const float pickupRadius = 64;
    static string pickupSound = LoadSound("pickup.wav");


    // Score system
    static int score = 0;

    // Window size
    const int windowWidth = 800;
    const int windowHeight = 600;

    static void PlayerMove()
    {
        if (GetKey(Keyboard.Key.Left)) playerDirection = 0;
        if (GetKey(Keyboard.Key.Right)) playerDirection = 1;
        if (GetKey(Keyboard.Key.Up)) playerDirection = 2;
        if (GetKey(Keyboard.Key.Down)) playerDirection = 3;

        if (playerDirection == 0) playerX -= playerSpeed * DeltaTime;
        if (playerDirection == 1) playerX += playerSpeed * DeltaTime;
        if (playerDirection == 2) playerY -= playerSpeed * DeltaTime;
        if (playerDirection == 3) playerY += playerSpeed * DeltaTime;

        // Collision with window borders
        if (playerX - playerRadius < 0 || playerX + playerRadius > windowWidth ||
            playerY - playerRadius < 0 || playerY + playerRadius > windowHeight)
        {
            if (score > highScore)
            {
                highScore = score;
            }

            isGameOver = true;
        }


    }

    static void PlayerDraw()
    {
        int frameX = 0;
        int frameY = 0;

        switch (playerDirection)
        {
            case 0: frameX = 128; frameY = 0; break;     // Left
            case 1: frameX = 0; frameY = 0; break;     // Right
            case 2: frameX = 0; frameY = 128; break;   // Up
            case 3: frameX = 128; frameY = 128; break;   // Down
        }

        float drawX = playerX - spriteSize / 2f;
        float drawY = playerY - spriteSize / 2f;

        DrawSprite(playerTexture, drawX, drawY, frameX, frameY, spriteSize, spriteSize);
    }

    static void PickupDraw()
    {
        float drawX = pickupX - 64;
        float drawY = pickupY - 64;
        DrawSprite(alienTexture, drawX, drawY);
    }

    static void HandlePickup()
    {
        float dx = playerX - pickupX;
        float dy = playerY - pickupY;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        if (distance < playerRadius + pickupRadius)
        {
            score++;
            PlaySound(pickupSound);
            RelocatePickup();
        }
    }

    static void RelocatePickup()
    {
        Random rand = new Random();
        pickupX = rand.Next((int)pickupRadius, windowWidth - (int)pickupRadius);
        pickupY = rand.Next((int)pickupRadius, windowHeight - (int)pickupRadius);
    }

    static void DrawScore()
    {
        SetFillColor(255, 255, 255);
        DrawText(10, 10, "Score: " + score, 24);
        DrawText(10, 40, "Press R to restart", 16);

    }

    static void DrawGameOver()
    {
        SetFillColor(255, 0, 0);
        DrawText(300, 250, "GAME OVER", 32);

        SetFillColor(255, 255, 255);
        DrawText(280, 290, $"Score: {score}", 20);
        DrawText(260, 320, $"High Score: {highScore}", 20);
        DrawText(240, 360, "Press R to restart", 16);
    }


    static void RestartGame()
    {
        playerX = 300;
        playerY = 220;
        playerDirection = 1;
        score = 0;
        isGameOver = false;
        RelocatePickup();
    }


    static void Main()
    {
        InitWindow(windowWidth, windowHeight, "SFML Learning Game");
        SetFont("arial.ttf"); // Make sure you have an "arial.ttf" file in your project directory

        RelocatePickup(); // Initialize pickup position

        while (true)
        {
            DispatchEvents();

            // ✅ Allow restart anytime
            if (GetKeyDown(Keyboard.Key.R))
            {
                RestartGame();
            }

            if (!isGameOver)
            {
                PlayerMove();
                HandlePickup();
            }

            ClearWindow(0, 0, 0);

            DrawSprite(backgroundTexture, 0, 0); // background

            PlayerDraw();
            PickupDraw();
            DrawScore();

            if (isGameOver)
            {
                DrawGameOver();
            }

            DisplayWindow();
            Delay(1);
        }
    }
}
