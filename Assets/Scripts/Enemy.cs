using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour {
	// public void OnMovement(InputValue value) {
	// 	Vector2 mvmt = value.Get<Vector2>();
	// 	if (mvmt.x == 0 || mvmt.y == 0)
	// 		transform.Translate(new Vector3(mvmt.x, 0, mvmt.y));
	// 		this.die(value);

	// }
	// // public void OnMovement(InputValue value) {
	// // 	Vector2 mvmt = value.Get<Vector2>();
	// // 	if (mvmt.x == 0 || mvmt.y == 0)
	// // 		transform.Translate(new Vector3(mvmt.x, 0, mvmt.y));
	// // }

    public void die(InputValue value) {
        // Kills the game object
        Destroy (gameObject);
        // Removes this script instance from the game object
        Destroy (this);
        // Removes the rigidbody from the game object
        Destroy(GetComponent<Rigidbody>());
	}
    //     // Kills the game object in 5 seconds after loading the object
    //     // Destroy (gameObject, 5);
    //     // When the user presses Ctrl, it will remove the script 
    //     // named FooScript from the game object
    // }


    //  When the user presses Ctrl, it will remove the script 
    //  named FooScript from the game object
    //  public void Update (InputValue input) {
    //      if (Input.GetButton ("Fire1") && GetComponent (FooScript))
    //         //  Destroy (GetComponent (FooScript));
    //         Console.WriteLine("This is C#");
	// 		this.die(input);
    //  }
}