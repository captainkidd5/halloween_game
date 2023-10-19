
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HalloweenGame;

public class AnimatedSprite
{
    public Texture2D Texture { get; private set; }

    public Dictionary<string, Animation> Animations { get; private set; }
    public Animation CurrentAnimation { get; private set; }

    public float Timer { get; private set; }

    private AnimatedSprite()
    {
        Animations = new Dictionary<string, Animation>();
    }

    public AnimatedSprite(Texture2D texture)
        : this()
    {
        Texture = texture;
    }

    public void Update(GameTime gameTime)
    {
        Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (CurrentAnimation == null)
            throw new Exception("No animation has been set!");

        AnimationFrame currentFrame = CurrentAnimation.Frames[CurrentAnimation.FrameIndex];
        if (Timer >= currentFrame.Duration)
        {
            CurrentAnimation.FrameIndex++;

            if (CurrentAnimation.FrameIndex >= CurrentAnimation.Frames.Count)
                CurrentAnimation.FrameIndex = 0;

            Timer = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 2.0f)
    {
        if (CurrentAnimation == null)
            throw new Exception("No animation has been set!");

        AnimationFrame currentFrame = CurrentAnimation.Frames[CurrentAnimation.FrameIndex];

        int rows = Texture.Width / currentFrame.Width;

        int rowIndex = currentFrame.SpriteIndex / rows;
        int columnIndex = currentFrame.SpriteIndex % rows;

        spriteBatch.Draw(Texture, position, new Rectangle(columnIndex * currentFrame.Width,
            rowIndex * currentFrame.Height, currentFrame.Width, currentFrame.Height),
            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    public void AddAnimation(string name, Animation animation)
    {
        if (Animations.ContainsKey(name))
            throw new Exception($"An animation with the name {name} already exists!");

        Animations.Add(name, animation);
    }

    public void RemoveAnimation(string name)
    {
        if (!Animations.ContainsKey(name))
            throw new Exception($"No animation with the name {name} exists!");

        Animations.Remove(name);
    }

    public void SetAnimation(string name)
    {
        if (!Animations.ContainsKey(name))
            throw new Exception($"No animation with the name {name} exists!");

        CurrentAnimation = Animations[name];
    }

    public class Animation
    {
        public List<AnimationFrame> Frames { get; set; }
        public int FrameIndex { get; set; }

        public Animation(List<AnimationFrame> frames, int frameIndex = 0)
        {
            Frames = frames;
            FrameIndex = frameIndex;
        }

        public Animation(params AnimationFrame[] frames)
        {
            Frames = new List<AnimationFrame>(frames);
            FrameIndex = 0;
        }

        public void AddFrame(AnimationFrame frame)
        {
            Frames.Add(frame);
            Console.Write("added");
        }
    }

    public class AnimationFrame
    {
        public int SpriteIndex { get; set; }
        public int Duration { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public AnimationFrame(int spriteIndex, int duration, int width = 32, int height = 32)
        {
            SpriteIndex = spriteIndex;
            Duration = duration;
            Width = width;
            Height = height;
        }
    }
}
