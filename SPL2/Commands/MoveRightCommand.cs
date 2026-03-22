using System;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

public class MoveRightCommand : ICommand
{
    const float MAX_X = Game1.VIRTUAL_WIDTH * 0.75f;

    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        float newPositionX = entity.Position.X + entity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (newPositionX > MAX_X)
        {
            float overflow = newPositionX - MAX_X;
            newPositionX = MAX_X;
            playState.Floor.MoveX(overflow);
            playState.Floor.Update(gameTime);
        }

        entity.Position = new(newPositionX, entity.Position.Y); 
    }
}
