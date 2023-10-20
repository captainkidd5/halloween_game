using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.PhysicsStuff.Primitives
{
    public struct Rectangle2D
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public Rectangle2D(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public Vector2 Position => new Vector2(X, Y);

        public void Update(Vector2 position)
        {
            Set(position.X, position.Y);

        }

        public void Draw(SpriteBatch spriteBatch, float layerDepth, Microsoft.Xna.Framework.Color? color = null, float? thickness = null)
        {

            Primitives2D.DrawRectangle(spriteBatch, this, layerDepth, color, thickness);
        }
        public Vector2 Location
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 Center
        {
            get { return new Vector2(X + (Width / 2), Y + (Height / 2)); }
        }

        public bool Contains(float x, float y)
        {
            return (x >= X && x <= Right && y >= Y && y <= Bottom);
        }

        public bool Contains(Vector2 point)
        {
            return Contains(point.X, point.Y);
        }
        public bool Contains(Rectangle2D rect)
        {
            return (rect.Left >= Left && rect.Right <= Right &&
                    rect.Top >= Top && rect.Bottom <= Bottom);
        }
        public bool Intersects(Rectangle2D other)
        {
            return !(Left > other.Right || Right < other.Left || Top > other.Bottom || Bottom < other.Top);
        }

        public bool Intersects(ref Rectangle2D other)
        {
            return !(Left > other.Right || Right < other.Left || Top > other.Bottom || Bottom < other.Top);
        }

        public bool Contains(ref Rectangle2D other)
        {
            return (other.Left >= Left && other.Right <= Right &&
                                   other.Top >= Top && other.Bottom <= Bottom);
        }

        public void Set(float x, float y)
        {
            X = x;
            Y = y;
        }
        public void Set(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }


        public static readonly Rectangle2D Empty = new Rectangle2D(0, 0, 0, 0);
    }
}
