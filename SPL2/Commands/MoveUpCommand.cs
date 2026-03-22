using System;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

public class MoveUpCommand : ICommand
{
    public const float MIN_Y = Game1.VIRTUAL_HEIGHT * 0.25f;
    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        float newPositionY = entity.Position.Y - entity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (newPositionY < MIN_Y && entity.GetType() == typeof(Player))
        {
            float overflow = newPositionY - MIN_Y;
            newPositionY = MIN_Y;
            playState.Floor.MoveY(overflow);
            playState.Floor.Update(gameTime);
        }

        entity.Position = new(entity.Position.X, newPositionY); 
    }
}
