using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour {

	[SerializeField]private Slider loadBar;

	[Space(10)]
	[SerializeField] private Text tipTxt;
	[SerializeField] private Text tipBgTxt;
	[SerializeField] private string[] loadTips;

	int currentTip;

	[SerializeField] private float tipDuration = 3f;
	private float lastTip;

	void Start() {
		currentTip = Random.Range(0, loadTips.Length);
		UpdateTip();

		LoadScene();
    }

	void LoadScene() {

		StartCoroutine(LoadCO());
    }

	IEnumerator LoadCO() {
		AsyncOperation _op = SceneManager.LoadSceneAsync(2);

        while(!_op.isDone) {
			float _progress = Mathf.Clamp01(_op.progress / .9f);
			loadBar.value = _progress;

			yield return null;
        }
	}

	void Update() {
		if(lastTip <= Time.time) { UpdateTip(); }

    }
	
	void UpdateTip() {
		if(currentTip >= loadTips.Length - 1){
			currentTip = 0;
        } else {
			currentTip++;
        }

		tipTxt.text = "Dica #" + (currentTip + 1) +":\n" + loadTips[currentTip];
		tipBgTxt.text = tipTxt.text;

		lastTip = Time.time + tipDuration;
	}
	
}
