using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParapluieBulgare.Code
{
    class NPC : Character
    {
        public static SoundEffect soundBulgared;
        public bool Target { get; set; } = false;

        private List<HintsEnum> unlockConditions = null;

        public NPC(Animation idle, Animation walk, Texture2D face, int x = 500) : base(idle, walk, face)
        {
            this.x = x;
            y = 2 * Game1.HEIGHT / 3 - width - 20;
        }
        public virtual void Die(List<NPC> npcs)
        {
            Console.WriteLine("Argh! Je suis mort !");
            soundBulgared.CreateInstance().Play();
            npcs.Remove(this);

            if (!Target)
            {
                Game1.ThreatLevel++;
            }
            else
            {
                Game1.Win = true;
            }
        }

        public void SetUnlockingConditions(List<HintsEnum> hints)
        {
            unlockConditions = hints;
        }

        public bool Blocks(Player player)
        {
            if (unlockConditions == null) return false;
            return !player.CheckHints(unlockConditions);
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            base.Update(keyState, prevKeyState);
        }

        public override void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            currentAnimation.Draw(spriteBatch, new Rectangle(x - cameraX, y, width, width), Flip);
            base.Draw(spriteBatch, cameraX);
        }
    }
}
