using System;
using Microsoft.Xna.Framework.Input;

namespace Missile_Master_2
{
    /// <summary>
    ///     An object used in MenuControls,
    ///     Prevents spam when pressing buttons in menus
    /// </summary>
    internal static class MenuKey
    {
        /// <summary>
        ///     
        /// </summary>
        private static KeyboardState _currentKeyboardState;

        private static KeyboardState _previousKeyboardState;

        /// <summary>
        ///     Update MenuKey logic
        /// </summary>
        /// <returns>Key is pressed</returns>
        public static void Update()
        {
            _previousKeyboardState = _currentKeyboardState;

            // Get current keyboard state 
            _currentKeyboardState = Keyboard.GetState();
        }

        public static bool SingleActivationKey(Keys key)
        {
            // If key is down but was up before
            if (_currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}