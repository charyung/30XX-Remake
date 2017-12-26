using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace _30XXRemakeRemake
{
    class Omastar : Fighter
    {

        private List<Texture2D> charTextures = new List<Texture2D>();

        public Omastar(Vector2 position, ContentManager Content) : base(position, 51, 44, 1)
        {
            LoadTextures(Content);

            idle = new Animation(charTextures[0], new Rectangle(0, 0, 51, 44), 1, "H");
            walking = new Animation(charTextures[0], new Rectangle(0, 0, 51, 44), 2, "H", 200f);

            //NeutralB: Rock Blast; Projectile
            //SideB: Scald; Ranged
            //UpB: Surf; Ranged
            //DownB: Whirlpool; Ranged
        }

        public void LoadTextures(ContentManager Content)
        {

            //Walking/idle
            charTextures.Add(Content.Load<Texture2D>("Textures/omastar2"));
            //NeutralB
            charTextures.Add(Content.Load<Texture2D>("Textures/rockBlast"));
            //SideB
            charTextures.Add(Content.Load<Texture2D>("Textures/scald"));
            //UpB
            charTextures.Add(Content.Load<Texture2D>("Textures/surf"));
            //DownB
            charTextures.Add(Content.Load<Texture2D>("Textures/whirlpool2"));
        }

        private void NeutralB(GameTime gt)
        {
            //Rock Blast: Projectile
            cdLength = 1;
            onCooldown = true;

            if (facing == "Left")
            {
                //new ProjectileMove(-30, "Left", "Textures/RockBlast", new Rectangle((int)position.X - 25, (int)position.Y + 14, 30, 30), 6, "H", this, 20, 10, Math.PI / 6, true);
            }
            
        }
    }
}
