using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrCameraScript : MonoBehaviour
{
    float camY = 0;
    float camX = 0;

    Vector3 camPos = Vector3.zero;
    Vector3 camPosOffset = Vector3.zero;
    [SerializeField] float sensitivity = 1;
    [SerializeField] float bobSpeed = 1;
    [Range(0.001f,0.1f)] [SerializeField] float bobIntensity = 1;
    [SerializeField] float smoothing = 10;

    float timer = 0;

    [SerializeField] Transform link;

    [SerializeField] Transform plr;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camPosOffset = transform.localPosition;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        float moveBobModifier = (float)PlrMovementScript.Instance.MoveState;

        camY += Input.GetAxis("Mouse X") * Time.deltaTime * 1000 * sensitivity;
        camX -= Input.GetAxis("Mouse Y") * Time.deltaTime * 1000 * sensitivity;
        camX = Mathf.Clamp(camX, -90, 90);

        plr.localRotation = Quaternion.Euler(plr.localEulerAngles.x, camY, plr.localEulerAngles.z);
        transform.localRotation = Quaternion.Euler(camX,0, 0);

        camPos.y = Mathf.Lerp(camPos.y, Mathf.Sin(timer * bobSpeed * moveBobModifier) * bobIntensity, Time.deltaTime * smoothing ) ;
        camPos.x = Mathf.Lerp(camPos.x, Mathf.Cos(timer * (bobSpeed * moveBobModifier) / 2) * bobIntensity, Time.deltaTime * smoothing);
        transform.localPosition = camPos + camPosOffset;


        if (link != null)
        {
            link.localRotation = transform.localRotation;
            link.localPosition = camPos/2 + camPosOffset;
        }
    }
}
