using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrCameraScript : MonoBehaviour
{
    public static PlrCameraScript Instance { get; private set; }

    float camY = 0;
    float camX = 0;

    Vector3 camPos = Vector3.zero;
    Vector3 camPosOffset = Vector3.zero;
    [SerializeField] float bobSpeed = 1;
    [Range(0.001f,0.1f)] [SerializeField] float bobIntensity = 1;
    [SerializeField] float smoothing = 10;

    [SerializeField] Transform link;

    [SerializeField] Transform plr;

    private void Awake()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        camPosOffset = transform.localPosition;
    }

    private void Update()
    {
        float moveBobModifier = (float)PlrMovementScript.Instance.MoveState;

        //Otáèení kamery podle myši
        camY += Input.GetAxis("Mouse X") * Time.deltaTime * 1000 * Settings.Sensitivity;
        camX -= Input.GetAxis("Mouse Y") * Time.deltaTime * 1000 * Settings.Sensitivity;
        camX = Mathf.Clamp(camX, -90, 90); //Omezení rotace

        //Aplikování rotace
        plr.localRotation = Quaternion.Euler(plr.localEulerAngles.x, camY, plr.localEulerAngles.z);
        transform.localRotation = Quaternion.Euler(camX,0, 0);

        //Houpání kamery
        camPos.y = Mathf.Lerp(camPos.y, Mathf.Sin(Time.time * bobSpeed * moveBobModifier) * bobIntensity, Time.deltaTime * smoothing ) ;
        camPos.x = Mathf.Lerp(camPos.x, Mathf.Cos(Time.time * (bobSpeed * moveBobModifier) / 2) * bobIntensity, Time.deltaTime * smoothing);
        transform.localPosition = camPos + camPosOffset;


        if (link != null)
        {
            link.localRotation = transform.localRotation;
            link.localPosition = camPos/2 + camPosOffset;
        }
    }
}
