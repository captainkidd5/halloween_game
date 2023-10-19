using HalloweenGame.TileManagerStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.LevelStuff
{
    internal class LevelManager
    {
        private Level _currentLevel;
        private readonly ContentManager _content;
        private readonly GraphicsDevice _graphics;
        public static Texture2D TilesTexture;
        public LevelManager(ContentManager content, GraphicsDevice graphics)
        {
            _content = content;
            _graphics = graphics;
            TilesTexture = _content.Load<Texture2D>("tiles/test_tile");
            TileMapInitializer.Initialize(_content);

            SwitchLevel("level_1");
        }

        public void SwitchLevel(string name)
        {
            _currentLevel = new Level(name);
            _currentLevel.Initialize();
        }
        public void Update(GameTime gameTime)
        {
            _currentLevel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _graphics.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera.Transform);
            _currentLevel.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
