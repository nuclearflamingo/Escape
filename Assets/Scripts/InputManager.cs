using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject player;
    public float playerMaxSpeed;
	// Use this for initialization
	void Start ()
    {

	}
	// Update is called once per frame
	void Update ()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis != 0.0f)
        {
            player.transform.Translate(horizontalAxis * playerMaxSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        if (verticalAxis != 0.0f)
        {
            player.transform.Translate(0.0f,verticalAxis * playerMaxSpeed * Time.deltaTime, 0.0f);
        }
    }
}
