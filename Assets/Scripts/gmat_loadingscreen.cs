using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gmat_loadingscreen : MonoBehaviour {
	protected AsyncOperation loading;
	protected float lastUpdated;
	public Camera LoadingCamera;

	protected gmat_engine m_Engine = null;
	public gmat_engine Engine {
		get {
			if (m_Engine == null) {
				m_Engine = FindObjectOfType<gmat_engine> ();
			}
			return m_Engine;
		}
	}

	public void Awake() {
		lastUpdated = Time.time;
		Engine.LoadingSceneManager = this;
	}

	public virtual void UnloadLoadingScene() {
		Debug.Log ("removing old assets");
		Destroy (gameObject);
	}
}
