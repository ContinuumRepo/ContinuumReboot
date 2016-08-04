using UnityEngine;
using System.Collections;

public class LineRendererSetPosition : MonoBehaviour {
	
	public LineRenderer lineRenderer;
	public Transform Origin;
	public Transform Attractor;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition (0, Origin.transform.position);
		lineRenderer.SetPosition (1, Attractor.transform.position);
		
		
	}
}
