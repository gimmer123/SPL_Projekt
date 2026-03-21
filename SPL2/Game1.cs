using GMDCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SPL2.States.GameStates;

namespace SPL2;

public class Game1 : Core
{
    public const int VIRTUAL_WIDTH = 256;
    public const int VIRTUAL_HEIGHT = 144;
    public new Matrix ScreenScaleMatrix => base.ScreenScaleMatrix;
    private GameStateBase _currentState;

    public Game1(): base("SPL2 Projekt", 1280, 720, VIRTUAL_WIDTH, VIRTUAL_HEIGHT)
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();
        SetState(new PlayState(this));
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        _currentState.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _currentState.Draw(SpriteBatch);

        base.Draw(gameTime);
    }

    public void SetState(GameStateBase newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
