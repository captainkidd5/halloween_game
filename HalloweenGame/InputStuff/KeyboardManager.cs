
using Microsoft.Xna.Framework.Input;

namespace HalloweenGame.InputStuff;

public class KeyboardManager
{
    private KeyboardState _currentState;
    private KeyboardState _previousState;

    public KeyboardManager()
    {
        _currentState = Keyboard.GetState();
        _previousState = _currentState;
    }

    public void Update()
    {
        _previousState = _currentState;
        _currentState = Keyboard.GetState();
    }

    public bool IsKeyPressed(Keys key) => _currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);
    public bool IsKeyDown(Keys key) => _currentState.IsKeyDown(key) && _previousState.IsKeyDown(key);
    public bool IsKeyReleased(Keys key) => _currentState.IsKeyUp(key) && _previousState.IsKeyDown(key);
    public bool IsKeyUp(Keys key) => _currentState.IsKeyUp(key) && _previousState.IsKeyUp(key);
}
