using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmat_givejob : MonoBehaviour {

	protected gmat_engine m_Engine = null;
	public gmat_engine Engine {
		get {
			if (m_Engine == null) {
				m_Engine = FindObjectOfType<gmat_engine> ();
			}
			return m_Engine;
		}
	}

	public virtual void LoadAddTask() {
		Engine.LoadLevel ("TaskView-Add");
	}
}
