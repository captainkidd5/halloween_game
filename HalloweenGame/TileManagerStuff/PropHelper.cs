using Core.TiledSharp;
using HalloweenGame.PhysicsStuff.Primitives;
using HalloweenGame.PhysicsStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HalloweenGame.TileManagerStuff
{
    internal static class PropHelper
    {
        /// <summary>
        /// Retrieve whether or not the tile contains the specified property.
        /// </summary>
        /// <param name="property">Property value will be stored in this string reference.</param>
        public static bool GetTileProperty(TmxTilesetTile tile, ref string property)
        {
            return tile.Properties.TryGetValue(property, out property);
        }

        public static bool GetProperty(Tile tile, ref string property)
        {
            var dict = TileMapInitializer.TilePropertyDictionary;
            if (!dict.ContainsKey(tile.GID))
                return false;
            TmxTilesetTile tmxTile
                = dict[tile.GID];
            if (GetTileProperty(tmxTile, ref property))
                return true;
            return false;
        }
        public static void AssignProperties(Tile tile)
        {
            var dict = TileMapInitializer.TilePropertyDictionary;
            if (!dict.ContainsKey(tile.GID))
                return;
            TmxTilesetTile tmxTile
                = dict[tile.GID];



            if (tmxTile.ObjectGroups.Count > 0)
            {


                AddObjectsFromObjectGroups(tile, (Layers)tile.Layer, tmxTile);


            }
        }

        /// <summary>
        /// Handles Tmx Object Groups by adding them to the tile's collision list. Updates pathgrid accordingly.
        /// </summary>
        internal static void AddObjectsFromObjectGroups(Tile tile, Layers tileLayer, TmxTilesetTile tileSetTile)
        {
            for (int k = 0; k < tileSetTile.ObjectGroups[0].Objects.Count; k++)
            {
                TmxObject tmxObj = tileSetTile.ObjectGroups[0].Objects[k];
                string offSet = string.Empty;
                Vector2 offSetVector = Vector2.Zero;
                if (tmxObj.Properties.TryGetValue("offset", out offSet))
                {
                    offSetVector = ParseVector2String(offSet);
                }
                offSetVector = new Vector2(offSetVector.X, offSetVector.Y - 8);
                if (tmxObj.ObjectType == TmxObjectType.Ellipse)
                {
                    int radius = (int)tmxObj.Width / 2;
                    tile.AddComponent(new CircleCollider(
                        ColliderType.Static, tile.Position, (int)tmxObj.Width / 2,
                        CollisionCategory.Solid, CollisionCategory.Player | CollisionCategory.Hook, offSetVector));

                }
                else if (tmxObj.ObjectType == TmxObjectType.Basic)
                {
                    tile.AddComponent(new RectangleCollider(ColliderType.Static, new Rectangle2D(tile.Position.X, tile.Position.Y,
                        (float)tmxObj.Width, (float)tmxObj.Height),
                        CollisionCategory.Solid, CollisionCategory.Player | CollisionCategory.Hook, offSetVector));

                }


            }
        }

        private static Vector2 ParseVector2String(string str)
        {
            float x = float.Parse(str.Split(',')[0]);
            float y = float.Parse(str.Split(',')[1]);

            return new Vector2(x, y);
        }
    }
}
