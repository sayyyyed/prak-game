using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Transform Player;
    public Transform ViewPoint;
    public Transform AIMViewPoint;
    public float RotationSpeed;
    public GameObject TPSCamera, AIMCamera, TPS2;
    public GameObject crosshair;
    bool TPSMode = true; 
    bool AIMMode = false;
    // Start is called before the first frame update
    void Start()
    {
     Cursor.lockState = CursorLockMode.Locked;
     crosshair.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 viewDir = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        ViewPoint.forward = viewDir.normalized;

        // Vector3 InputDir = ViewPoint.forward * verticalInput + ViewPoint.right * horizontalInput;
       if (TPSMode)
       {
        Vector3 InputDir = ViewPoint.forward * verticalInput + ViewPoint.right * horizontalInput;
        if(InputDir != Vector3.zero)
        {
            Player.forward = Vector3.Slerp(Player.forward, InputDir.normalized, Time.deltaTime * RotationSpeed);
        }
       }
       else if(AIMMode)
       {
        Vector3 dirToCombatLookAt = AIMViewPoint.position - new Vector3(transform.position.x, AIMViewPoint.position.y, transform.position.z);
        AIMViewPoint.forward = dirToCombatLookAt.normalized;

        Player.forward = Vector3.Slerp(Player.forward, dirToCombatLookAt.normalized, Time.deltaTime * RotationSpeed);
       }
    }

    public void CameraModeChanger(bool TPS, bool AIM)
    {
        TPSMode = TPS;
        AIMMode = AIM;
        if (TPS)
        {
            TPS2.SetActive(true);
            AIMCamera.SetActive(false);
            crosshair.SetActive(false);
        }
        else if (AIM)
        {
            TPSCamera.SetActive(false);
            AIMCamera.SetActive(true);
            crosshair.SetActive(true);
        }
    }
}
