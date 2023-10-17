using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.TileManagerStuff
{
    public enum Layers
    {
        Background,
        Foreground,
    }
    public struct TileData
    {

        private ushort _gid;
        public ushort GID { get { return _gid > 10000 ? (ushort)(_gid - 10001) : (ushort)(_gid - 1); } internal set { _gid = value; } }

        public ushort X;
        public ushort Y;
        public Layers Layer;

        public void Load(ushort gid, ushort x, ushort y, Layers layer)
        {
            _gid = gid;
            X = x;
            Y = y;
            Layer = layer;
        }

        /// <summary>
        /// returns unique tile key thru bitwise shifting.
        /// </summary>
        /// <returns>Tile Key</returns>
        public int GetKey()
        {
            return X << 18 | Y << 4 | (int)Layer << 0; //14 bits for x and y, 4 bits for layer.
        }
    }
}
