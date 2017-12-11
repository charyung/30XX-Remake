using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _30XXRemakeRemake
{
    class Omastar : Fighter
    {
        static public List<Animation> animations = new List<Animation>();

        public Omastar(Vector2 position) : base(position, "Textures/omastar2", 1, 51, 44)
        {
            idle = new Animation("Textures/omastar2", 0, 0, 0, 1, 51, 44, "H");
            walking = new Animation("Textures/omastar2", 0, 0, 0, 2, 51, 44, "H");
        }
    }
}
