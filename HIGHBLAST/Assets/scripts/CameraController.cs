using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPunCallbacks
{

    public Camera mainCamera;
    public Camera weaponCamera;
    float sensX = 1f;
    float sensY = 1f;
    float wallRunTilt = 15f;

    float wishTilt = 0;
    float curTilt = 0;
    Vector2 currentLook;
    Vector2 sway = Vector3.zero;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        curTilt = transform.localEulerAngles.z;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        RotateMainCamera();
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        currentLook = Vector2.Lerp(currentLook, currentLook + sway, 0.8f);
        curTilt = Mathf.LerpAngle(curTilt, wishTilt * wallRunTilt, 0.05f);

        sway = Vector2.Lerp(sway, Vector2.zero, 0.2f);
    }

    void RotateMainCamera()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseInput.x *= sensX;
        mouseInput.y *= sensY;

        currentLook.x += mouseInput.x;
        currentLook.y = Mathf.Clamp(currentLook.y += mouseInput.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-currentLook.y, Vector3.right);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, curTilt);
        transform.root.transform.localRotation = Quaternion.Euler(0, currentLook.x, 0);
    }

    public void Punch(Vector2 dir)
    {
        sway += dir;
    }

    #region Setters
    public void SetTilt(float newVal)
    {
        wishTilt = newVal;
    }

    public void SetXSens(float newVal)
    {
        sensX = newVal;
    }

    public void SetYSens(float newVal)
    {
        sensY = newVal;
    }

    #endregion
}
