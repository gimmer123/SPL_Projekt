using System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace GMDCore.Input;

public enum MouseButton
{
    Left,
    Right,
    Middle,
    XButton1,
    XButton2
}

public class MouseInfo
{
    public MouseState CurrentState { get; private set; }
    public MouseState PreviousState { get; private set; }

    public MouseInfo()
    {
        CurrentState = Mouse.GetState();
        PreviousState = CurrentState;
    }

    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Mouse.GetState();
    }

    public bool ButtonDown(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => CurrentState.LeftButton == ButtonState.Pressed,
            MouseButton.Right => CurrentState.RightButton == ButtonState.Pressed,
            MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Pressed,
            MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Pressed,
            MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Pressed,
            _ => false
        };
    }

    public bool ButtonPressed(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => CurrentState.LeftButton == ButtonState.Pressed && PreviousState.LeftButton == ButtonState.Released,
            MouseButton.Right => CurrentState.RightButton == ButtonState.Pressed && PreviousState.RightButton == ButtonState.Released,
            MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Pressed && PreviousState.MiddleButton == ButtonState.Released,
            MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Pressed && PreviousState.XButton1 == ButtonState.Released,
            MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Pressed && PreviousState.XButton2 == ButtonState.Released,
            _ => false
        };
    }

    public bool ButtonReleased(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => CurrentState.LeftButton == ButtonState.Released && PreviousState.LeftButton == ButtonState.Pressed,
            MouseButton.Right => CurrentState.RightButton == ButtonState.Released && PreviousState.RightButton == ButtonState.Pressed,
            MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Released && PreviousState.MiddleButton == ButtonState.Pressed,
            MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Released && PreviousState.XButton1 == ButtonState.Pressed,
            MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Released && PreviousState.XButton2 == ButtonState.Pressed,
            _ => false
        };
    }

    public Vector2 GetPosition()
    {
        return new Vector2(CurrentState.X, CurrentState.Y);
    }

    public Vector2 GetPreviousPosition()
    {
        return new Vector2(PreviousState.X, PreviousState.Y);
    }

    public Vector2 GetDelta()
    {
        return GetPosition() - GetPreviousPosition();
    }
}