using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Utility
{
	public class BulletMovement : MonoBehaviour 
	{
		private AutoMoveAndRotate MoveAndotateScript;
		public Vector3 degreesPerSec;

		void Start () 
		{
			MoveAndotateScript = GetComponent<AutoMoveAndRotate> ();

			if (MoveAndotateScript == null) 
			{
				Debug.LogWarning("Cannot find AutoMoveAndRotate script");
			}

			if (MoveAndotateScript != null) 
			{
				MoveAndotateScript.rotateDegreesPerSecond.value 
					= new Vector3 (degreesPerSec.x, degreesPerSec.y, degreesPerSec.z);
			}
		}

		void Update () 
		{

		}
	}
}
