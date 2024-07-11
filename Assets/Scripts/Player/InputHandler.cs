using UnityEngine;

public class InputHandler
{
    public float GetHorizontalMovement()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool IsJumpPressed()
    {
        return Input.GetButtonDown("Jump");
    }
}