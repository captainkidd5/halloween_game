using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.PhysicsStuff.Primitives
{
    public class Circle2D
    {
        public Vector2 Center { get; private set; }
        public float Radius { get; private set; }
        public Vector2[] Points { get; private set; }

        public Circle2D(Vector2 center, int radius, int precision = 12)
        {
            Center = center;
            Points = new Vector2[precision];
            Radius = radius;
            Points = GetPoints();

        }

        private Vector2[] GetPoints(int precision = 12)
        {
            Vector2[] points = new Vector2[precision];
            float angle = 0;
            for (int i = 0; i < precision; i++)
            {
                angle += (float)Math.PI * 2 / precision;
                points[i] = new Vector2((float)(Radius * Math.Cos(angle)), (float)(Radius * Math.Sin(angle)));
            }

            return points;
        }

        public bool Intersects(Circle2D other)
        {
            float distanceBetweenCirclesSquared =
   (other.Center.X - Center.X) * (other.Center.X - Center.X) +
   (other.Center.Y - Center.Y) * (other.Center.Y - Center.Y);


            bool didCollide = (distanceBetweenCirclesSquared <
            (Radius + other.Radius) * (Radius + other.Radius));

            return didCollide;
        }

        public bool Intersects(Rectangle2D other)
        {
            // Calculate the center of the circle
            float circleCenterX = Center.X;
            float circleCenterY = Center.Y;

            // Find the closest point on the rectangle to the center of the circle
            float closestX = MathHelper.Clamp(circleCenterX, other.Left, other.Right);
            float closestY = MathHelper.Clamp(circleCenterY, other.Top, other.Bottom);

            // Calculate the distance between the circle's center and the closest point on the rectangle
            float distanceSquared = (circleCenterX - closestX) * (circleCenterX - closestX) +
                                    (circleCenterY - closestY) * (circleCenterY - closestY);

            // Check if the distance squared is less than the circle's radius squared
            bool didCollide = (distanceSquared < Radius * Radius);

            return didCollide;
        }
        public bool Intersects(Rectangle other)
        {
            // Calculate the center of the circle
            float circleCenterX = Center.X;
            float circleCenterY = Center.Y;

            // Find the closest point on the rectangle to the center of the circle
            float closestX = MathHelper.Clamp(circleCenterX, other.Left, other.Right);
            float closestY = MathHelper.Clamp(circleCenterY, other.Top, other.Bottom);

            // Calculate the distance between the circle's center and the closest point on the rectangle
            float distanceSquared = (circleCenterX - closestX) * (circleCenterX - closestX) +
                                    (circleCenterY - closestY) * (circleCenterY - closestY);

            // Check if the distance squared is less than the circle's radius squared
            bool didCollide = (distanceSquared < Radius * Radius);

            return didCollide;
        }
        public void Update(Vector2 position)
        {
            Center = position;

        }

        public void Draw(SpriteBatch spriteBatch, float layerDepth, Color? color = null, float? thickness = null)
        {

            Primitives2D.DrawCircle(spriteBatch, Center, Points, layerDepth, color, thickness);
        }

    }
}
