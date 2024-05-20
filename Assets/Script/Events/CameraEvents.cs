using System;

public class CameraEvents 
{
  public event Action<CameraStates> OnSwapCamera;
    public void SwapCamera(CameraStates cameraState)
    {
        if (OnSwapCamera != null)
        {
            OnSwapCamera(cameraState);
        }
    }
}
