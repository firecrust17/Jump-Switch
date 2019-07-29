using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public void LoadScene(int index){
		SceneManager.LoadScene(index);
	}

	public void LoadSceneByName(string name){
		SceneManager.LoadScene(name);
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void restartLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void nextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}

}
