using System;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

public class MoveUpCommand : ICommand
{
    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        float newPositionY = entity.Position.Y - entity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        entity.Position = new(entity.Position.X, newPositionY); 
    }
}
