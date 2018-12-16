using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    class Omastar : Fighter
    {
	    private readonly List<Texture2D> charTextures = new List<Texture2D>();

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
            charTextures.Add(Content.Load<Texture2D>("Textures/whirlpool"));
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

	        MeleeMove sb;

			//The plan for MeleeMoves is to make it so we can make the hitbox rectangles here go from 10 to 60, 10 at a time.
            if (facing == "Left")
            {
				sb = new MeleeMove("Left", charTextures[2], new Rectangle((int)position.X - 46, (int)position.Y + 35, 60, 5), new Rectangle((int)position.X + 14, (int)position.Y + 35, 10, 5), 6, "V", this, 20, 10, Math.PI / 6, true);
				Drawer.AddToDrawList(sb, true);
            }
			else
			{
				sb = new MeleeMove("Left", charTextures[2], new Rectangle((int)position.X + 36, (int)position.Y + 35, 60, 5), new Rectangle((int)position.X + 36, (int)position.Y + 35, 10, 5), 6, "V", this, 20, 10, Math.PI / 6, true);
				Drawer.AddToDrawList(sb, false);
			}

			Physics.AddToUpdateList(sb);
		}

	    protected override void DownB()
	    {
		    cdLength = 700;

		    this.paused = true;

			MeleeMove db = new MeleeMove("Right", charTextures[4], new Rectangle((int)position.X - 35, (int)position.Y + 34, 120, 10), new Rectangle((int)position.X - 35, (int)position.Y, 120, 10), 13, "V", this, 30, 10, Math.PI/6, true);

		    Drawer.AddToDrawList(db, false);
		    Physics.AddToUpdateList(db);
		}

	}
}
