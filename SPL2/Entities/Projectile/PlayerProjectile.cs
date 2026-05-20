using System.Collections.Generic;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SPL2.Entities;
using SPL2.Entities.Projectile;
using SPL2.States.GameStates;

namespace SPL2.Projectile.PlayerProjectile;

public class PlayerProjectile(Sprite sprite, Vector2 direction, IEntity origin, PlayState playState, GameTime gameTime) 
                    : ProjectileBase(sprite, direction, origin, playState, gameTime)
{
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_playState.IntersectsEnemy(Collider, out List<BaseEnemy> enemiesHit))
        {
            foreach (BaseEnemy enemy in enemiesHit)
            {
                enemy.TakeDamage();
                Remove = true;
            }
        }  
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        sprite.Color = Color.Yellow;
        base.Draw(spriteBatch);
    }
}