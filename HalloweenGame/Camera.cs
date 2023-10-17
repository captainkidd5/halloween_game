using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame
{
    public static class Camera
    {

        private static Vector2 _origin { get; set; }
        public static Vector2 position;
        private static float _zoom { get; set; }
        private static float _rotation { get; set; }

        private static Rectangle _viewPortRectangle;


        public static float Zoom { get { return _zoom; } set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } }

        public static float X { get { return position.X; } }
        public static float Y { get { return position.Y; } }

        public static bool LockBounds { get; set; }

        public static Matrix Transform { get; private set; }

        private static GraphicsDevice _graphicsDevice;

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            SetToDefault();
            _origin = new Vector2(graphicsDevice.Viewport.Width / 2.0f, graphicsDevice.Viewport.Height / 2.0f);
            _graphicsDevice = graphicsDevice;
        }



        public static void SetToDefault()
        {
            Zoom = 3.0f;
            _rotation = 0.0f;
            position = Vector2.Zero;
            LockBounds = false;
        }

        public static Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, GetViewMatrix(Vector2.Zero));
        }

        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            //   Vector2 newCoords = screenPosition -  new Vector2(_graphicsDevice.Viewport.X, _graphicsDevice.Viewport.Y);
            //Vector2 transformed = Vector2.Transform(screenPosition, Matrix.Invert(GetViewMatrix(Vector2.One))) - new Vector2(Screen.BackbufferWidth, Screen.BackbufferHeight) / 2;
            //return transformed;
            return Vector2.Transform(screenPosition,
               Matrix.Invert(Transform));


        }
        public static void Jump(Vector2 jumpToPos)
        {
            position = jumpToPos;

        }

        public static void Update()
        {
            Transform = GetTransform();

        }
        public static void Follow(Vector2 amount, Rectangle mapRectangle)
        {

            position = amount;
            _viewPortRectangle = new Rectangle((int)(mapRectangle.X + Screen.BackbufferWidth / 2 / Zoom),
              (int)(mapRectangle.Y + Screen.BackbufferHeight / 2 / Zoom),
                (int)(mapRectangle.Width - Screen.BackbufferWidth / 2 / Zoom),
                (int)(mapRectangle.Height - Screen.BackbufferHeight / 2 / Zoom));

            if (LockBounds)
            {
                if (position.X < _viewPortRectangle.X)
                    position.X = _viewPortRectangle.X;

                if (position.X > _viewPortRectangle.Width)
                    position.X = _viewPortRectangle.Width;
                if (position.Y < _viewPortRectangle.Y)
                    position.Y = _viewPortRectangle.Y;
                if (position.Y > _viewPortRectangle.Height)
                    position.Y = _viewPortRectangle.Height;
            }
        }

        /// <summary>
        /// Will have camera "float" towards a desired point.
        /// </summary>
        /// <param name="amount">Position to float to</param>
        private static void Lerp(Vector2 amount)
        {
            position.X = MathHelper.Lerp(X, amount.X, .5f);
            position.Y = MathHelper.Lerp(Y, amount.Y, .5f);
        }


        private static Matrix GetTransform()
        {

            return Matrix.CreateTranslation(
                new Vector3(-X, -Y, 0)) *
                Matrix.CreateRotationZ(_rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(
                    new Vector3(Screen.BackbufferWidth / 2,
           Screen.BackbufferHeight / 2, 0));
        }

        public static Matrix GetViewMatrix(Vector2 parallax)
        {
            // To add parallax, simply multiply it by the position
            return Matrix.CreateTranslation(new Vector3(-position * parallax, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-_origin, 0.0f)) *
                Matrix.CreateRotationZ(_rotation) *
                Matrix.CreateScale(Zoom, Zoom, 0) *
           Matrix.CreateTranslation(
                    new Vector3(_graphicsDevice.Viewport.Width * 0.5f,
            _graphicsDevice.Viewport.Height * 0.5f, 0));
        }


    }
}
