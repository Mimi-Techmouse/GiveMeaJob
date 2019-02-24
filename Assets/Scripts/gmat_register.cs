using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gmat_register : MonoBehaviour {

	public InputField username;
	public InputField email;
	public InputField password;
	public InputField password_confirmed;

	protected gmat_engine m_Engine = null;
	public gmat_engine Engine {
		get {
			if (m_Engine == null) {
				m_Engine = FindObjectOfType<gmat_engine> ();
			}
			return m_Engine;
		}
	}

	public virtual void RegisterUser() {
		if (ValidateUser ()) {
			Debug.Log ("form validated");
			string sPass = gmat_encryption.Encrypt (password.text);
			Debug.Log ("password: "+sPass);
			gmat_classes.User u = new gmat_classes.User (username.text, sPass, email.text);
			Debug.Log (u);
			StartCoroutine(u.CreateUser ());
			Engine.SetUser (u);
			Engine.WaitForIDThenLoad ("TaskView-Give");
		}
	}

	public virtual bool ValidateUser() {
		bool hasErrors = false;
		if (password.text != password_confirmed.text) {
			ReportError (password_confirmed, "Passwords must match!");
			hasErrors = true;
		}
		if (username.text.Length < 1 || username.text.Length >= 30) {
			ReportError (username, "Username must be between 1 and 30 characters.");
			hasErrors = true;
		}
		if (email.text.Length < 1) {
			ReportError (username, "Email is required.");
			hasErrors = true;
		}
		if (password.text.Length < 1) {
			ReportError (username, "Password is required.");
			hasErrors = true;
		}

		return !hasErrors;
	}

	public virtual void ReportError(InputField field, string message) {
		Debug.Log (message);
	}

}