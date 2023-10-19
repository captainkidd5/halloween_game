using HalloweenGame.LevelStuff;
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

        public static  int TileSetDimension => LevelManager.TilesTexture.Width / Tile.S_Width;

        private Tile[][,] _tiles;
        private BoundsHelper _boundsHelper;
        public TileManager(TileData[][,] tileData)
        {
            _tiles = new Tile[tileData.Count()][,];
            for(int z = 0; z < tileData.Count(); z++)
            {
                _tiles[z] = new Tile[tileData[z].GetLength(0), tileData[z].GetLength(1)];
                for(int i =0 ; i < tileData[z].GetLength(0); i++)
                {
                    for (int j =0; j < tileData[z].GetLength(1); j++)
                    {
                        Tile tile = new Tile();
                        tile.Load(tileData[z][i, j]);

                        _tiles[z][i, j] = tile;
                        PropHelper.AssignProperties(tile);
                    }
                }
            }
            _boundsHelper = new BoundsHelper();
            _boundsHelper.Load(tileData[0].GetLength(0));
        }

        public void Update(GameTime gameTime)
        {
            _boundsHelper.Update();
            //move the camera so that the very bottom tiles of the map are visible
            Camera.Jump(new Vector2(Screen.BackbufferWidth /2, _tiles[0].GetLength(1) * Tile.S_Width - Screen.BackbufferHeight/2));

            for (int z = 0; z < _tiles.Length; z++)
            {
                for(int x = _boundsHelper.StartX; x < _boundsHelper.EndX; x++)
                {
                    for(int y = _boundsHelper.StartY; y < _boundsHelper.EndY; y++)
                    {
                        _tiles[z][x, y].Update(gameTime);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int z = 0; z < 1; z++)
            {
                for (int x = _boundsHelper.StartX; x < _boundsHelper.EndX; x++)
                {
                    for (int y = _boundsHelper.StartY; y < _boundsHelper.EndY; y++)
                    {
                        _tiles[z][x, y].Draw(spriteBatch, LevelManager.TilesTexture);
                    }
                }
            }
        }
    }
}
