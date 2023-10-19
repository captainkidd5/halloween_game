using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.TileManagerStuff
{
    internal class BoundsHelper
    {
        //How many tiles outside of the viewport should be rendered.
        //some tiles (trees, buildings) are quite large so we have to extend culling a bit so as to not cut them off!
        public static readonly int _cullingLeeWay = 12;

        #region INDEX VARIABLES
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int EndX { get; private set; }

        public int EndY { get; private set; }


        #endregion

        private int _mapWidth;

        public void Load(int mapWidth)
        {
            _mapWidth = mapWidth;
        }

        /// <summary>
        /// This method will cull tiles, so that only the tiles within the screen are updated/drawn.
        /// </summary>
        private void CalculateStartAndEndIndexes()
        {

            int screenHalfTiles = (int)(Screen.BackbufferWidth / Camera.Zoom / 2 / Tile.S_Width);

            StartX = (int)(Camera.X /Tile.S_Width) - screenHalfTiles - _cullingLeeWay;
            if (StartX < 0)
                StartX = 0;

            StartY = (int)(Camera.Y / Tile.S_Width) - screenHalfTiles - _cullingLeeWay;
            if (StartY < 0)
                StartY = 0;

            EndX = (int)(Camera.X / Tile.S_Width) + screenHalfTiles + _cullingLeeWay;
            if (EndX > _mapWidth)
                EndX = _mapWidth;

            EndY = (int)(Camera.Y / Tile.S_Width) + screenHalfTiles + _cullingLeeWay;
            if (EndY > _mapWidth)
                EndY = _mapWidth;

  
        }

        public bool InRange(int x, int y)
        {
            return x >= StartX && x <= EndX && y >= StartY && y <= EndY;
        }

        public void Update()
        {
            CalculateStartAndEndIndexes();
        }

        public bool IsWithinUpdateRange(Tile tile)
        {
            if (tile.X >= StartX && tile.X < EndX)
            {
                if (tile.Y >= StartY && tile.Y < EndY)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
