using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gmat_createjob : MonoBehaviour {

	public string taskName;
	public string taskDescription;
	public gmat_enums.TIMEFRAME taskFrame;
	public string taskStart;
	public string taskEnd;
	public int taskCategory;
	public int taskImportance;

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

	protected gmat_uicontroller m_Control = null;
	public gmat_uicontroller Controller {
		get {
			if (m_Control == null) {
				m_Control = GetComponent<gmat_uicontroller> ();
			}
			return m_Control;
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
			Controller.UpdateLayoutFromTimeframe (gmat_enums.TIMEFRAME.NONE);
			isDrawn = true;
		}
	}

	public virtual void SetName(InputField field) {
		taskName = field.text;
	}

	public virtual void SetDescription(InputField field) {
		taskDescription = field.text;
	}

	public virtual void SetTimeFrame(Dropdown field) {
		gmat_enums.TIMEFRAME n = (gmat_enums.TIMEFRAME)creator.timeframes[field.value].id;
		taskFrame = n;

		if (Controller != null) {
			Controller.UpdateLayoutFromTimeframe (taskFrame);
		}
	}
}
