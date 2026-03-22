using System;
using Microsoft.Xna.Framework;
using SPL2.Entities;
using SPL2.States.GameStates;

namespace SPL2.Commands;

public interface ICommand
{
    void Execute(IEntity entity, GameTime gameTime, PlayState playState);
}
