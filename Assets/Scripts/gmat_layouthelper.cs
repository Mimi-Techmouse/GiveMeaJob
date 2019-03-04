using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmat_layouthelper : MonoBehaviour {

	//pick one; don't fill out both of these
	public gmat_enums.TIMEFRAME[] IncludedIn;
	public gmat_enums.TIMEFRAME[] ExcludedFrom;

	public bool GetIsInLayout(gmat_enums.TIMEFRAME frame) {
		if (ShouldBeIncluded (frame) && !ShouldBeExcluded (frame))
			return true;
		return false;
	}

	protected bool ShouldBeIncluded(gmat_enums.TIMEFRAME frame) {
		if (IncludedIn.Length <= 0)
			return true;
		
		foreach (gmat_enums.TIMEFRAME f in IncludedIn) {
			if (f == frame)
				return true;
		}
		return false;
	}

	protected bool ShouldBeExcluded(gmat_enums.TIMEFRAME frame) {
		if (ExcludedFrom.Length <= 0)
			return false;
		
		foreach (gmat_enums.TIMEFRAME f in ExcludedFrom) {
			if (f == frame)
				return false;
		}
		return true;
	}
}
