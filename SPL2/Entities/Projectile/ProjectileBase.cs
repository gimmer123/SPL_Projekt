using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SPL2.Commands;
using SPL2.States.GameStates;

namespace SPL2.Entities.Projectile;

public abstract class ProjectileBase : IEntity
{
    public Vector2 Position {get; set;}
    public float Speed => 60;
    private Vector2 _direction;
    private Sprite _sprite;
    protected PlayState _playState;
    public bool Remove {get; set;} = false;
    private float _duration => 4;
    private float _spawnTime;
    public Circle Collider {get; set;}

    public ProjectileBase(Sprite sprite, Vector2 direction, IEntity origin, PlayState playState, GameTime gameTime)
    {
        _direction = direction;
        Position = new(origin.Position.X, origin.Position.Y);
        _spawnTime = gameTime.ElapsedGameTime.Seconds;
        _sprite = sprite;
        _playState = playState;
        _playState.Floor.OnMove += FloorMovement; 
        Collider = new Circle(new Point((int)Position.X, (int)Position.Y), (int)_sprite.Height / 2);
    }

    public virtual void Update(GameTime gameTime)
    {
        Position += _direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        Collider = new Circle(new Point((int)Position.X, (int)Position.Y), (int)_sprite.Height / 2);

        if (_spawnTime - gameTime.ElapsedGameTime.TotalSeconds >= _duration)
        {
            Remove = true;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }

    private void FloorMovement(double x, double y)
    {
        Position = new Vector2(Position.X - (float)x, Position.Y - (float)y);
    }

    public bool Intersects(Circle other)
    {
        return Collider.Intersects(other);
    }

}