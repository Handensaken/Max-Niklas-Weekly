using System;

public class InputEvents 
{
    public event Action OnSubmitPressed;
    public void SubmitPressed()
    {
        if (OnSubmitPressed != null)
        {
            OnSubmitPressed();
        }
    }
}
