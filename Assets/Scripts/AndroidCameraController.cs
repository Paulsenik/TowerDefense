using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCameraController : MonoBehaviour
{

    private static readonly float[] BoundsX = new float[] { -10f, 5f };
    private static readonly float[] BoundsZ = new float[] { -18f, -4f };
    private static readonly float[] ZoomBounds = new float[] { 10f, 85f };

    public float speed = 0.1f;
    public float ZoomSpeedTouch = 0.1f;
    public Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId; // Touch mode only

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only


    void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

        }
        if(Input.touchCount == 2) {
            Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
            if (!wasZoomingLastFrame) {
                lastZoomPositions = newPositions;
                wasZoomingLastFrame = true;
            } else {
                // Zoom based on the distance between the new positions compared to the 
                // distance between the previous positions.
                float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                float offset = newDistance - oldDistance;

                ZoomCamera(offset, ZoomSpeedTouch);

                lastZoomPositions = newPositions;
            }
        }

    }
    void ZoomCamera(float offset, float speed) {
        if (offset == 0) {
            return;
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }
}
