using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    class Omastar : Fighter
    {
	    private readonly List<Texture2D> _charTextures = new List<Texture2D>();

	    public Omastar(Vector2 position, ContentManager Content) : base(position, 51, 44, 1)
        {
            LoadTextures(Content);

            idle = new Animation(_charTextures[0], new Rectangle(0, 0, 51, 44), 1, "H", false);
            walking = new Animation(_charTextures[0], new Rectangle(0, 0, 51, 44), 2, "H", true, 200f);

            //NeutralB: Rock Blast; Projectile
            //SideB: Scald; Ranged
            //UpB: Surf; Ranged
            //DownB: Whirlpool; Ranged
        }

	    public void LoadTextures(ContentManager Content)
        {

            //Walking/idle
            _charTextures.Add(Content.Load<Texture2D>("Textures/omastar2"));
            //NeutralB
            _charTextures.Add(Content.Load<Texture2D>("Textures/rockBlast"));
            //SideB
            _charTextures.Add(Content.Load<Texture2D>("Textures/scald"));
            //UpB
            _charTextures.Add(Content.Load<Texture2D>("Textures/surf2"));
            //DownB
            _charTextures.Add(Content.Load<Texture2D>("Textures/whirlpool"));
        }

	    protected override void NeutralB()
        {
            //Rock Blast: Projectile
            cdLength = 500;

	        onCooldown = true;

			ProjectileAttack nb;

            //Here we spawn the attack according to the direction that the Omastar was facing when he used the attack.
            //The spritebatch draws are different because the left facing sprite has to be flipped first.
            if (facing == "Left")
            {
                nb = new ProjectileAttack(new Vector2(-0.01f, 0), "Left", _charTextures[1], new Rectangle((int)Position.X - 25, (int)Position.Y + 14, 30, 30), new Rectangle((int)Position.X - 25, (int)Position.Y + 14, 30, 30), 6, "H", this, 20, 10, Math.PI - Math.PI / 6, true);
				Drawer.AddToDrawList(nb, true);
            }
            else
            {
                nb = new ProjectileAttack(new Vector2(0.01f, 0), "Right", _charTextures[1], new Rectangle((int)Position.X + 46, (int)Position.Y + 14, 30, 30), new Rectangle((int)Position.X + 46, (int)Position.Y + 14, 30, 30), 6, "H", this, 20, 10, Math.PI / 6, true);
				Drawer.AddToDrawList(nb, false);
            }

			Physics.AddToUpdateList(nb);
		}

	    protected override void SideB()
        {
            cdLength = 200;

	        onCooldown = true;
	        paused = true;

	        MeleeAttack sb;

			//The plan for MeleeMoves is to make it so we can make the hitbox rectangles here go from 10 to 60, 10 at a time.
			if (facing == "Left")
            {
				sb = new MeleeAttack("Left", _charTextures[2], new Rectangle((int)Position.X - 46, (int)Position.Y + 35, 60, 5), new Rectangle((int)Position.X + 14, (int)Position.Y + 35, 10, 5), 6, "V", this, 20, 10, Math.PI / 6, true);
				Drawer.AddToDrawList(sb, true);
            }
			else
			{
				sb = new MeleeAttack("Right", _charTextures[2], new Rectangle((int)Position.X + 36, (int)Position.Y + 35, 60, 5), new Rectangle((int)Position.X + 36, (int)Position.Y + 35, 10, 5), 6, "V", this, 20, 10, Math.PI / 6, true);
				Drawer.AddToDrawList(sb, false);
			}

			Physics.AddToUpdateList(sb);
		}

	    protected override void UpB()
	    {
		    cdLength = 100;

		    MeleeAttack ub;

		    if (facing == "Left")
		    {
			    ub = new MeleeAttack("Left", _charTextures[3], new Rectangle((int)Position.X + 10, (int)Position.Y - 8, 30, 60), new Rectangle((int)Position.X + 10, (int)Position.Y - 8, 30, 60), 12, "H", this, 5, 70, 180, false);
			    Drawer.AddToDrawList(ub, true);
			}
		    else
		    {
				ub = new MeleeAttack("Right", _charTextures[3], new Rectangle((int)Position.X + 10, (int)Position.Y - 8, 30, 60), new Rectangle((int)Position.X + 10, (int)Position.Y - 8, 30, 60), 12, "H", this, 5, 70, 180, false);
			    Drawer.AddToDrawList(ub, false);
			}

		    Physics.AddToUpdateList(ub);

			vel = new Vector2(0, -1f);
		}

	    protected override void DownB()
	    {
		    cdLength = 700;

		    this.paused = true;

			MeleeAttack db = new MeleeAttack("Right", _charTextures[4], new Rectangle((int)Position.X - 35, (int)Position.Y + 34, 120, 10), new Rectangle((int)Position.X - 35, (int)Position.Y, 120, 10), 13, "V", this, 30, 10, Math.PI/6, true);

		    Drawer.AddToDrawList(db, false);
		    Physics.AddToUpdateList(db);
		}

	}
}
