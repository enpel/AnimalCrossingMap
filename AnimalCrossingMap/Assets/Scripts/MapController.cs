using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public float angle = 10.0f;
	public float move = 5.0f;
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.transform.RotateAround(Vector3.right,-angle * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			this.transform.RotateAround(Vector3.right,angle * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			this.transform.Translate(Vector3.left * move * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			this.transform.Translate(Vector3.right * move * Time.deltaTime);
		}
	}
}
