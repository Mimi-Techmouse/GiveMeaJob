using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmat_uicontroller : MonoBehaviour {

	public List<RectTransform> UIComponents;
	public Transform ComponentParent;
	public float StartingY = -14;

	public virtual void Awake() {
		UIComponents = new List<RectTransform> ();
		RectTransform[] rects = ComponentParent.GetComponentsInChildren<RectTransform> ();
		foreach (RectTransform child in rects) {
			//Make sure we're getting the right level
			if (child.parent == ComponentParent)
				UIComponents.Add (child);
		}
		RedrawComponents ();
	}

	public virtual void RedrawComponents() {

		float currentY = StartingY;
		float buffer = 5;
		foreach (RectTransform child in UIComponents) {
			if (child.gameObject.activeSelf) {
				float halfHeight = (child.sizeDelta.y / 2); 
				currentY -= halfHeight;
				child.localPosition = new Vector3 (child.localPosition.x, currentY, child.localPosition.z);
				child.ForceUpdateRectTransforms ();
				currentY -= (halfHeight+buffer);
			}
		}
	}

	public virtual void UpdateLayoutFromTimeframe(gmat_enums.TIMEFRAME frame) {
		Debug.Log ("frame: " + frame);
		foreach (RectTransform child in UIComponents) {
			gmat_layouthelper helper = child.gameObject.GetComponent<gmat_layouthelper> ();
			if (helper != null) {
				if (helper.GetIsInLayout (frame)) {
					Debug.Log (helper.name + " is in layout");
					helper.gameObject.SetActive (true);
				} else {
					Debug.Log (helper.name + " is not in layout");
					helper.gameObject.SetActive (false);
				}
			} else {
				Debug.Log (child.name + " does not have a layout");
			}
		}

		RedrawComponents ();
	}
}
