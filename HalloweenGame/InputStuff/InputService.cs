
namespace HalloweenGame.InputStuff;

public class InputService
{
    public KeyboardManager Keyboard { get; private set; }

    public InputService()
    {
        Keyboard = new KeyboardManager();
    }

    public void Update()
    {
        Keyboard.Update();
    }
}
