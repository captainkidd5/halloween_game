
using System.Collections.Specialized;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HalloweenGame.InputStuff;

public class Input
{
    public KeyboardManager Keyboard { get; private set; }

    public Input()
    {
        Keyboard = new KeyboardManager();
    }

    public void Update()
    {
        Keyboard.Update();
    }

    public Vector2 GetVelocity()
    {
        Vector2 velocity = Vector2.Zero;

        if (Keyboard.IsKeyDown(Keys.A))
            velocity.X -= 1;

        if (Keyboard.IsKeyDown(Keys.D))
            velocity.X += 1;
        if (Keyboard.IsKeyDown(Keys.S))
            velocity.Y += 1;
        if (velocity != Vector2.Zero)
            velocity.Normalize();

        return velocity;
    }
}
