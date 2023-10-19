
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HalloweenGame;

public class Player
{
    public AnimatedSprite Sprite { get; private set; }
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 0.25f;

    public Player(ContentManager content, Vector2 position)
    {
        AnimatedSprite.Animation scareAnimation = new AnimatedSprite.Animation();
        
        for (int i = 0; i < 14; ++i)
            scareAnimation.AddFrame(new AnimatedSprite.AnimationFrame(i, 500, 30, 44));

        // We're just using the ScaredSkelly texture for this for now...
        Sprite = new AnimatedSprite(content.Load<Texture2D>("sprites/ScaredSkelly"));
        Sprite.AddAnimation("scared", scareAnimation);
        Sprite.SetAnimation("scared");

        Position = position;
    }

    public void Update(GameTime gameTime)
    {
        // TODO: Apply this to the existing physics system somehow
        Position += (Globals.Input.GetVelocity() * Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        Sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, Position);
    }
}
