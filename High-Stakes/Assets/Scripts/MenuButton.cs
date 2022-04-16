using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private MeshRenderer buttonRenderer;
    public bool inCollider;
    // Start is called before the first frame update
    void Start()
    {
        buttonRenderer = GetComponent<MeshRenderer>();
        buttonRenderer.material.color = Color.white;
        inCollider = false;
    }
    void OnMouseOver() {
        buttonRenderer.material.color = Color.red;
        inCollider = true;
    }

    void OnMouseExit() {
        buttonRenderer.material.color = Color.white;
        inCollider = false;
    }
}
