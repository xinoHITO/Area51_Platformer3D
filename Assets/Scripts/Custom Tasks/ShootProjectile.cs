using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("Basic/ShootProjectile")]
    [Help("Creates a projectile and sets the it rigidbody's velocity towards the target")]
	public class ShootProjectile : GOAction
    {
		[InParam("projectile")]
		[Help("Projectile prefab")]
		public Rigidbody projectile;

		[InParam("target")]
		[Help("Target to shoot")]
		public GameObject target;

		[InParam("speed")]
		[Help("The projectile's speed toward the target")]
		public float speed;

        public override TaskStatus OnUpdate()
        {
			if (target == null) {
				return TaskStatus.FAILED;
			} else {
				Vector3 dir = target.transform.position-gameObject.transform.position;
				dir.Normalize ();

				Rigidbody newProjectile =Object.Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
				newProjectile.velocity = dir * speed;
				return TaskStatus.COMPLETED;
			}

        }
    }
}
