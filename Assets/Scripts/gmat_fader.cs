using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gmat_fader : MonoBehaviour {

	protected gmat_engine m_Engine = null;
	public gmat_engine Engine {
		get {
			if (m_Engine == null) {
				m_Engine = FindObjectOfType<gmat_engine> ();
			}
			return m_Engine;
		}
	}

	public virtual void Start() {
		Engine.Fader = this;
	}

	protected Image m_Sprite = null;
	public Image FaderSprite {
		get {
			if (m_Sprite == null)
				m_Sprite = GetComponent<Image> ();
			return m_Sprite;
		}
	}
		
	public IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = FaderSprite.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			FaderSprite.color = newColor;
			yield return null;
		}
	}
}
