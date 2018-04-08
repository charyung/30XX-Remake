using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    interface IUpdatable
    {
        //A class for everything that has an update function, to let Physics call it
        //Apparently XNA has its own version, but it's misspelt but has some other functions
        void Update(GameTime gt);
    }
}
