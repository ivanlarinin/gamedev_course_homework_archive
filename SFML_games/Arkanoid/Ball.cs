using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

internal class Ball
{
    public Sprite sprite;
    private float speed;
    private Vector2f direction;
    private bool isMoving;

    public Ball(Texture texture)
    {
        sprite = new Sprite(texture);
        isMoving = false;
    }

    public void Start(float speed, Vector2f direction)
    {
        if (this.speed != 0 && isMoving) return;
        this.speed = speed;
        this.direction = direction;
        isMoving = true;
    }

    public void Move(Vector2u windowSize, float deltaTime)
    {
        if (!isMoving) return;

        // Перемещение мяча с учетом времени
        Vector2f newPosition = sprite.Position + direction * speed * deltaTime;
        sprite.Position = newPosition;

        // Проверка столкновений со стенами
        FloatRect ballBounds = sprite.GetGlobalBounds();

        // Левая и правая стены
        if (ballBounds.Left <= 0 || ballBounds.Left + ballBounds.Width >= windowSize.X)
        {
            direction = new Vector2f(-direction.X, direction.Y);
        }

        // Верхняя стена
        if (ballBounds.Top <= 0)
        {
            direction = new Vector2f(direction.X, -direction.Y);
        }

        // Нижняя стена (мяч упал)
        if (ballBounds.Top >= windowSize.Y)
        {
            Reset();
        }
    }

    public bool CheckCollision(Sprite target)
    {
        if (!isMoving) return false;

        FloatRect ballBounds = sprite.GetGlobalBounds();
        FloatRect targetBounds = target.GetGlobalBounds();

        if (ballBounds.Intersects(targetBounds))
        {
            // Определяем с какой стороны произошло столкновение
            Vector2f ballCenter = new Vector2f(
                ballBounds.Left + ballBounds.Width / 2,
                ballBounds.Top + ballBounds.Height / 2
            );
            Vector2f targetCenter = new Vector2f(
                targetBounds.Left + targetBounds.Width / 2,
                targetBounds.Top + targetBounds.Height / 2
            );

            float deltaX = ballCenter.X - targetCenter.X;
            float deltaY = ballCenter.Y - targetCenter.Y;

            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                // Горизонтальное столкновение
                direction = new Vector2f(-direction.X, direction.Y);
            }
            else
            {
                // Вертикальное столкновение
                direction = new Vector2f(direction.X, -direction.Y);
            }

            return true;
        }
        return false;
    }

    public bool CheckPaddleCollision(Sprite paddle)
    {
        if (!isMoving) return false;

        FloatRect ballBounds = sprite.GetGlobalBounds();
        FloatRect paddleBounds = paddle.GetGlobalBounds();

        if (ballBounds.Intersects(paddleBounds))
        {
            // Вычисляем угол отражения на основе места удара по платформе
            float ballCenterX = ballBounds.Left + ballBounds.Width / 2;
            float paddleCenterX = paddleBounds.Left + paddleBounds.Width / 2;
            float hitPosition = (ballCenterX - paddleCenterX) / (paddleBounds.Width / 2);

            // Ограничиваем угол отражения
            hitPosition = Math.Max(-0.8f, Math.Min(0.8f, hitPosition));

            direction = new Vector2f(hitPosition, -Math.Abs(direction.Y));

            // Нормализуем вектор направления
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            direction = new Vector2f(direction.X / length, direction.Y / length);

            return true;
        }
        return false;
    }

    public void Reset()
    {
        isMoving = false;
        speed = 0;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void SetPosition(Vector2f position)
    {
        sprite.Position = position;
    }
}