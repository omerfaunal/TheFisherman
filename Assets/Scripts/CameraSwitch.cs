using UnityEngine;
using Cinemachine;

public enum CameraType
{
    FirstPerson,
    ThirdPerson
}

public class CameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera firstPersonCamera;
    public CinemachineVirtualCamera thirdPersonCamera;

    private CameraType selectedCameraType = CameraType.ThirdPerson;


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
            SetCameraType(CameraType.ThirdPerson, false);
        }
        else
        {
            Debug.Log("Switching to first person camera");
            SetCameraType(CameraType.FirstPerson, false);
        }
    }

    private void SwitchToFirstPersonCamera()
    {
        firstPersonCamera.Priority = 10;
        thirdPersonCamera.Priority = 0;
    }

    private void SwitchToThirdPersonCamera()
    {
        firstPersonCamera.Priority = 0;
        thirdPersonCamera.Priority = 10;
    }

    public void SetCameraType(CameraType cameraType, bool forced = false)
    {
        if(!forced){
            selectedCameraType = cameraType;
        }
        if (cameraType == CameraType.FirstPerson)
        {
            SwitchToFirstPersonCamera();
        }
        else
        {
            SwitchToThirdPersonCamera();
        }
    }

    public void ResetToDefaultCamera()
    {
        SetCameraType(CameraType.ThirdPerson);
    }

}
