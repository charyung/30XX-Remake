using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    internal class Omastar : Fighter
    {
	    private readonly List<Texture2D> _charTextures = new List<Texture2D>();

	    public Omastar(bool isPlayer, Vector2 position, ContentManager Content) : base(isPlayer, position, 51, 44, 4, 2, -13, 2)
        {
            LoadTextures(Content);

            idle = new Animation(_charTextures[0], new Rectangle(0, 0, 51, 44), 1, "H", false);
            walking = new Animation(_charTextures[0], new Rectangle(0, 0, 51, 44), 2, "H", true, 200f);

            currAnimation = idle;

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
            cdTimer = 500;

            int velX = facing == "Left" ? -1 : 1;
			float accelX = facing == "Left" ? -0.01f : 0.01f;
			int positionX = (int)Position.X + (facing == "Left" ? -25 : 46);
			int positionY = (int)Position.Y + 20;
			Rectangle atkPosition = new Rectangle(positionX, positionY, 30, 30);
			Rectangle atkHitbox = atkPosition;
			double kbAngle = facing == "Left" ? 5 * Math.PI / 6 : Math.PI / 6;

			//Here we spawn the attack according to the direction that the Omastar was facing when he used the attack.
			//The spritebatch draws are different because the left facing sprite has to be flipped first.
            ProjectileAttack nb = new ProjectileAttack(new Vector2(velX, 0), new Vector2(accelX, 0), "Left", _charTextures[1], atkPosition, atkHitbox, 6, "H", this, 20, 5, kbAngle, true);

			activeAttacks.Add((ActionTypes.NeutralSp, nb));
		}

	    protected override void SideB()
        {
            cdTimer = 200;

            state = FighterStates.Paused;

            string direction = facing;
	        int positionX = (int)Position.X + (facing == "Left" ? -46 : 36);
	        int positionY = (int)Position.Y + 35;
	        Rectangle atkPosition = new Rectangle(positionX, positionY, 60, 5);
	        Rectangle atkHitbox = atkPosition;
	        double kbAngle = facing == "Left" ? 5 * Math.PI / 6 : Math.PI / 6;

			//The plan for MeleeMoves is to make it so we can make the hitbox rectangles here go from 10 to 60, 10 at a time.
			MeleeAttack sb = new MeleeAttack(direction, _charTextures[2], atkPosition, atkHitbox, 6, "V", this, 20, 5, kbAngle, true);
			activeAttacks.Add((ActionTypes.SideSp, sb));
		}

	    protected override void UpB()
	    {
		    cdTimer = 100;

		    state = FighterStates.Helpless;

		    string direction = facing;
		    int positionX = (int)Position.X + 10;
		    int positionY = (int)Position.Y - 8;
		    Rectangle atkPosition = new Rectangle(positionX, positionY, 30, 60);
		    Rectangle atkHitbox = atkPosition;
		    double kbAngle = Math.PI / 2;

			MeleeAttack ub = new MeleeAttack(direction, _charTextures[3], atkPosition, atkHitbox, 12, "H", this, 5, 7, kbAngle, false);

		    activeAttacks.Add((ActionTypes.SideSp, ub));

			vel.Y = -15;
		}

	    protected override void DownB()
	    {
		    cdTimer = 700;

		    state = FighterStates.Paused;

			MeleeAttack db = new MeleeAttack("Right", _charTextures[4], new Rectangle((int)Position.X - 35, (int)Position.Y + 34, 120, 10), new Rectangle((int)Position.X - 35, (int)Position.Y, 120, 10), 13, "V", this, 30, 10, Math.PI/6, true);

			activeAttacks.Add((ActionTypes.SideSp, db));
		}

	    public override void Update(GameTime gt)
	    {
			base.Update(gt);

			int i = 0;

			while (i < activeAttacks.Count)
			{
				(ActionTypes type, Attack attack) = activeAttacks[i];

				bool removed = false;

				switch (type)
				{
					case ActionTypes.NeutralSp:
						if (attack.Position.X > Game1.SCREEN_WIDTH || attack.Position.X < 0)
						{
							activeAttacks.RemoveAt(i);
							removed = true;
						}

						break;
					default:
						if (attack.SpriteTexture.Finished)
						{
							activeAttacks.RemoveAt(i);
							removed = true;
						}

						break;
				}

				if (removed)
				{
					attack.Cleanup();
				}
				else
				{
					attack.Update(gt);
					i++;
				}
			}
	    }
	}
}
