using UnityEngine;

public class Mover : MonoBehaviour
{
	public float speed;
   
    private void Start ()
	{
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }


}
