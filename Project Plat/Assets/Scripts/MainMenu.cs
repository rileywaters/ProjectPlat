using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string firstLevel;
	public string LevelSelect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame(){
		SceneManager.LoadScene (firstLevel);
	}
	public void Continue(){
		SceneManager.LoadScene (LevelSelect);
	}
	public void QuitGame(){
		Application.Quit ();
	}
}
