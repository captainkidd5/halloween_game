using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloweenGame.PhysicsStuff.Primitives;

namespace HalloweenGame.PhysicsStuff
{
    public abstract class Collider : Component
    {
        public static readonly Color StaticColor = Color.Yellow;
        public static readonly Color DynamicColor = Color.Blue;
        protected Vector2 OffSet { get; set; }
        public ColliderType ColliderType { get; private set; }
        public CollisionCategory CollisionCategories { get; set; }
        public CollisionCategory CategoriesCollidesWith { get; set; }

        public bool IsSensor { get; set; }
        protected Vector2 Velocity { get; set; }
        public bool HadCollision { get; protected set; }
        public object UserData { get; set; }

        public Action<Collider> OnCollidesAction { get; set; }
        public Action<Collider> OnSeparatesAction { get; set; }



        public Dictionary<Collider, bool> CurrentContacts;

        public int ContactCount => CurrentContacts.Count;

        public Rectangle2D Rect { get; protected set; }

        public Collider(ColliderType colliderType, CollisionCategory collisionCategory, CollisionCategory collidesWith, Vector2 offSet)
        {

            ColliderType = colliderType;
            CollisionCategories = collisionCategory;
            CategoriesCollidesWith = collidesWith;
            CurrentContacts = new Dictionary<Collider, bool>();
            IsSensor = false;
            OffSet = offSet;
        }

        public override void Update(GameTime gameTime)
        {
            HadCollision = false;
            Vector2 newVelocity = Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ColliderType != ColliderType.Static)
                newVelocity = new Vector2(newVelocity.X, newVelocity.Y + PhysicsWorld.S_Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            Entity.Position += newVelocity;
           

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public bool Resolve(Collider other)
        {
            if (this.CollisionCategories == CollisionCategory.Player)
                Console.WriteLine("test");
            if (NoCategoryOverlap(other))
            {

                return false;
            }

            if (DidCollide(other))
            {
                //we already know they collided

                ReactToCollision(other);
                return true;
            }
            else
            {
                return false;
            }

        }

        public void CleanupPhase()
        {

            if (this.CollisionCategories == CollisionCategory.Player)
                Console.WriteLine("test");
            if (CurrentContacts.Count > 0)
            {
                if (FlaggedForRemoval)
                {
                    foreach (var kvp in CurrentContacts)
                        kvp.Key.CurrentContacts.Remove(this);
                    return;
                }

                List<Collider> collidersToRemove = new List<Collider>();
                foreach (var kvp in CurrentContacts)
                {
                    if (!kvp.Value)
                    {
                        //false means we had a collision last frame, but not this one, therefore we should remove it and fire the separate event
                        collidersToRemove.Add(kvp.Key);
                        OnSeparates(kvp.Key);
                        kvp.Key.OnSeparates(this);
                        kvp.Key.CurrentContacts.Remove(this);
                    }
                    CurrentContacts[kvp.Key] = false;

                }
                foreach (var collider in collidersToRemove)
                {
                    CurrentContacts.Remove(collider);
                }
            }

        }
        /// <summary>
        /// Only dynamic colliders should react to collisions. Static colliders will be unaffected
        /// </summary>
        /// <param name="other"></param>
        protected virtual void ReactToCollision(Collider other)
        {

            if (this.CollisionCategories == CollisionCategory.Player)
                Console.WriteLine("test");
            if (!CurrentContacts.ContainsKey(other))
            {
                CurrentContacts.Add(other, true);
                OnCollides(other);
                //Both collided if one did
                other.OnCollides(this);
            }
            else
            {
                //We're still in contact, signal this to prevent a separate event in the cleanup phase
                CurrentContacts[other] = true;
            }
            HadCollision = true;
            //  other.HadCollision = true;
        }
        private bool NoCategoryOverlap(Collider other)
        {
            return ((CollisionCategories & other.CategoriesCollidesWith) == CollisionCategory.None ||
                 (CategoriesCollidesWith & other.CollisionCategories) == CollisionCategory.None);
        }



        public abstract bool DidCollide(Collider other);
        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        protected Color ColorFromColliderType()
        {
            Color color = StaticColor;
            switch (ColliderType)
            {
                case ColliderType.Static:
                    color = StaticColor;
                    break;
                case ColliderType.Dynamic:
                    color = DynamicColor;
                    break;
                default:
                    color = StaticColor;
                    break;
            }

            //reduce color intensity if sensor
            if (IsSensor)
                color.A = 100;

            return color;
        }

        public override void Attach(Entity entity)
        {
            base.Attach(entity);
            if (ColliderType == ColliderType.Dynamic)
                Game1.PhysicsWorld.Add(this);
            else if (ColliderType == ColliderType.Static)
                Game1.PhysicsWorld.Add(this);

        }


        public void OnCollides(Collider other)
        {

            OnCollidesAction?.Invoke(other);
        }

        public void OnSeparates(Collider other)
        {
            OnSeparatesAction?.Invoke(other);

        }


    }
}
