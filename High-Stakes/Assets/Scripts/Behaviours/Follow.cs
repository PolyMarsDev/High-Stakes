using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

public class Follow : MonoBehaviour {
	[SerializeField] Optional<GameObject> FollowObject;

	void Update() {
		if (FollowObject.Enabled) transform.position = FollowObject.Value.transform.position;
	}
}
