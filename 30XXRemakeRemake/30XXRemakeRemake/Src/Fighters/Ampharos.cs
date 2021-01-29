using System;
using System.Collections.Generic;
using _30XXRemakeRemake.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    internal class Ampharos : Fighter
	{
		private readonly Dictionary<ActionTypes, Animation> _charTextures = new Dictionary<ActionTypes, Animation>(); // The textures that the character uses while taking each action, such as a punching motion
		private readonly Dictionary<ActionTypes, Texture2D> _attackTextures = new Dictionary<ActionTypes, Texture2D>(); // The textures that attack uses, such as electric bubbles

		public Ampharos(bool isPlayer, Vector2 position, ContentManager Content) : base(isPlayer, position, 60, 61, 4, 2, -10, 2)
		{
			LoadTextures(Content);

			idle = _charTextures[ActionTypes.Idle];
			walking = _charTextures[ActionTypes.Walking];
			currAnimation = _charTextures[ActionTypes.NeutralSp];

			//NeutralB: Thunder Punch; Melee
			//SideB: Thunder; Melee
			//UpB: Magnet Rise; Melee
			//DownB: Charge Beam; Melee
		}

		public void LoadTextures(ContentManager Content)
		{
			_attackTextures.Add(ActionTypes.Idle, Content.Load<Texture2D>("Textures/ampharos"));
			_attackTextures.Add(ActionTypes.NeutralSp, Content.Load<Texture2D>("Textures/thunderPunch"));
			_attackTextures.Add(ActionTypes.SideSp, Content.Load<Texture2D>("Textures/thunder2"));
			_attackTextures.Add(ActionTypes.UpSp, Content.Load<Texture2D>("Textures/magnetRise"));
			_attackTextures.Add(ActionTypes.DownSp, Content.Load<Texture2D>("Textures/chargeBeamFire"));

			_charTextures.Add(ActionTypes.Idle, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 0, 60, 61), 1, "H", false));
			_charTextures.Add(ActionTypes.Walking, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 0, 60, 61), 2, "H", true, 200f));
			_charTextures.Add(ActionTypes.NeutralSp, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 64, 60, 61), 2, "H", false, 300f));
			_charTextures.Add(ActionTypes.SideSp, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 128, 60, 64), 2, "H", false, 220f));
			_charTextures.Add(ActionTypes.UpSp, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 0, 60, 61), 1, "H", false));
			_charTextures.Add(ActionTypes.DownSp, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 128, 60, 64), 2, "H", false));
		}

		protected override void NeutralB()
		{
			// Thunder Punch; Melee
			cdTimer = 700;

			string direction = facing;
			int positionX = (int)Position.X + (facing == "Left" ? -2 : 2);
			int positionY = (int)Position.Y + 1;
			Rectangle atkPosition = new Rectangle(positionX, positionY, 60, 36);
			Rectangle atkHitbox = atkPosition;
			double kbAngle = facing == "Left" ? 5 * Math.PI / 6 : Math.PI / 6;

			MeleeAttack nb = new MeleeAttack(direction, _attackTextures[ActionTypes.NeutralSp], atkPosition, atkHitbox, 2, "H", this, 30, 5, kbAngle, true, 300f);

			activeAttacks.Add((ActionTypes.NeutralSp, nb));

			state = FighterStates.Paused;
			currAnimation = _charTextures[ActionTypes.NeutralSp];
		}

		protected override void SideB()
		{

			// Thunder; Melee
			cdTimer = 500;

			string direction = facing;
			int positionX = (int) Position.X + (facing == "Left" ? -65 : 65);
			int positionY = (int) Position.Y + 20;
			Rectangle atkPosition = new Rectangle(positionX, positionY, 65, 14);
			Rectangle atkHitbox = atkPosition;
			double kbAngle = facing == "Left" ? Math.PI : 0;

			MeleeAttack nb = new MeleeAttack(direction, _attackTextures[ActionTypes.SideSp], atkPosition, atkHitbox, 8, "V", this, 30, 5, kbAngle, true);

			activeAttacks.Add((ActionTypes.SideSp, nb));

			state = FighterStates.Paused;
			currAnimation = _charTextures[ActionTypes.SideSp];
		}

		protected override void UpB()
		{
			// Magnet Rise; Melee
			cdTimer = 500;

			string direction = facing;
			int positionX = (int)Position.X + (facing == "Left" ? -5 : 5);
			int positionY = (int)Position.Y;
			Rectangle atkPosition = new Rectangle(positionX, positionY, 50, 60);
			Rectangle atkHitbox = atkPosition;

			MeleeAttack nb = new MeleeAttack(direction, _attackTextures[ActionTypes.UpSp], atkPosition, atkHitbox, 10, "H", this, 30, 0, 0, true);

			activeAttacks.Add((ActionTypes.UpSp, nb));
			state = FighterStates.Helpless;
			currAnimation = _charTextures[ActionTypes.UpSp];

			vel.Y = -10;
		}

		protected override void DownB()
		{
			// Magnet Rise; Melee
			cdTimer = 500;

			string direction = facing;
			int positionX = (int)Position.X + (facing == "Left" ? -105 : 65);
			int positionY = (int)Position.Y;
			Rectangle atkPosition = new Rectangle(positionX, positionY, 100, 20);
			Rectangle atkHitbox = atkPosition;
			double kbAngle = facing == "Left" ? Math.PI : 0;

			MeleeAttack nb = new MeleeAttack(direction, _attackTextures[ActionTypes.DownSp], atkPosition, atkHitbox, 5, "V", this, 30, 5, kbAngle, true);

			activeAttacks.Add((ActionTypes.DownSp, nb));

			state = FighterStates.Paused;
			currAnimation = _charTextures[ActionTypes.DownSp];
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
					_charTextures[type].Reset();
					state = FighterStates.Normal;
					currAnimation = idle;
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
