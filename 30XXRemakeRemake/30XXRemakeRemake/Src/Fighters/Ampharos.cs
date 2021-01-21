using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    internal class Ampharos : Fighter
	{
		private readonly Dictionary<ActionTypes, Animation> _charTextures = new Dictionary<ActionTypes, Animation>(); // The textures that the character uses while taking each action, such as a punching motion
		private readonly Dictionary<ActionTypes, Texture2D> _attackTextures = new Dictionary<ActionTypes, Texture2D>(); // The textures that attack uses, such as electric bubbles

		public Ampharos(Vector2 position, ContentManager Content) : base(position, 51, 44, 1)
		{
			LoadTextures(Content);

			idle = _charTextures[ActionTypes.Idle];
			walking = _charTextures[ActionTypes.Walking];
			currAnimation = _charTextures[ActionTypes.NeutralSp];

			//NeutralB: Thunder Punch; Melee
			//SideB: Thunder; Ranged
			//UpB: Magnet Rise; Ranged
			//DownB: Charge Beam; Ranged
		}

		public void LoadTextures(ContentManager Content)
		{
			_attackTextures.Add(ActionTypes.Idle, Content.Load<Texture2D>("Textures/ampharos"));
			_attackTextures.Add(ActionTypes.NeutralSp, Content.Load<Texture2D>("Textures/thunderPunch"));
			_attackTextures.Add(ActionTypes.SideSp, Content.Load<Texture2D>("Textures/thunder2"));
			_attackTextures.Add(ActionTypes.UpSp, Content.Load<Texture2D>("Textures/magnetRise"));
			_attackTextures.Add(ActionTypes.DownSp, Content.Load<Texture2D>("Textures/chargeBeam"));

			_charTextures.Add(ActionTypes.Idle, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 0, 60, 61), 1, "H", false));
			_charTextures.Add(ActionTypes.Walking, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 0, 60, 61), 2, "H", true, 200f));
			_charTextures.Add(ActionTypes.NeutralSp, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 64, 60, 61), 2, "H", false, 300f));
			_charTextures.Add(ActionTypes.SideSp, new Animation(_attackTextures[ActionTypes.Idle], new Rectangle(0, 64, 60, 61), 2, "H", false));
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

			MeleeAttack nb = new MeleeAttack(direction, _attackTextures[ActionTypes.NeutralSp], atkPosition, atkHitbox, 2, "H", this, 30, 20, 0, true, 300f);

			activeAttacks.Add((ActionTypes.NeutralSp, nb));

			state = FighterStates.Paused;
			currAnimation = _charTextures[ActionTypes.NeutralSp];
		}

		protected override void SideB()
		{
			throw new NotImplementedException();
		}

		protected override void UpB()
		{
			throw new NotImplementedException();
		}

		protected override void DownB()
		{
			throw new NotImplementedException();
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
						if (attack.SpriteTexture.Finished)
						{
							activeAttacks.RemoveAt(i);
							removed = true;
							_charTextures[ActionTypes.NeutralSp].Reset();
							state = FighterStates.Normal;
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
