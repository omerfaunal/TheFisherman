using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera firstPersonCamera;
    public CinemachineVirtualCamera thirdPersonCamera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Switching camera");
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        if (firstPersonCamera.Priority > thirdPersonCamera.Priority)
        {
            Debug.Log("Switching to third person camera");
            firstPersonCamera.Priority = 0;
            thirdPersonCamera.Priority = 10;
        }
        else
        {
            Debug.Log("Switching to first person camera");
            firstPersonCamera.Priority = 10;
            thirdPersonCamera.Priority = 0;
        }
    }
}
