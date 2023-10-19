
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
}
