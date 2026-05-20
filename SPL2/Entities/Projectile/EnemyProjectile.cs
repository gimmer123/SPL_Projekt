using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SPL2.States.GameStates;

namespace SPL2.Entities.Projectile
{
    public class EnemyProjectile(Sprite sprite, Vector2 direction, IEntity origin, PlayState playState, GameTime gameTime) 
                       : ProjectileBase(sprite, direction, origin, playState, gameTime)
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Intersects(_playState.Player.Collider))
            {
                _playState.Player.TakeDamage();
                Remove = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Color = Color.Red;
            base.Draw(spriteBatch);
        }
    }
}