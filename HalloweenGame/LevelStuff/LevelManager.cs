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

        public LevelManager(ContentManager content)
        {
            _content = content;

            _currentLevel = new Level();
            TileMapInitializer.Initialize(_content);
        }

        public void SwitchLevel(string name)
        {
            _currentLevel = new Level();
        }
        public void Update(GameTime gameTime)
        {
            _currentLevel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentLevel.Draw(spriteBatch);
        }
    }
}
