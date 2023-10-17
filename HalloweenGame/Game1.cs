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

    private AnimatedSprite _animatedSprite;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _levelManager = new LevelManager();

        AnimatedSprite.Animation scareAnimation = new AnimatedSprite.Animation();

        for (int i = 0; i < 14; ++i)
            scareAnimation.AddFrame(new AnimatedSprite.AnimationFrame(i, 500, 30, 44));

        _animatedSprite = new AnimatedSprite(Content.Load<Texture2D>("sprites/ScaredSkelly"));
        _animatedSprite.AddAnimation("scared", scareAnimation);
        _animatedSprite.SetAnimation("scared");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        _levelManager.Update(gameTime);
        _animatedSprite.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _levelManager.Draw(_spriteBatch);
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        // TODO: Merge this with Waiiki's code when the time is here
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
        _animatedSprite.Draw(_spriteBatch, new Vector2(300, 150));
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
