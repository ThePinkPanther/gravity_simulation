using UnityEngine;
using System.Collections;


public class Gravity : MonoBehaviour {

	private const float DEFAULT_MASS = 20;
	private const double G_CONST = 6.673e-11;

	private Rigidbody rb = null;
	public bool IsAffectedByOtherBodies;
	public bool AffectsOtherBodies = true;
	public double GForce = G_CONST;
	public float Mass = DEFAULT_MASS;
	
	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		IsAffectedByOtherBodies = rb != null;
		Mass = rb != null ? rb.mass : DEFAULT_MASS;
	}

	public void useTheForce(Gravity target) {
		if (!IsAffectedByOtherBodies 
		    || rb == null
		    || target == this)
			return;
		float distance = Vector3.Distance(this.gameObject.transform.position, target.gameObject.transform.position);
		float force = ((float)(GForce * Mass * target.Mass)) / (distance*distance*10);
		var direction = target.transform.position - transform.position;
		rb.AddForce(direction.normalized * force);
	}

	void Update () {
		if (!IsAffectedByOtherBodies 
			|| rb == null)
			return;
		foreach (Gravity component in FindObjectsOfType(typeof(Gravity))) {
			if (component.AffectsOtherBodies){
				useTheForce(component);
			}
		}
	}
}
