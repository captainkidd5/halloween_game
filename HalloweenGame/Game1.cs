using HalloweenGame.InputStuff;
using HalloweenGame.LevelStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HalloweenGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private LevelManager _levelManager;

    private Player _player;

    private InputService _input;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _input = new InputService();

        Camera.Initialize(GraphicsDevice);
        Screen.Initialize(_graphics, Window);
        _levelManager = new LevelManager(Content, GraphicsDevice);

        _player = new Player(Content, new Vector2(100, 100));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        _input.Update();

        if (_input.Keyboard.IsKeyPressed(Keys.Escape))
            Exit();

        // TODO: Update player position within the player object?
        if (_input.Keyboard.IsKeyDown(Keys.A))
            _player.Position = new Vector2(_player.Position.X - 10, _player.Position.Y);

        if (_input.Keyboard.IsKeyDown(Keys.D))
            _player.Position = new Vector2(_player.Position.X + 10, _player.Position.Y);

        Camera.Update();
        // TODO: Add your update logic here

        _levelManager.Update(gameTime);
        _player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _levelManager.Draw(_spriteBatch);
      //  GraphicsDevice.Clear(Color.CornflowerBlue);


  //  TODO: Merge this with Waiiki's code when the time is here
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
        _player.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
