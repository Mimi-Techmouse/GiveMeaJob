using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class gmat_classes : MonoBehaviour {

	public static string BaseURL = "http://miriamrauschert.com/app-integration/";

	[System.Serializable]
	public class User {

		public int ID;
		public string Username;
		protected string Password;
		protected string Email;

		public User(int i, string user, string pass, string e) {
			ID = i;
			Username = user;
			Password = pass;
			Email = e;
		}

		public User(string user, string pass, string e) {
			ID = -1;
			Username = user;
			Password = pass;
			Email = e;
		}

		public User(string user, string pass) {
			ID = -1;
			Username = user;
			Password = pass;
			Email = "";
		}

		public IEnumerator LoginUser() {
			WWWForm form = new WWWForm();
			form.AddField("username", Username);
			form.AddField("password", Password);
			Debug.Log (form);

			using (UnityWebRequest www = UnityWebRequest.Post(BaseURL+"?action=get_user", form))
			{
				yield return www.SendWebRequest();

				Debug.Log ("responded");
				if (www.isNetworkError || www.isHttpError) {
					Debug.Log(www.error);
				}
				else {
					UserResponse response = JsonUtility.FromJson<UserResponse> (www.downloadHandler.text);
					if (response.error != "")
						Debug.Log (response.error);
					else
						ID = response.id;
				}
			}
		}

		public IEnumerator CreateUser() {
			WWWForm form = new WWWForm();
			form.AddField("username", Username);
			form.AddField("password", Password);
			form.AddField("email", Email);
			Debug.Log (form);

			using (UnityWebRequest www = UnityWebRequest.Post(BaseURL+"?action=create_user", form))
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log(www.error);
				}
				else {
					UserResponse response = JsonUtility.FromJson<UserResponse> (www.downloadHandler.text);
					if (response.error != "")
						Debug.Log (response.error);
					else
						ID = response.id;
				}
			}
		}

		public bool SaveUser() {
			return true;
		}

	}

	[System.Serializable]
	public class UserResponse {
		public int id;
		public string error;

		public UserResponse(string i, string e) {
			id = int.Parse(i);
			error = e;
		}

		public UserResponse(int i, string e) {
			id = i;
			error = e;
		}
	}

	[System.Serializable]
	public class TaskCreator {

		public List<SimpleData> timeframes;
		public List<SimpleData> importanceLevels;
		public List<SimpleData> categories;

		public TaskCreator() {
		}

		public bool IsReadyToDraw() {
			if (timeframes != null && importanceLevels != null) // && categories != null)
				return true;

			return false;
		}

		public void DrawTimeFrames(Dropdown element) {
			List<string> newOptions = new List<string> ();
			foreach (SimpleData o in timeframes) {
				newOptions.Add (o.label);
			}
			element.AddOptions (newOptions);
		}

		public void DrawImportanceLevels(Dropdown element) {
			List<string> newOptions = new List<string> ();
			foreach (SimpleData o in importanceLevels) {
				newOptions.Add (o.label);
			}
			element.AddOptions (newOptions);
		}

		public IEnumerator GetTimeFrames() {
			WWWForm form = new WWWForm();

			using (UnityWebRequest www = UnityWebRequest.Post(BaseURL+"?action=get_time_frames", form))
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log(www.error);
				}
				else {
					timeframes = new List<SimpleData> ();
					SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(www.downloadHandler.text);
					foreach (SimpleJSON.JSONNode message in data["times"]) {
						SimpleData f = new SimpleData (message ["frame_id"].AsInt, message ["label"].Value);
						timeframes.Add (f);
					}
				}
			}
		}

		public IEnumerator GetImportanceLevels() {
			WWWForm form = new WWWForm();

			using (UnityWebRequest www = UnityWebRequest.Post(BaseURL+"?action=get_importance_levels", form))
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log(www.error);
				}
				else {
					importanceLevels = new List<SimpleData> ();
					SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(www.downloadHandler.text);
					foreach (SimpleJSON.JSONNode message in data["importances"]) {
						SimpleData f = new SimpleData (message ["importance_id"].AsInt, message ["label"].Value);
						importanceLevels.Add (f);
					}
				}
			}
		}

		public IEnumerator GetCategories(int id) {
			WWWForm form = new WWWForm();
			form.AddField("user_id", id);

			using (UnityWebRequest www = UnityWebRequest.Post(BaseURL+"?action=get_categories", form))
			{
				yield return www.SendWebRequest();

				if (www.isNetworkError || www.isHttpError) {
					Debug.Log(www.error);
				}
				else {
					categories = new List<SimpleData> ();
					SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(www.downloadHandler.text);
					foreach (SimpleJSON.JSONNode message in data["categories"]) {
						SimpleData f = new SimpleData (message ["category_id"].AsInt, message ["label"].Value, message ["description"].Value);
						categories.Add (f);
					}
				}
			}
		}
	}

	[System.Serializable]
	public class SimpleData {
		public int id = -1;
		public string label = "";
		public string description = "";

		public SimpleData(int i, string l) {
			id = i;
			label = l;
			description = "";
		}

		public SimpleData(int i, string l, string d) {
			id = i;
			label = l;
			description = d;
		}
	}
}