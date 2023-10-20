using HalloweenGame.PhysicsStuff.Primitives;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.PhysicsStuff
{
    internal class RectangleCollider : Collider
    {
        public RectangleCollider(ColliderType colliderType, Rectangle2D rect, CollisionCategory collisionCategory, CollisionCategory collidesWith, Vector2 offSet) :
            base(colliderType, collisionCategory, collidesWith, offSet)
        {
            Rect = rect;
        }

        public override void Update(GameTime gameTime)
        {
            Entity.Position = Rect.Center;
            base.Update(gameTime);
            Rect.Update(Rect.Position + Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            //  Rect.Update(Entity.Position + OffSet);
        }

        protected override void ReactToCollision(Collider other)
        {
            base.ReactToCollision(other);
            //sensors cannot physically affect, or be affected by other colliders
            if (IsSensor || other.IsSensor || ColliderType == ColliderType.Static)
                return;
            if (other is CircleCollider)
            {
                Circle2D otherCircle = (other as CircleCollider).Circle;

                double angle = System.Math.Atan2(otherCircle.Center.Y - Rect.Center.Y, otherCircle.Center.X - Rect.Center.X);

                float distanceBetweenCircles = (float)System.Math.Sqrt(
                    (otherCircle.Center.X - Rect.Center.X) * (otherCircle.Center.X - Rect.Center.X) +
                    (otherCircle.Center.Y - Rect.Center.Y) * (otherCircle.Center.Y - Rect.Center.Y)
                );

                float distanceToMove = Rect.Width / 2 + otherCircle.Radius - distanceBetweenCircles;
                distanceToMove = distanceToMove * -1;
                Rect.Update(new Vector2(Rect.Position.X + (float)(System.Math.Cos(angle) * distanceToMove),
                    Rect.Position.Y + (float)(System.Math.Sin(angle) * distanceToMove)));


            }
            else if (other is RectangleCollider)
            {
                Rectangle2D otherRect = other.Rect;

                // Calculate the overlap between the two rectangles
                float overlapX = Math.Max(0, Math.Min(Rect.Right, otherRect.Right) - Math.Max(Rect.Left, otherRect.Left));
                float overlapY = Math.Max(0, Math.Min(Rect.Bottom, otherRect.Bottom) - Math.Max(Rect.Top, otherRect.Top));

                // Check for diagonal touch
                if (overlapX > 0 && overlapY > 0 && overlapX == overlapY)
                {
                    // The circle just touches the diagonal of the rectangle
                    // You can add specific handling code for this case here, if needed.
                    return;
                }
                // Check if there is an overlap
                if (overlapX > 0 && overlapY > 0)
                {
                    // Determine the direction of the collision
                    if (overlapX < overlapY)
                    {
                        // Colliding horizontally, adjust the X position
                        if (Rect.Left < otherRect.Left)
                            Rect.Update(new Vector2(Rect.Position.X - overlapX, Rect.Position.Y));
                        else
                            Rect.Update(new Vector2(Rect.Position.X + overlapX, Rect.Position.Y));
                    }
                    else
                    {
                        // Colliding vertically, adjust the Y position
                        if (Rect.Top < otherRect.Top)
                            Rect.Update(new Vector2(Rect.Position.X, Rect.Position.Y - overlapY));
                        else
                            Rect.Update(new Vector2(Rect.Position.X, Rect.Position.Y + overlapY));
                    }
                }
            }
        }
        public override bool DidCollide(Collider other)
        {
            if (other is CircleCollider)
            {
                Circle2D otherCircle = (other as CircleCollider).Circle;


                bool didCollide = otherCircle.Intersects(Rect);

                return didCollide;
            }
            else if (other is RectangleCollider)
            {
                bool didCollide = other.Rect.Intersects(Rect);
                return didCollide;
            }

            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //  if (Settings.Data.DebugCollisionCategories.HasFlag(CollisionCategories))
            Rect.Draw(spriteBatch, 1f, HadCollision ? PhysicsWorld.S_CollidedColor : ColorFromColliderType());
        }
    }
}
