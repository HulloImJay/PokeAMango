using UnityEngine;
using System.Collections;

public class Mango : MonoBehaviour {

	public AudioSource[] Noises;

	void OnMouseUpAsButton ()
	{
		GetComponent<Animator> ().SetTrigger ("Poked");
			
		if (Noises.Length > 0) {
			int ind = Random.Range (0, Noises.Length);
			Noises [ind].pitch = Random.Range (1.6f, 2f);
			Noises [ind].Play ();
		}
	}
}
