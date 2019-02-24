using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

		public List<TimeFrame> timeframes;

		public TaskCreator() {
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
					timeframes = new List<TimeFrame> ();
					SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(www.downloadHandler.text);
					foreach (SimpleJSON.JSONNode message in data["times"]) {
						TimeFrame f = new TimeFrame (message ["frame_id"].AsInt, message ["label"].Value);
						timeframes.Add (f);
					}
				}
			}
		}
	}

	[System.Serializable]
	public class TimeFrame {
		public int frame_id = -1;
		public string label = "";

		public TimeFrame(int i, string l) {
			frame_id = i;
			label = l;
		}
	}
}