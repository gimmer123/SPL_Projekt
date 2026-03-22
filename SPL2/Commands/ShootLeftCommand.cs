using System;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

public class ShootLeftCommand : ICommand
{
    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        playState.PendingAdd.Add(new Projectile(playState.ProjectileSprite, new MoveLeftCommand(), entity, playState, gameTime));
    }
}
