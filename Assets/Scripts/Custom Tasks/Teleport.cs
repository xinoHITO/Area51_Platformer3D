using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("Enemy/Teleport")]
    [Help("Teleports a random point around the enemy")]
	public class Teleport : GOAction
    {
		[InParam("Target")]
		[Help("The target to teleport around")]
		public GameObject target;

		[InParam("distance")]
		[Help("How much farther the teleport travels")]
		public float distance;

        public override TaskStatus OnUpdate()
        {
			Vector3 dirVector = Random.onUnitSphere;
			dirVector.y = 0;
			dirVector.Normalize ();
			gameObject.transform.position = target.transform.position + (dirVector * distance);
			return TaskStatus.COMPLETED;
        }
    }
}
