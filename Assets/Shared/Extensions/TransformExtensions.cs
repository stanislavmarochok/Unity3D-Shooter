using UnityEngine;

namespace Shared.Extensions
{
	public static class TransformExtensions 
	{

		/// <summary>
		/// Check if the target is within line of sight
		/// </summary>
		/// <returns><c>true</c> if is in line of sight the specified origin target fieldOfView collisionMask offset; otherwise, <c>false</c>.</returns>
		/// <param name="origin">Transform origin</param>
		/// <param name="target">Target direction</param>
		/// <param name="fieldOfView">Field of view</param>
		/// <param name="collisionMask">Check against layers</param>
		/// <param name="offset">transform origin offset</param>
		/// <returns>Yes or no</returns>

		public static bool IsInLineOfSight (this Transform origin, Vector3 target, float fieldOfView, LayerMask collisionMask, Vector3 offset)
		{

			Vector3 direction = target - origin.position;

			if (Vector3.Angle (origin.forward, direction.normalized) < fieldOfView / 2) {
				float distanceToTarget = Vector3.Distance (origin.position, target);

				// something blocking our view?
				if (Physics.Raycast (origin.position + offset + origin.forward * .3f, direction.normalized, distanceToTarget, collisionMask)) {
					return false;
				}

				return true;
			}

			return false;
		}
	}
}
