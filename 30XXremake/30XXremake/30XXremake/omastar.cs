using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXremake
{
    class omastar : fighter
    {
        static public List<animation> animations = new List<animation>();
        static private ContentManager content;

        public omastar(Vector2 pos, ContentManager cont) : base(pos, "textures/omastar2", 1, 51, 44)
        {
            content = cont;
        }

        static public void addToAnimationsList()
        {
            animations.Add(new animation(content.Load<Texture2D>("textures/omastar2"), 1, 2, 51, 44));
            animations.Add(new animation(content.Load<Texture2D>("textures/omastar2"), 1, 1, 51, 44));
        }
    }
}
