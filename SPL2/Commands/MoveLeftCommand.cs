using System;
using Microsoft.Xna.Framework;

namespace SPL2.Commands;

 public class MoveLeftCommand : ICommand
{
    public void Execute(Player player, GameTime gameTime)
    {
        player.Position.X -= player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    } 
}
