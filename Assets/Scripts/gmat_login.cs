using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gmat_login : MonoBehaviour {

	public InputField username;
	public InputField password;

	protected gmat_engine m_Engine = null;
	public gmat_engine Engine {
		get {
			if (m_Engine == null) {
				m_Engine = FindObjectOfType<gmat_engine> ();
			}
			return m_Engine;
		}
	}

	public virtual void LoginUser() {
		string sPass = gmat_encryption.Encrypt (password.text);
		Debug.Log (sPass);
		gmat_classes.User u = new gmat_classes.User (username.text, sPass);
		StartCoroutine(u.LoginUser ());
		Engine.SetUser (u);
		Engine.WaitForIDThenLoad ("TaskView-Give");
	}

	protected virtual bool ValidateUser() {
		bool hasErrors = false;
		if (username.text.Length < 1 || username.text.Length >= 30) {
			ReportError (username, "Username must be between 1 and 30 characters.");
			hasErrors = true;
		}
		if (password.text.Length < 1) {
			ReportError (username, "Password is required.");
			hasErrors = true;
		}

		return !hasErrors;
	}

	public virtual void LoadRegisterScreen() {
		Engine.LoadLevel ("MainMenu-Register");
	}

	public virtual void ReportError(InputField field, string message) {
		Debug.Log (message);
	}
}
