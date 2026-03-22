using System;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

 public class MoveLeftCommand : ICommand
{
    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        float newPositionX = entity.Position.X - entity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        entity.Position = new(newPositionX, entity.Position.Y);
    } 
}
