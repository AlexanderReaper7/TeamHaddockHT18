using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamHaddock
{
    public class GameObject
    {
        public static CollidableObject ground; 
        // reference 
        //// public void LoadContent(ContentManager content)
        //{
        //    // Create a new collidableObject
        //    collidableObject = new CollidableObject(content.Load<Texture2D>(@"Textures/Player"), new Vector2(250, 500), new Rectangle(60, 0, 60, 120), 0);
        //}



        public void LoadContent(ContentManager content)
        {
            ground = new CollidableObject(content.Load<Texture2D>(@"Textures/Ground"), new Vector2(-500, 520), new Rectangle(0, 0, 1780, 200), 0);
        }
    }
}
