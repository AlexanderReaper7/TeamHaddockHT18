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
        private readonly Vector2 selectionMax;

        /// <summary>
        ///     Selected menu option
        /// </summary>
        private Vector2 selected;

        /// <summary>
        ///     Creates a new instance of MenuControls
        /// </summary>
        /// <param name="selectionMax">Number of menu options</param>
        public MenuControls(Vector2 selectionMax)
        {
            this.selectionMax = selectionMax;
        }


        /// <summary>
        ///     returns selected
        /// </summary>
        public Vector2 Selected => selected;

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
            {
                if (selected.Y > 0)
                {
                    selected.Y--;
                }
            }

            // When A or Left arrow keys are pressed
            if (UtilityClass.SingleActivationKey(Keys.A) || UtilityClass.SingleActivationKey(Keys.Left))
            {
                if (selected.X > 0)
                {
                    selected.X--;
                }
            }

            // When S or Down arrow keys are pressed
            if (UtilityClass.SingleActivationKey(Keys.S) || UtilityClass.SingleActivationKey(Keys.Down))
            {
                if (selected.Y < selectionMax.Y)
                {
                    selected.Y++;
                }
            }

            // When D or Right arrow keys are pressed
            if (UtilityClass.SingleActivationKey(Keys.D) || UtilityClass.SingleActivationKey(Keys.Right))
            {
                // And selected.X is LESS THAN selectionMax.X, preventing it from exceeding maximum X selection range, 
                if (selected.X < selectionMax.X)
                {
                    selected.X++;
                }

            }
                // Update Enterkey
                IsEnterDown = UtilityClass.SingleActivationKey(Keys.Enter);

                // Update
                IsEscapeDown = UtilityClass.SingleActivationKey(Keys.Escape);

            // Return updated selected
            return selected;
        }
    }
}