using System;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

public class MoveDownCommand : ICommand
{
    public const float MAX_Y = Game1.VIRTUAL_HEIGHT * 0.75f;
    public void Execute(IEntity entity, GameTime gameTime, PlayState playState)
    {
        float newPositionY = entity.Position.Y + entity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (newPositionY > MAX_Y && entity.GetType() == typeof(Player))
        {
            float overflow = newPositionY - MAX_Y;
            newPositionY = MAX_Y;
            playState.Floor.MoveY(overflow);
            playState.Floor.Update(gameTime);
        }
        entity.Position = new(entity.Position.X, newPositionY);
    }
}
