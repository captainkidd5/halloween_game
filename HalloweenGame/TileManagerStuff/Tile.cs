﻿using HalloweenGame.PhysicsStuff;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.TileManagerStuff
{
    internal class Tile : Entity
    {
        public static readonly int S_Width = 32;



        public static int EmptyGid = 65535;

        private TileData _tileData;
        public ushort GID => _tileData.GID;

        public int Key => _tileData.GetKey();
        public ushort X => _tileData.X;
        public ushort Y => _tileData.Y;
        public Layers Layer => _tileData.Layer;

        public void Load(TileData tileData)
        {
            _tileData = tileData;
            Position = GetPosition();


        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tileSetTexture)
        {
            base.Draw(spriteBatch);
            if (GID == EmptyGid)
                return;
            spriteBatch.Draw(tileSetTexture, Position, GetNormalSourceRectangle(GID),
                Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, .5f);
        }

        public Vector2 GetPosition()
        {
            return new Vector2(X * S_Width, Y * S_Width);
        }

        /// <summary>
        /// Cut out a rectangle from the tileset based on the tile GID.
        /// </summary>
        public static Rectangle GetNormalSourceRectangle(int gid)
        {

            int Column = gid % TileManager.TileSetDimension;
            int Row = (int)Math.Floor(gid / (float)TileManager.TileSetDimension);
            return new Rectangle(S_Width * Column, S_Width * Row, S_Width, S_Width);
        }
    }
}
