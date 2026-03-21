


using System.Data;
using GMDCore;
using GMDCore.Graphics;
using GMDCore.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SPL2;
using SPL2.Commands;

public class Player
{
    public Vector2 Position;
    public float Speed = 20;
    private ICommand _wKey, _aKey, _sKey, _dKey;
    
    private Sprite _sprite;
    
    public Player(Sprite sprite)
    {
        _sprite = sprite;
        _wKey = new MoveUpCommand();
        _aKey = new MoveLeftCommand();
        _sKey = new MoveDownCommand();
        _dKey = new MoveRightCommand();
    }

    public void Update(GameTime gameTime)
    {
        HandleInput(gameTime);
    }

    private void HandleInput(GameTime gameTime)
    {
        if (Core.Input.Keyboard.CurrentState.IsKeyDown(Keys.W))
        {
            _wKey.Execute(this, gameTime);
        }

        if (Core.Input.Keyboard.CurrentState.IsKeyDown(Keys.A))
        {
            _aKey.Execute(this, gameTime);
        }

        if (Core.Input.Keyboard.CurrentState.IsKeyDown(Keys.S))
        {
            _sKey.Execute(this, gameTime);
        }

        if (Core.Input.Keyboard.CurrentState.IsKeyDown(Keys.D))
        {
            _dKey.Execute(this, gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, Position);
    }

}