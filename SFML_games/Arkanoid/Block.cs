using SFML.Graphics;
using SFML.System;

internal class Block
{
    public Sprite sprite;
    public int health;
    public bool isDestroyed;
    private Color originalColor;

    public Block(Texture texture, int health = 1)
    {
        sprite = new Sprite(texture);
        this.health = health;
        isDestroyed = false;
        originalColor = Color.White;
        UpdateColor();
    }

    public bool Hit()
    {
        if (isDestroyed) return false;

        health--;
        if (health <= 0)
        {
            isDestroyed = true;
            return true; // Блок уничтожен
        }

        UpdateColor();
        return false; // Блок повреждён, но не уничтожен
    }

    private void UpdateColor()
    {
        switch (health)
        {
            case 3:
                sprite.Color = Color.Red;
                break;
            case 2:
                sprite.Color = Color.Yellow;
                break;
            case 1:
                sprite.Color = Color.Green;
                break;
            default:
                sprite.Color = originalColor;
                break;
        }
    }

    public void SetPosition(Vector2f position)
    {
        sprite.Position = position;
    }
}