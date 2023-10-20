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
    internal class CircleCollider : Collider
    {
        public Circle2D Circle { get; private set; }
        public CircleCollider(ColliderType colliderType, Vector2 center, int radius, CollisionCategory collisionCategory, CollisionCategory collidesWith, Vector2 offSet,
            int precision = 12) : base(colliderType, collisionCategory, collidesWith, offSet)
        {
            Circle = new Circle2D(center + OffSet, radius, precision);
            Rect = new Rectangle2D(Circle.Center.X - Circle.Radius,
                Circle.Center.Y - Circle.Radius, Circle.Radius * 2, Circle.Radius * 2);
        }

        /// <summary>
        /// This should never be called by static colliders
        /// </summary>
        public override bool DidCollide(Collider other)
        {

            if (other is CircleCollider)
            {
                Circle2D otherCircle = (other as CircleCollider).Circle;


                bool didCollide = Circle.Intersects(otherCircle);

                return didCollide;
            }
            else if (other is RectangleCollider)
            {
                return Circle.Intersects(other.Rect);
            }

            return false;
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

                double angle = System.Math.Atan2(otherCircle.Center.Y - Circle.Center.Y, otherCircle.Center.X - Circle.Center.X);

                float distanceBetweenCircles =
  (float)System.Math.Sqrt(
    (otherCircle.Center.X - Circle.Center.X) * (otherCircle.Center.X - Circle.Center.X) +
    (otherCircle.Center.Y - Circle.Center.Y) * (otherCircle.Center.Y - Circle.Center.Y)
  );

                float distanceToMove = Circle.Radius + otherCircle.Radius - distanceBetweenCircles;
                distanceToMove = distanceToMove * -1;
                Entity.Position = new Vector2(Entity.Position.X + (float)(System.Math.Cos(angle) * distanceToMove),
                    Entity.Position.Y + (float)(System.Math.Sin(angle) * distanceToMove));

            }
            else if (other is RectangleCollider)
            {
           Rectangle2D otherRect = other.Rect;

                // Calculate the closest point on the rectangle's boundary to the circle's center
                float closestX = Math.Max(otherRect.Left, Math.Min(Circle.Center.X, otherRect.Right));
                float closestY = Math.Max(otherRect.Top, Math.Min(Circle.Center.Y, otherRect.Bottom));

                // Calculate the distance between the circle's center and the closest point on the rectangle
                float deltaX = Circle.Center.X - closestX;
                float deltaY = Circle.Center.Y - closestY;
                float distance = (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));

                // If the circle is colliding with the rectangle, adjust its position
                if (distance < Circle.Radius)
                {
                    if (distance == 0)
                    {
                        // The circle's center is exactly at the closest point on the rectangle,
                        // which means it's touching a corner. Move the circle along the normal vector.
                        Vector2 normal = new Vector2(deltaX, deltaY);
                        normal.Normalize();

                        // Calculate the new position of the circle to move it out of the collision
                        Vector2 newPosition = Circle.Center + normal * Circle.Radius;

                        // Update the circle's position
                        Entity.Position = newPosition;
                    }

                    else
                    {
                        // Calculate the penetration depth
                        float penetrationDepth = Circle.Radius - distance;

                        // Calculate the normal vector from the circle center to the closest point on the rectangle
                        Vector2 normal = new Vector2(deltaX, deltaY);
                        normal.Normalize();

                        // Calculate the new position of the circle to move it out of the collision
                        Vector2 newPosition = Circle.Center + normal * penetrationDepth;

                        // Update the circle's position
                        Entity.Position = newPosition;
                    }
                }
            }

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Circle.Update(Entity.Position + OffSet);
            Rect = new Rectangle2D(Circle.Center.X - Circle.Radius,
                Circle.Center.Y - Circle.Radius, Circle.Radius * 2, Circle.Radius * 2);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if (Settings.Data.DebugCollisionCategories.HasFlag(CollisionCategories))
                Circle.Draw(spriteBatch, 1f, HadCollision ? PhysicsWorld.S_CollidedColor : ColorFromColliderType());
        }
    }
}
