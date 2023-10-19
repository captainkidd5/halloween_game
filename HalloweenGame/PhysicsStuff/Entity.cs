using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloweenGame.PhysicsStuff
{
    public class Entity
    {
        private List<Component> _components { get; set; }

        public Vector2 Position { get; set; }

        public float LayerDepth { get; set; }

        public Entity()
        {
            _components = new List<Component>();

        }
  

        public void WarpTo(Vector2 newPosition)
        {
            Position = newPosition;
        }
        public virtual void Update(GameTime gameTime)
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                _components[i].Update(gameTime);
                if (_components[i].FlaggedForRemoval)
                    _components.RemoveAt(i);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                _components[i].Draw(spriteBatch);
            }
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);
            component.Attach(this);
        }

        public void Destroy()
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                _components[i].Destroy();
            }
        }

    }
}
