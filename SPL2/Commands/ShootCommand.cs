using System;
using GMDCore;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

public class ShootCommand : ICommand
{
    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        Matrix screenScaleMatrix = playState.Game.ScreenScaleMatrix;
        Vector2 playerPosition = entity.Position;
        Vector2 mousePosition = new Vector2(Core.Input.Mouse.GetPosition().X / screenScaleMatrix.M11, Core.Input.Mouse.GetPosition().Y / screenScaleMatrix.M22);

        Vector2 direction = Vector2.Normalize(mousePosition - playerPosition);

        playState.PendingAdd.Add(new Projectile(playState.ProjectileSprite, direction, entity, playState, gameTime));
    }
}
