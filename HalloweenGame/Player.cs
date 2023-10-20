
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using HalloweenGame.PhysicsStuff;
using System;

namespace HalloweenGame;

public class Player : Entity
{
    public AnimatedSprite Sprite { get; private set; }
    public float Speed { get; set; } = 6f;
    public Collider Collider { get; private set; }
    public Player(ContentManager content, Vector2 position) : base()
    {
        AnimatedSprite.Animation scareAnimation = new AnimatedSprite.Animation();
        
        for (int i = 0; i < 14; ++i)
            scareAnimation.AddFrame(new AnimatedSprite.AnimationFrame(i, 500, 30, 44));

        // We're just using the ScaredSkelly texture for this for now...
        Sprite = new AnimatedSprite(content.Load<Texture2D>("sprites/ScaredSkelly"));
        Sprite.AddAnimation("scared", scareAnimation);
        Sprite.SetAnimation("scared");

        Position = position;

        Collider = new CircleCollider(ColliderType.Dynamic,Position,8, GetCollisionCategory(), GetCollidesWith(),new Vector2(16,80));
        
        Collider.UserData = this;

        Collider.OnCollidesAction = (collider) =>
        {
            if (collider.CollisionCategories == CollisionCategory.Solid)
            {
                Console.WriteLine("test");
            }
        };
        AddComponent(Collider);
    }
    public CollisionCategory GetCollisionCategory()
    {
        return CollisionCategory.Player;
    }
    public CollisionCategory GetCollidesWith()
    {
        return CollisionCategory.Solid | CollisionCategory.Hook | CollisionCategory.WorldItem;
    }
    public override void Update(GameTime gameTime)
    {
        // TODO: Apply this to the existing physics system somehow
        Vector2 velocity = (Globals.Input.GetVelocity() * Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        if(Globals.Input.Keyboard.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Space))
            velocity.Y = -5000f;
        Collider.SetVelocity(velocity);
        Sprite.Update(gameTime);
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, Position);

        base.Draw(spriteBatch);
    }
}
