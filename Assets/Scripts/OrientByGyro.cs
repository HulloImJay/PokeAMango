using UnityEngine;
using System.Collections;

/// <summary>
/// Gyroscope orientation for the "Pokémethod" of AR.
/// </summary>
public class OrientByGyro : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		// Be sure to request the gyro be enabled!
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (SystemInfo.supportsGyroscope)
			transform.localRotation = Quaternion.Euler(90f, 0f, 0f) * Input.gyro.attitude * new Quaternion(0f, 0f, 1f, 0f);

		/* NOTES:
		 * 	• Gyro co-ordinates come reflected in the XY plane, hence the Quat at the end
		 *    reflecting the Z value.
		 *  • Gyro co-ordinates are relative to the device laying flat. We rotate everything
		 *    forward by 90 degrees so we can think of "centre" as device upright instead.
		 */
	}

}

