using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.PhysicsStuff
{
    public class Component
    {
        protected Entity Entity { get; set; }
        public bool FlaggedForRemoval { get; private set; }
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Attach(Entity entity)
        {
            Entity = entity;
        }


        public virtual void Destroy()
        {
            //This will intentionally (for now?) double up with the physics world removal
            FlaggedForRemoval = true;
        }
    }
}
