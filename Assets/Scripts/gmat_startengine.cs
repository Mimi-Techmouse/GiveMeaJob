using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmat_startengine : MonoBehaviour {

	protected gmat_engine m_Engine = null;
	public gmat_engine Engine {
		get {
			if (m_Engine == null) {
				m_Engine = FindObjectOfType<gmat_engine> ();
			}
			return m_Engine;
		}
	}

	// Use this for initialization
	void Awake () {
		if (Engine == null) {
			Transform e = Resources.Load<Transform>("Engine");
			Instantiate (e, Vector3.zero, e.rotation);
		}
	}
}
