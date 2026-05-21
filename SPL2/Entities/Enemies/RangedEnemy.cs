using System.Data;
using GMDCore;
using GMDCore.Graphics;
using GMDCore.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SPL2;
using SPL2.Commands;
using SPL2.Commands.EnemyShootCommand;
using SPL2.States.GameStates;

namespace SPL2.Entities;

public class RangedEnemy : BaseEnemy
{
    private float _attackRange = 50;
    private float _cooldown = 2;
    private float _timer = 0;
    private ICommand _attack;
    public RangedEnemy(Sprite sprite, PlayState playState) : base(sprite, playState)
    {
        sprite.Color = Color.Turquoise;
        _attack = new EnemyShootCommand();
    }

    public override void Update(GameTime gameTime)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (base.DistanceToPlayer() <= _attackRange && _cooldown <= _timer)
        {
            Attack(gameTime);
            _timer = 0;
        }
        else
        {
            MoveTowardsPlayer(gameTime);
        }
    }

    protected override void Attack(GameTime gameTime)
    {
        _attack.Execute(this, gameTime, _playState);
    }
}