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
			Debug.Log (child.name + ", " + child.sizeDelta);
			if (child.gameObject.activeSelf) {
				float halfHeight = (child.sizeDelta.y / 2);
				currentY -= halfHeight;
				child.localPosition = new Vector3 (child.localPosition.x, currentY, child.localPosition.z);
				child.ForceUpdateRectTransforms ();
				currentY -= (halfHeight+buffer);
			}
		}
	}
}
