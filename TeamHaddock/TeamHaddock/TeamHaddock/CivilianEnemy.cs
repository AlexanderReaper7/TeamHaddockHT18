using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamHaddock
{
    class CivilianEnemy
    {
        private CollidableObject collidableObject;
        private static Texture2D colorMap;
        private static Texture2D normalMap;
        private static Texture2D collisionMap;

        public CollidableObject CollidableObject => collidableObject;

        private Animation moveLeftAnimation;
        private Animation moveRightAnimation;

        private Vector2 velocity;
        private Vector2 direction;
        private const int defaultHealth = 1000;
        private int health;

        private const float baseWalkingSpeed = 0.1f, baseJumpStrength = -0.08f;
        private readonly Vector2 maxMovementSpeed = new Vector2(0.5f, 100f);

        public CivilianEnemy(Vector2 position)
        {
            collidableObject = new CollidableObject(collisionMap, position, new Rectangle(120, 0, 98, 114), 0);

            health = (int)(defaultHealth * InGame.difficultyModifier);

            int walkingTime = 200;

            // Load all frames into Animation // Edited by Noble 12-10 
            moveRightAnimation = new Animation(new List<Frame>
                {

                }
            );
            moveLeftAnimation = new Animation(new List<Frame>
                {

                }
            );
        }


    }
}
