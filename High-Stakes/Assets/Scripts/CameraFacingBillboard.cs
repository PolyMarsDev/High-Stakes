using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class CameraFacingBillboard : MonoBehaviour
{
    public Camera m_Camera {get; private set; }
	[SerializeField] float childrenMoveForward = 1f;
	[SerializeField] float selfMoveForward = 1f;

	Vector3 localPos;

	// Change to automatically detect camera in scene
	void Awake() {
		m_Camera = Camera.main;
		if (m_Camera == null)
			m_Camera = FindObjectOfType<Camera>();
		localPos = transform.localPosition;
	}
 
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up
		);
		foreach (Transform child in transform) {
			Vector3 forward = (child.transform.position - m_Camera.transform.position).normalized;
			child.transform.position += forward * -childrenMoveForward;
		}
		if (selfMoveForward > 0) {
			Vector3 forward2 = (transform.position - m_Camera.transform.position).normalized;
			transform.localPosition = localPos; 
			transform.position += forward2 * -selfMoveForward;
		}
    }
}


