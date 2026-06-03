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

public abstract class BaseEnemy : IEntity
{
    public Vector2 Position {get; set;}
    public float Speed => 20;
    public bool Remove {get; set;} = false;
    public Circle Collider {get; set;}
    public int Health { get; protected set; }
    public int MaxHealth { get; protected set; } = 3;
    protected Sprite _sprite;
    protected PlayState _playState;
    
    public BaseEnemy(Sprite sprite, PlayState playState)
    {
        _sprite = sprite;
        _sprite.CenterOrigin();
        _playState = playState;
        _playState.Floor.OnMove += FloorMovement; 
        Collider = new Circle(new Point((int)Position.X, (int)Position.Y), (int)_sprite.Height / 2);

        Health = MaxHealth;
    }
    
    public abstract void Update(GameTime gameTime);
    
    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
        Collider = new Circle(new Point((int)Position.X, (int)Position.Y), (int)_sprite.Height / 2);
    }

    private void FloorMovement(double x, double y)
    {
        Position = new Vector2(Position.X - (float)x, Position.Y - (float)y);
    }

    protected void MoveTowardsPlayer(GameTime gameTime)
    {
        Vector2 direction = DirectionToPlayer();
        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

    protected Vector2 DirectionToPlayer()
    {
        Vector2 direction = _playState.Player.Position - Position;
        if (direction != Vector2.Zero)
        {
            direction.Normalize();
        }
        return direction;
    }
    protected double DistanceToPlayer()
    {
        return Vector2.Distance(Position, _playState.Player.Position);
    }
    protected abstract void Attack(GameTime gameTime);

    public void TakeDamage()
    {
        Health--;
        if (Health <= 0)
        {
            Remove = true;
            _playState.Spawner.EnemyRemoved();
        }
    }
}