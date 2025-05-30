using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

class Program
{
    static Texture ballTexture;
    static Texture stickTexture;
    static Texture blockTexture;
    static Font font;

    static Sprite stick;
    static List<Block> blocks;
    static Ball ball;

    static int level = 1;
    static int lives = 3;
    static int score = 0;
    static bool gameStarted = false;
    static bool gameOver = false;
    static bool levelComplete = false;

    const int WINDOW_WIDTH = 800;
    const int WINDOW_HEIGHT = 600;

    public static void LoadTextures()
    {
        ballTexture = new Texture("Ball.png");
        stickTexture = new Texture("Stick.png");
        blockTexture = new Texture("Block.png");
    }

    public static void InitializeGame()
    {
        ball = new Ball(ballTexture);
        stick = new Sprite(stickTexture);
        blocks = new List<Block>();

        SetupLevel();
    }

    public static void SetupLevel()
    {
        blocks.Clear();

        // Количество блоков зависит от уровня
        int rows = Math.Min(5 + level, 10);
        int cols = Math.Min(8 + level, 12);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                // Пропускаем некоторые блоки для создания узоров
                if ((x + y) % 3 == 0 && level > 2) continue;

                // Прочность блоков зависит от уровня и позиции
                int health = 1;
                if (level > 1 && y < 2) health = 2;
                if (level > 3 && y == 0) health = 3;

                Block block = new Block(blockTexture, health);
                float blockWidth = blockTexture.Size.X;
                float blockHeight = blockTexture.Size.Y;

                float startX = (WINDOW_WIDTH - cols * (blockWidth + 5)) / 2;
                block.SetPosition(new Vector2f(
                    startX + x * (blockWidth + 5),
                    50 + y * (blockHeight + 5)
                ));

                blocks.Add(block);
            }
        }

        // Сброс позиций
        stick.Position = new Vector2f(WINDOW_WIDTH / 2 - stickTexture.Size.X / 2, WINDOW_HEIGHT - 80);
        ball.SetPosition(new Vector2f(WINDOW_WIDTH / 2 - ballTexture.Size.X / 2, WINDOW_HEIGHT - 100));
        ball.Reset();

        gameStarted = false;
        levelComplete = false;
    }

    public static void Update(RenderWindow window, float deltaTime)
    {
        // Управление платформой
        if (Mouse.GetPosition(window).X >= 0 && Mouse.GetPosition(window).X <= WINDOW_WIDTH)
        {
            float newX = Mouse.GetPosition(window).X - stickTexture.Size.X / 2;
            newX = Math.Max(0, Math.Min(WINDOW_WIDTH - stickTexture.Size.X, newX));
            stick.Position = new Vector2f(newX, stick.Position.Y);
        }

        // Запуск мяча
        if (!ball.IsMoving())
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left) || Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                ball.Start(500f, new Vector2f(0.3f, -1f));
                gameStarted = true;
            }
            else
            {
                // Мяч следует за платформой до запуска
                ball.SetPosition(new Vector2f(stick.Position.X + stickTexture.Size.X / 2 - ballTexture.Size.X / 2, stick.Position.Y - ballTexture.Size.Y));
            }
        }

        if (ball.IsMoving())
        {
            ball.Move(new Vector2u((uint)WINDOW_WIDTH, (uint)WINDOW_HEIGHT), deltaTime);

            // Проверка столкновения с платформой
            ball.CheckPaddleCollision(stick);

            // Проверка столкновений с блоками
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                if (!blocks[i].isDestroyed && ball.CheckCollision(blocks[i].sprite))
                {
                    if (blocks[i].Hit())
                    {
                        score += 10 * level;
                        blocks.RemoveAt(i);
                    }
                    else
                    {
                        score += 5 * level;
                    }
                }
            }

            // Проверка падения мяча
            if (ball.sprite.Position.Y > WINDOW_HEIGHT)
            {
                lives--;
                ball.Reset();
                gameStarted = false;

                if (lives <= 0)
                {
                    gameOver = true;
                }
            }
        }

        // Проверка завершения уровня
        if (blocks.Count == 0 && !levelComplete)
        {
            levelComplete = true;
            level++;
            lives++; // Бонусная жизнь за прохождение уровня
            SetupLevel();
        }

        // Перезапуск игры
        if (gameOver && Keyboard.IsKeyPressed(Keyboard.Key.R))
        {
            level = 1;
            lives = 3;
            score = 0;
            gameOver = false;
            SetupLevel();
        }
    }

    public static void Render(RenderWindow window)
    {
        window.Clear(Color.Black);

        // Отрисовка игровых объектов
        if (!gameOver)
        {
            window.Draw(ball.sprite);
            window.Draw(stick);

            foreach (var block in blocks)
            {
                if (!block.isDestroyed)
                {
                    window.Draw(block.sprite);
                }
            }
        }

        // Отрисовка UI
        try
        {
            if (font != null)
            {
                Text scoreText = new Text($"Score: {score}", font, 20);
                scoreText.Position = new Vector2f(10, 10);
                scoreText.FillColor = Color.White;
                window.Draw(scoreText);

                Text livesText = new Text($"Lives: {lives}", font, 20);
                livesText.Position = new Vector2f(10, 35);
                livesText.FillColor = Color.White;
                window.Draw(livesText);

                Text levelText = new Text($"Level: {level}", font, 20);
                levelText.Position = new Vector2f(10, 60);
                levelText.FillColor = Color.White;
                window.Draw(levelText);

                if (gameOver)
                {
                    Text gameOverText = new Text("GAME OVER! Press R to restart", font, 30);
                    gameOverText.Position = new Vector2f(WINDOW_WIDTH / 2 - 200, WINDOW_HEIGHT / 2);
                    gameOverText.FillColor = Color.Red;
                    window.Draw(gameOverText);
                }
                else if (!ball.IsMoving())
                {
                    Text startText = new Text("Click or press SPACE to start", font, 20);
                    startText.Position = new Vector2f(WINDOW_WIDTH / 2 - 150, WINDOW_HEIGHT / 2 + 100);
                    startText.FillColor = Color.Yellow;
                    window.Draw(startText);
                }

                if (levelComplete && blocks.Count == 0)
                {
                    Text levelCompleteText = new Text($"Level {level - 1} Complete!", font, 25);
                    levelCompleteText.Position = new Vector2f(WINDOW_WIDTH / 2 - 100, WINDOW_HEIGHT / 2 - 50);
                    levelCompleteText.FillColor = Color.Green;
                    window.Draw(levelCompleteText);
                }
            }
        }
        catch
        {
            // Если шрифт не загружен, пропускаем текст
        }

        window.Display();
    }

    static void Main()
    {
        RenderWindow window = new RenderWindow(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Arkanoid Game");
        window.Closed += (sender, e) => window.Close();

        // Загрузка ресурсов
        LoadTextures();

        try
        {
            font = new Font("arial.ttf"); // Попытка загрузить шрифт
        }
        catch
        {
            font = null; // Если шрифт не найден, продолжаем без текста
        }

        InitializeGame();

        Clock clock = new Clock();
        while (window.IsOpen)
        {
            window.DispatchEvents();

            float deltaTime = clock.Restart().AsSeconds();
            Update(window, deltaTime);
            Render(window);
        }
    }
}