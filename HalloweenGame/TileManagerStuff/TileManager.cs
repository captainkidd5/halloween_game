using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.TileManagerStuff
{
    public class TileManager
    {

        public static readonly int TileSetDimension = 100;

        private Tile[][,] _tiles;
        private BoundsHelper _boundsHelper;
        public TileManager(TileData[][,] tileData)
        {
            _tiles = new Tile[tileData.Count()][,];
            for(int z = 0; z < tileData.Count(); z++)
            {
               
                for(int i =0 ; i < tileData[z].GetLength(0); i++)
                {
                    for (int j =0; j < tileData[z].GetLength(1); j++)
                    {
                        Tile tile = new Tile();
                        tile.Load(tileData[z][i, j]);

                        _tiles[z][i, j] = tile;
                    }
                }
            }
            _boundsHelper = new BoundsHelper();
        }

        public void Update(GameTime gameTime)
        {
            _boundsHelper.Update();

            for(int z = 0; z < _tiles.Length; z++)
            {
                for(int x = _boundsHelper.StartX; x < _boundsHelper.EndX; x++)
                {
                    for(int y = _boundsHelper.StartY; y < _boundsHelper.EndY; y++)
                    {
                       // _tiles[z][x, y].Update(gameTime);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int z = 0; z < _tiles.Length; z++)
            {
                for (int x = _boundsHelper.StartX; x < _boundsHelper.EndX; x++)
                {
                    for (int y = _boundsHelper.StartY; y < _boundsHelper.EndY; y++)
                    {
                        _tiles[z][x, y].Draw(spriteBatch);
                    }
                }
            }
        }
    }
}
