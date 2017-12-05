using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXremake
{
    class stage
    {
        //rect is the size of the stage's hitbox
        //(12,65) -> (335,135) for TT

        private Texture2D img;
        public Rectangle hbRect;

        //initialize at the beginning instead of here? Find out if this is a better idea than making a new stage for every time.
        //In case I forget: The problem here is that the hitbox manager needs to be able to point to hbRect here, but a) it needs be made first (during runtime) to be able to point to it and b)physics.cs needs a copy to point to, but it wouldn't make sense to have an instance of it made in physics.cs (since it would be different fron the one that Game1.cs creates).
        //However, it also wouldn't really make sense for variables here to be static, since there will eventually be more than 1 stage and also it's kinda ugly to directly give the variable values.
        public stage(Texture2D img, Rectangle hbRect)
        {
            this.img = img;
            this.hbRect = hbRect;
        }



        public Texture2D Img
        {
            get { return img; }
            set { img = value; }
        }
    }
}
