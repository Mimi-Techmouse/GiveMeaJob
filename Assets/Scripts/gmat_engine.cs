using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmat_engine : MonoBehaviour {

	public gmat_classes.User CurrentUser;
	public gmat_loadingscreen LoadingSceneManager;
	public gmat_fader Fader;

	public bool IsSafeToPlay;
	public float MinLoadScreenTime;

	protected bool isWaiting = false;
	protected string sLevel = "";

	public virtual void Awake() {
		DontDestroyOnLoad (gameObject);
	}

	public virtual void Update() {
		if (isWaiting && CurrentUser.ID >= 0) {
			isWaiting = false;
			LoadLevel (sLevel);
			sLevel = "";
		}
	}

	public void SetUser(gmat_classes.User u) {
		CurrentUser = u;
	}

	public void WaitForIDThenLoad(string LevelToLoad) {
		sLevel = LevelToLoad;
		isWaiting = true;
	}
		
	public void LoadLevel(string LevelToLoad) {
		IsSafeToPlay = false;
		StartCoroutine (LoadScene (LevelToLoad));
	}

	public IEnumerator LoadScene(string sceneName) {

		// Fade to black
		//yield return StartCoroutine(Fader.FadeTo(1.0f, 0.5f));

		// Load loading screen
		yield return Application.LoadLevelAsync("LoadingScreen");

		// Fade to loading screen
		//yield return StartCoroutine(Fader.FadeTo(0.0f, 0.5f));

		float endTime = Time.time + MinLoadScreenTime;

		// Load level async
		yield return Application.LoadLevelAdditiveAsync(sceneName);

		if (Time.time < endTime)
			yield return new WaitForSeconds(endTime - Time.time);

		// Fade to black
		//yield return StartCoroutine(Fader.FadeTo(1.0f, 0.5f));

		// !!! unload loading screen
		LoadingSceneManager.UnloadLoadingScene();
		IsSafeToPlay = true;

		// Fade to new screen
		//yield return StartCoroutine(Fader.FadeTo(0.0f, 0.5f));

	}

	public void QuitGame() {
		Debug.Log ("we are quitting!");
		Application.Quit ();
	}
}