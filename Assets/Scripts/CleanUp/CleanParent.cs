using UnityEngine;

namespace CleanUp
{
	public class CleanParent : MonoBehaviour
	{
		public void DestroySelf()
		{
			if (transform.childCount <= 1)
				Destroy(gameObject);
		}
	}
}
