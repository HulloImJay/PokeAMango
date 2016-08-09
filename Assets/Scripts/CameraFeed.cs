
using UnityEngine;
using System.Collections;

/// <summary>
/// Camera feed for the "Pokémethod" of AR.
/// </summary>
public class CameraFeed : MonoBehaviour {

	public int CamWidth = 1280;
	public int CamHeight = 720;
	public int CamFPS = 30;

	// Current script can be configured only for a single video FOV.
	public float VerticalFOV = 23f;

	// Remember our rotation at the start. We'll try to keep it relative to the camera feed.
	Quaternion baseRotation;

	// The principle camera feed object supplied by Unity.
	WebCamTexture camTexture;

	// Convenient material reference.
	Material myMaterial;

	void Start ()
	{
		baseRotation = transform.localRotation;

		// Init the camera feed.
		camTexture = new WebCamTexture (CamWidth, CamHeight, CamFPS);
		camTexture.Play ();

		// Assign it to our material.
		myMaterial = GetComponent<Renderer> ().material;
		myMaterial.mainTexture = camTexture;
	}


	void Update ()
	{
		// Only configure when the camera feed is started. Note that we don't actually need to do this each frame in the future...
		if (camTexture != null && camTexture.isPlaying) {

			/* NOTE:
			 * We don't accomodate the difference between camera feed aspect
			 * and screen aspect, so the code here is not 100% correct. I leave
			 * it to you to figure out that adaptation :) */

			// Apply rotation.
			transform.localRotation = baseRotation * Quaternion.AngleAxis(camTexture.videoRotationAngle, Vector3.up);

			// Flip if necessary.
			if (camTexture.videoVerticallyMirrored) {
				myMaterial.mainTextureScale = new Vector2 (1f, -1f);
				myMaterial.mainTextureOffset = new Vector2 (0f, 1f);
			} else {
				myMaterial.mainTextureScale = new Vector2 (1f, 1f);
				myMaterial.mainTextureOffset = new Vector2 (0f, 0f);
			}

			// Assuming the vertical FOV is correct, we can calculate the horizontal.
			float horizontalFOV = VerticalFOV * ((float)camTexture.width / camTexture.height);

			// Assign the best field of view to the main camera.
			if (Screen.height > Screen.width) {
				Camera.main.fieldOfView = horizontalFOV;
			} else {
				Camera.main.fieldOfView = VerticalFOV;
			}

			// The raw distance from camera to centre of the plane.
			float distance = (transform.position - Camera.main.transform.position).magnitude;

			// Resize the plane to fit the screen.
			transform.localScale = new Vector3 ( Mathf.Tan(Mathf.Deg2Rad * horizontalFOV/2f) * distance / 5f, 1f, Mathf.Tan(Mathf.Deg2Rad * VerticalFOV/2f) * distance / 5f);
		}
	}
}