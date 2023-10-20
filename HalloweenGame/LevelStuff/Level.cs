using HalloweenGame.TileManagerStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HalloweenGame.LevelStuff
{
    internal class Level
    {

        private TileManager _tileManager;
        private readonly string _name;

        public Level(string name)
        {
            _name = name;
        }
        public void Initialize()
        {
            _tileManager = new TileManager(TileMapInitializer.CreateTileDataForMap(_name));

        }
        public void Update(GameTime gameTime)
        {
            Globals.Player.Update(gameTime);

            _tileManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Globals.Player.Draw(spriteBatch);

            _tileManager.Draw(spriteBatch);
        }
  
    }
}
