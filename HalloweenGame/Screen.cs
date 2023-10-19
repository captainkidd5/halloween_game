using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame
{
    public static class Screen
    {

        public static readonly Point[] SupportedScreenResolutoins = new Point[]
        {
            new Point(393, 852),
            //new Point(800,1280),
            new Point(1024,768),

            new Point(1280,720),

            new Point(1280,800),

            new Point(1280,1024),
            new Point(1360,768),
            new Point(1366,768),
            new Point(1920,1280),


        };

        public static bool IsDesktopResolution => BackbufferWidth >= 1280 && BackbufferHeight >= 720;
        public static string GetScreenString() { return $"{BackbufferWidth}x{BackbufferHeight}"; }
        private static GraphicsDeviceManager Graphics;
        public static int BackbufferWidth => Graphics.PreferredBackBufferWidth;
        public static int BackbufferHeight => Graphics.PreferredBackBufferHeight;

        public static float AspectRatio => (float)BackbufferWidth / (float)BackbufferHeight;

        public static int NativeWidth = 1280;
        public static int NativeHeight = 720;

        public static float PreferredAspect = (float)NativeWidth / (float)NativeHeight;
        public static Rectangle ScreenRectangle;
        public static GameWindow GameWindow;

        public static Vector2 CenterScreen => new Vector2(BackbufferWidth / 2, BackbufferHeight / 2);
        public static void Initialize(GraphicsDeviceManager graphics, GameWindow gameWindow)
        {
            Graphics = graphics;
            GameWindow = gameWindow;
            GameWindow.ClientSizeChanged += Window_ClientSizeChanged;
            GameWindow.AllowUserResizing = false;
            SetResolution(NativeWidth, NativeHeight);
        }
        public static Rectangle GetVisibleRectangle()
        {

            Rectangle rect = new Rectangle(
                (int)Camera.X - (int)(BackbufferWidth / 2 / Camera.Zoom),
                (int)Camera.Y - (int)(BackbufferHeight / 2 / Camera.Zoom),
                (int)(BackbufferWidth / Camera.Zoom), (int)(BackbufferHeight / Camera.Zoom));
            return rect;
        }

        public static void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            if (GameWindow != null)
            {
                GameWindow.ClientSizeChanged -= Window_ClientSizeChanged;
                SetResolution(GameWindow.ClientBounds.Width, GameWindow.ClientBounds.Height);
                GameWindow.ClientSizeChanged += Window_ClientSizeChanged;
            }

        }



        public static void SetResolution(int width, int height)
        {

            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            ScreenRectangle = GetScreenRectangle();
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();


        }

        public static void ToggleFullscreen(bool? value)
        {
            if (value == null)
                Graphics.IsFullScreen = !Graphics.IsFullScreen;
            else
                Graphics.IsFullScreen = value.Value;
            //Graphics.ToggleFullScreen();
            Graphics.HardwareModeSwitch = false;
            //  Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ScreenRectangle = GetScreenRectangle();

            Graphics.ApplyChanges();
        }


        public static Rectangle GetScreenRectangle()
        {
            Rectangle screenRectangle;

            screenRectangle = new Rectangle(0, 0, BackbufferWidth, BackbufferHeight);

            return screenRectangle;
        }


    }
}
