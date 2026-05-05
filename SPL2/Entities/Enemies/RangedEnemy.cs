using System.Data;
using GMDCore;
using GMDCore.Graphics;
using GMDCore.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SPL2;
using SPL2.Commands;
using SPL2.States.GameStates;

namespace SPL2.Entities;

public class RangedEnemy : BaseEnemy
{
    private float _attackRange = 50;
    public RangedEnemy(Sprite sprite, PlayState playState) : base(sprite, playState)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (base.DistanceToPlayer() <= _attackRange)
        {
            Attack();
        }
        else
        {
            MoveTowardsPlayer(gameTime);
        }
    }

    protected override void Attack()
    {
        
    }
}