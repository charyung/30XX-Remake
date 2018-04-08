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

            idle = new Animation(charTextures[0], new Rectangle(0, 0, 51, 44), 1, "H", false);
            walking = new Animation(charTextures[0], new Rectangle(0, 0, 51, 44), 2, "H", true, 200f);

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
    
        protected override void NeutralB()
        {
            //Rock Blast: Projectile
            cdLength = 500;

            ProjectileMove nb;

            //Here we spawn the attack according to the direction that the Omastar was facing when he used the attack.
            //The spritebatch draws are different because the left facing sprite has to be flipped first.
            if (facing == "Left")
            {
                nb = new ProjectileMove(new Vector2(-0.01f, 0), "Left", charTextures[1], new Rectangle((int)position.X - 25, (int)position.Y + 14, 30, 30), new Rectangle((int)position.X - 25, (int)position.Y + 14, 30, 30), 6, "H", this, 20, 10, Math.PI - Math.PI / 6, true);
				Drawer.AddToDrawList(nb, true);
            }
            else
            {
                nb = new ProjectileMove(new Vector2(0.01f, 0), "Right", charTextures[1], new Rectangle((int)position.X + 46, (int)position.Y + 14, 30, 30), new Rectangle((int)position.X + 46, (int)position.Y + 14, 30, 30), 6, "H", this, 20, 10, Math.PI / 6, true);
				Drawer.AddToDrawList(nb, false);
            }

			Physics.AddToUpdateList(nb);
		}

        protected override void SideB()
        {
            cdLength = 200;

            MeleeMove nb;

            if (facing == "Left")
            {
				nb = new MeleeMove("Left", charTextures[2], new Rectangle((int)position.X + 14, (int)position.Y + 35, 5, 10), new Rectangle((int)position.X + 14, (int)position.Y + 35, 5, 10), 6, "V", this, 20, 10, Math.PI / 6, true);
				Drawer.AddToDrawList(nb, true);
            }
			else
			{
				nb = new MeleeMove("Left", charTextures[2], new Rectangle((int)position.X + 36, (int)position.Y + 35, 5, 10), new Rectangle((int)position.X + 36, (int)position.Y + 35, 5, 10), 6, "V", this, 20, 10, Math.PI / 6, true);
				Drawer.AddToDrawList(nb, false);
			}

			Physics.AddToUpdateList(nb);
		}
    }
}
