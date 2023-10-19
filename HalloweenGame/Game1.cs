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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Globals.Input = new InputService();

        Camera.Initialize(GraphicsDevice);
        Screen.Initialize(_graphics, Window);
        _levelManager = new LevelManager(Content, GraphicsDevice);

        Globals.Player = new Player(Content, new Vector2(100, 100));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        Globals.Input.Update();

        if (Globals.Input.Keyboard.IsKeyPressed(Keys.Escape))
            Exit();

        // TODO: Update player position within the player object?
        if (Globals.Input.Keyboard.IsKeyDown(Keys.A))
            Globals.Player.Position = new Vector2(Globals.Player.Position.X - 10, Globals.Player.Position.Y);

        if (Globals.Input.Keyboard.IsKeyDown(Keys.D))
            Globals.Player.Position = new Vector2(Globals.Player.Position.X + 10, Globals.Player.Position.Y);

        Camera.Update();
        // TODO: Add your update logic here

        _levelManager.Update(gameTime);
        Globals.Player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _levelManager.Draw(_spriteBatch);
      //  GraphicsDevice.Clear(Color.CornflowerBlue);


  //  TODO: Merge this with Waiiki's code when the time is here
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
        Globals.Player.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
