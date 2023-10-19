using Core.TiledSharp;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.TileManagerStuff
{
    internal static class TileMapInitializer
    {
        private static ContentManager _content;

        public static Texture2D S_TileSetTexture;
    

        public static void Initialize(ContentManager content)
        {
            _content = content;
        }
        public static void LoadContent()
        {
            S_TileSetTexture = _content.Load<Texture2D>("tiles/test_tile");
        }
        internal static TileData[][,] CreateTileDataForMap(string levelName)
        {
            TmxMap map = new TmxMap(_content.RootDirectory + "/maps/" + levelName + ".tmx");


            List<TmxLayer> allLayers = new List<TmxLayer>()
            {
                map.TileLayers["background"],
            map.TileLayers["foreground"],
 
        };
            TileData[][,] tileDataToReturn = new TileData[allLayers.Count][,];

            for (int i = 0; i < allLayers.Count; i++)
            {
                tileDataToReturn[i] = new TileData[map.Width, map.Width];
                foreach (TmxLayerTile layerNameTile in allLayers[i].Tiles)
                {
                    tileDataToReturn[i][layerNameTile.X, layerNameTile.Y] = new TileData();
                    TileData tileData = new TileData();

                    tileData.Load(
                        (ushort)(layerNameTile.Gid),
                        (ushort)layerNameTile.X,
                        (ushort)layerNameTile.Y,
                        (Layers)i);
                    tileDataToReturn[i][layerNameTile.X, layerNameTile.Y] = tileData;
                }
            }
            return tileDataToReturn;
        }
    }
}
