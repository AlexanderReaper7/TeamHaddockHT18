using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TeamHaddock
{
    /// <summary>
    ///     An object used for easy control in menus using the keyboard
    /// </summary>
    internal class MenuControls
    {
        /// <summary>
        ///     Number of menu options
        /// </summary>
        private readonly Vector2 _selectionMax;

        /// <summary>
        ///     Selected menu option
        /// </summary>
        private Vector2 _selected;

        /// <summary>
        ///     Creates a new instance of MenuControls
        /// </summary>
        /// <param name="selectionMax">Number of menu options</param>
        public MenuControls(Vector2 selectionMax)
        {
            _selectionMax = selectionMax;
        }


        /// <summary>
        ///     returns selected
        /// </summary>
        public Vector2 Selected => _selected;

        public bool IsEnterDown { get; private set; }

        public bool IsEscapeDown { get; set; }

        /// <summary>
        ///     Updates selected menu option in menus
        /// </summary>
        /// <returns>New Vector2 position</returns>
        public Vector2 Update()
        {
            // When W or UP arrow keys are pressed
            if (UtilityClass.SingleActivationKey(Keys.W) || UtilityClass.SingleActivationKey(Keys.Up))
                if (_selected.Y > 0)
                    _selected.Y--;

            // When A or Left arrow keys are pressed
            if (UtilityClass.SingleActivationKey(Keys.A) || UtilityClass.SingleActivationKey(Keys.Left))
                if (_selected.X > 0)
                    _selected.X--;

            // When S or Down arrow keys are pressed
            if (UtilityClass.SingleActivationKey(Keys.S) || UtilityClass.SingleActivationKey(Keys.Down))
                if (_selected.Y < _selectionMax.Y)
                    _selected.Y++;

            // When D or Right arrow keys are pressed
            if (UtilityClass.SingleActivationKey(Keys.D) || UtilityClass.SingleActivationKey(Keys.Right))
            {
                // And selected.X is LESS THAN selectionMax.X, preventing it from exceeding maximum X selection range, 
                if (_selected.X < _selectionMax.X) _selected.X++;

                // if Enter is pressed
                if (UtilityClass.SingleActivationKey(Keys.Enter))
                    IsEnterDown = true;
                else
                    IsEnterDown = false;

                // If Escape pressed
                if (UtilityClass.SingleActivationKey(Keys.Escape))
                    IsEscapeDown = true;
                else
                    IsEscapeDown = false;
            }

            // Return updated selected
            return _selected;
        }
    }
}