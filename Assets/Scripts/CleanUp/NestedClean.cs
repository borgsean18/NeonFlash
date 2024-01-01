using UnityEngine;

namespace CleanUp
{
	public class NestedClean : MonoBehaviour
	{
		public void DestroySelf()
		{
			if (transform.childCount <= 1)
				Destroy(gameObject);
		}
	}
}
