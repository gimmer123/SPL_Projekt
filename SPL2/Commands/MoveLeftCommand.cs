using System;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

 public class MoveLeftCommand : ICommand
{
        const float MIN_X = Game1.VIRTUAL_WIDTH * 0.25f;
    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        float newPositionX = entity.Position.X - entity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (newPositionX < MIN_X && entity.GetType() == typeof(Player))
        {
            float overflow = newPositionX - MIN_X;
            newPositionX = MIN_X;
            playState.Floor.MoveX(overflow);
            playState.Floor.Update(gameTime);
        }
        entity.Position = new(newPositionX, entity.Position.Y);
    } 
}
