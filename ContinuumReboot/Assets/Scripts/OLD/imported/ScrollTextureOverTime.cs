using UnityEngine;
using System.Collections;

public class ScrollTextureOverTime : MonoBehaviour {

	public enum OffsetType { X = 0, Y = 1, XandY = 2 }
	public OffsetType OffsetAxes = OffsetType.X;
	public float scrollSpeed = 2.0f;
	public float lockedOffset;
	public float offset = 0.0f;
	public float rotate = 0.0f;
	public Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		offset+= (Time.deltaTime*scrollSpeed)/10.0f;

		if (OffsetAxes == OffsetType.X) {
			rend.material.SetTextureOffset ("_MainTex", new Vector2 (offset, lockedOffset));
		}

		if (OffsetAxes == OffsetType.Y) {
			rend.material.SetTextureOffset ("_MainTex", new Vector2 (lockedOffset, offset));
		}

		if (OffsetAxes == OffsetType.XandY) {
			rend.material.SetTextureOffset ("_MainTex", new Vector2 (offset, offset));
		}
	}
}
