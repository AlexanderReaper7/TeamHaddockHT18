using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// Created by Alexander 11-25
namespace TeamHaddock
{
    public struct Frame
    {
        public readonly Rectangle sourceRectangle;
        public readonly float frameTime;

        /// <summary>
        /// Creates a new frame with a source rectangle and frame time
        /// </summary>
        /// <param name="sourceRectangle">Position of frame in texture</param>
        /// <param name="frameTime">Time between this and next frame in milliseconds</param>
        public Frame(Rectangle sourceRectangle, float frameTime)
        {
            this.sourceRectangle = sourceRectangle;
            this.frameTime = frameTime;
        }
    }

    public class Animation
    {
        public List<Frame> frames;
        private float timeForCurrentFrame;
        private int currentFrame;

        /// <summary>
        /// Animates through the list of frames
        /// </summary>
        /// <param name="sourceRectangle">source rectangle to apply animation to</param>
        /// <param name="gameTime"></param>
        public void Animate(ref Rectangle sourceRectangle, GameTime gameTime)
        {
            timeForCurrentFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeForCurrentFrame >= frames[currentFrame].frameTime)
            {
                currentFrame = (currentFrame + 1) % frames.Count;
                sourceRectangle = frames[currentFrame].sourceRectangle;
                timeForCurrentFrame = 0.0f;
            }
        }
    }
}    
