using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class Loading : MonoBehaviour {
	void Start() {
	}
	void Update() {
		try {
			if (FMODUnity.RuntimeManager.HaveMasterBanksLoaded) {
				Debug.Log("Master Bank Loaded");
				SceneManager.LoadScene(1);
			} else {
				Debug.Log("Master Bank Not Yet Loaded " + FMODUnity.RuntimeManager.AnySampleDataLoading());
			}
		} catch (Exception err) {
			Debug.Log(err);
		}

	}
}
