using System;

public class PlayerEvents
{
    public event Action OnDisableMovement;
    public void DisableMovement()
    {
        if (OnDisableMovement != null)
        {
            OnDisableMovement();
        }
    }
    public event Action OnEnableMovement;
    public void EnableMovement()
    {
        if (OnEnableMovement != null)
        {
            OnEnableMovement();
        }
    }
}
