using HalloweenGame.PhysicsStuff.Primitives;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.PhysicsStuff
{
    /// <summary>
    /// Static will never have forces affect it, but can be manually moved. Dynamic will act upon, and be acted upon
    /// </summary>
    public enum ColliderType
    {
        Static = 1,
        Dynamic = 2
    }
    [Flags]
    public enum CollisionCategory
    {
        None = 0,
        All = int.MaxValue,
        Solid = 1,
        Player = 2,
        Hook = 4,
        WorldItem = 8,
        Cat5 = 16,
        Cat6 = 32,
        Cat7 = 64,
        Cat8 = 128,
        Cat9 = 256,
        Cat10 = 512,
        Cat11 = 1024,
        Cat12 = 2048,
        Cat13 = 4096,
        Cat14 = 8192,
        Cat15 = 16384,
        Cat16 = 32768,
        Cat17 = 65536,
        Cat18 = 131072,
        Cat19 = 262144,
        Cat20 = 524288,
        Cat21 = 1048576,
        Cat22 = 2097152,
        Cat23 = 4194304,
        Cat24 = 8388608,
        Cat25 = 16777216,
        Cat26 = 33554432,
        Cat27 = 67108864,
        Cat28 = 134217728,
        Cat29 = 268435456,
        Cat30 = 536870912,
        Cat31 = 1073741824
    }

    public enum Speed : byte
    {
        None = 0,
        Slow = 1,
        Medium = 16,
        Fast = 32,
        ExtraFast = 64
    }
    public class PhysicsWorld
    {

        public int DynamicCount { get; private set; }
        public int StaticCount { get; private set; }
        public int ContactCount { get; private set; }

        public static readonly float S_Gravity = 200f;

        public static readonly Color S_DynamicColor = Color.Blue;
        public static readonly Color S_StaticColor = Color.Yellow;
        public static readonly Color S_CollidedColor = Color.Red;


        private QuadTree<Collider> _quadTree2;
        private List<Collider> _colliders;
        private List<Collider> _collisions;
        public void Initialize()
        {

            _quadTree2 = new QuadTree<Collider>(4, 16, new Rectangle2D(0, 0, 2400, 2400));
            _colliders = new List<Collider>();
            _collisions = new List<Collider>();

        }


        public void Add(Collider collider)
        {
            _colliders.Add(collider);

        }


        public void Update()
        {
       
                Reset();
                DoSimulation();
            

        }

        private void Reset()
        {
            DynamicCount = 0;
            StaticCount = 0;
            ContactCount = 0;
            _quadTree2.Clear();
        }

        private void DoSimulation()
        {


            foreach (Collider originalCollider in _colliders)
            {
                _quadTree2.Insert(originalCollider, originalCollider.Rect);

            }

            foreach (Collider originalCollider in _colliders)
            {
 

                if (originalCollider.ColliderType == ColliderType.Static)
                    StaticCount++;
                else if (originalCollider.ColliderType == ColliderType.Dynamic)
                    DynamicCount++;

                ContactCount += originalCollider.ContactCount;


                _quadTree2.FindCollisions(originalCollider, ref _collisions);
                foreach (Collider collision in _collisions)
                {
                    if (originalCollider == collision)
                        continue;
                    if (originalCollider.Resolve(collision))
                        Console.WriteLine("test");
                }
                _collisions.Clear();


            }
            for (int i = _colliders.Count - 1; i >= 0; i--)
            {
                _colliders[i].CleanupPhase();

                if (_colliders[i].FlaggedForRemoval)
                    _colliders.RemoveAt(i);
            }

            //two things touching should just mean a single contact
            ContactCount = ContactCount / 2;



        }


    }
}
