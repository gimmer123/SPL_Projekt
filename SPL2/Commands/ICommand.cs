using System;
using Microsoft.Xna.Framework;

namespace SPL2.Commands;

interface ICommand
{
    void Execute(Player player, GameTime gameTime);
}
