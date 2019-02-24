using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmat_createjob : MonoBehaviour {

	public gmat_classes.TaskCreator creator;

	public void Awake() {
		creator = new gmat_classes.TaskCreator ();
		StartCoroutine(creator.GetTimeFrames ());
	}
}
