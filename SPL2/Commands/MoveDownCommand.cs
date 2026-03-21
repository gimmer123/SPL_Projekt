using System;
using Microsoft.Xna.Framework;

namespace SPL2.Commands;

public class MoveDownCommand : ICommand
{
    public void Execute(Player player, GameTime gameTime)
    {
        player.Position.Y += player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}
