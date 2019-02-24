using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gmat_createjob : MonoBehaviour {

	public gmat_classes.TaskCreator creator;
	public Dropdown TimeFrameDropdown;
	public Dropdown ImportanceDropdown;
	public Dropdown CategoryDropdown;
	public bool isDrawn = false;

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
		creator = new gmat_classes.TaskCreator ();
		StartCoroutine(creator.GetTimeFrames ());
		StartCoroutine(creator.GetImportanceLevels ());
		//StartCoroutine(creator.GetCategories (Engine.CurrentUser.ID));
	}

	public void Update() {
		if (isDrawn)
			return;

		if (creator.IsReadyToDraw ()) {
			Debug.Log ("we're gonna draw!");
			creator.DrawTimeFrames (TimeFrameDropdown);
			creator.DrawImportanceLevels (ImportanceDropdown);
			isDrawn = true;
		}
	}
}
