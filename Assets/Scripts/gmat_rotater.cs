using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmat_rotater : MonoBehaviour {

	public Vector3 spinVector;

	public virtual void Update() {
		Vector3 v = spinVector * Time.deltaTime;
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x + v.x, transform.localEulerAngles.y + v.y, transform.localEulerAngles.y + v.y);
	}
}
