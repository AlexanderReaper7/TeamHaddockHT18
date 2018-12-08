﻿using System;
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
        public readonly int frameTime;

        /// <summary>
        /// Creates a new frame with a source rectangle and frame time
        /// </summary>
        /// <param name="sourceRectangle">Position of frame in texture</param>
        /// <param name="frameTime">Time between this and next frame in milliseconds</param>
        public Frame(Rectangle sourceRectangle, int frameTime)
        {
            this.sourceRectangle = sourceRectangle;
            this.frameTime = frameTime;
        }
    }

    public class Animation
    {
        public List<Frame> frames;
        private int timeForCurrentFrame;

        /// <summary>
        /// Total time for a single loop in milliseconds
        /// </summary>
        public int TotalFrameTime
        {
            get
            {
                int totalFrameTime = 0;

                for (int frame = 0; frame < frames.Count; frame++)
                {
                    totalFrameTime += frames[frame].frameTime;
                }

                return totalFrameTime;
            }
        }

        public int CurrentFrame { get; private set; }

        public Animation(List<Frame> frames)
        {
            this.frames = frames;
        }

        /// <summary>
        /// Animates through the list of frames
        /// </summary>
        /// <param name="sourceRectangle">source rectangle to apply animation to</param>
        /// <param name="gameTime"></param>
        public void Animate(ref Rectangle sourceRectangle, GameTime gameTime)
        {
            // Update time elapsed for this frame
            timeForCurrentFrame += gameTime.ElapsedGameTime.Milliseconds;
            // If time has passed longer for this frame than this frame´s frameTime
            if (timeForCurrentFrame >= frames[CurrentFrame].frameTime)
            {
                // Go to next frame in frames
                CurrentFrame = (CurrentFrame + 1) % frames.Count;
                // Set sourceRectangle to this frame
                sourceRectangle = frames[CurrentFrame].sourceRectangle;
                // Reset time elapsed
                timeForCurrentFrame = 0;
            }
        }

        public void SetToFrame(ref Rectangle sourceRectangle, int frameToSet)
        {
            // Set animation to first frame
            CurrentFrame = frameToSet % frames.Count;
            // Set sourceRectangle to the first frame
            sourceRectangle = frames[CurrentFrame].sourceRectangle;
            // Reset time elapsed
            timeForCurrentFrame = 0;
        }

        /// <summary>
        /// Resets animation
        /// </summary>
        public void Reset()
        {
            // Set animation to  frame
            CurrentFrame = 0;
            // Reset time elapsed
            timeForCurrentFrame = 0;

        }
    }
}    
