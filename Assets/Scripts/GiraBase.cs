using UnityEngine;

public class GiraBase : MonoBehaviour {

	public float smooth = 1.0F;
	public float tiltAngle = 30.0F;
	public string eixo = "green";
	public int eixo_invertido = 1;

	Transform giro; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (eixo == "green") // transform.up
		transform.RotateAround(transform.position, transform.up, smooth *Time.deltaTime * 90f * eixo_invertido);
		if (eixo == "blue")//transform.forward
			transform.RotateAround(transform.position, transform.forward, smooth*Time.deltaTime * 90f * eixo_invertido);
		if (eixo == "red") // transform.right
			transform.RotateAround(transform.position, transform.right, smooth*Time.deltaTime * 90f * eixo_invertido);
	}

	

}
