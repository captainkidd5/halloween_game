
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

        Collider = new RectangleCollider(ColliderType.Dynamic,new PhysicsStuff.Primitives.Rectangle2D(Position.X,Position.Y,16,16), GetCollisionCategory(), GetCollidesWith(), new Vector2(24,80));
        
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
