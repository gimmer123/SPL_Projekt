using GMDCore;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.Entities.Projectile;
using SPL2.States.GameStates;

namespace SPL2.Commands.EnemyShootCommand;

public class EnemyShootCommand : ICommand
{
    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        Vector2 enemyPosition = entity.Position;

        Vector2 direction = Vector2.Normalize(playState.Player.Position - enemyPosition);

        playState.PendingAdd.Add(new EnemyProjectile(playState.ProjectileSprite, direction, entity, playState, gameTime));
    }
}