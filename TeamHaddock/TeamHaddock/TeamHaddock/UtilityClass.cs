using System;
using Microsoft.Xna.Framework.Input;

namespace TeamHaddock
{
    /// <summary>
    /// Contains small utility methods
    /// </summary>
    internal static class UtilityClass
    {
        /// <summary>
        /// KeyboardState during this update
        /// </summary>
        private static KeyboardState _currentKeyboardState;

        /// <summary>
        /// KeyboardState during last update
        /// </summary>
        private static KeyboardState _previousKeyboardState;

        /// <summary>
        /// Update UtilityClass logic
        /// </summary>
        public static void Update()
        {
            // Set previous KeyboardState
            _previousKeyboardState = _currentKeyboardState;
            // Get current KeyboardState 
            _currentKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Check if key is pressed now but not one update ago
        /// </summary>
        /// <param name="key">key to check</param>
        /// <returns></returns>
        public static bool SingleActivationKey(Keys key)
        {
            // If key is down but was up before
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }
    }
}