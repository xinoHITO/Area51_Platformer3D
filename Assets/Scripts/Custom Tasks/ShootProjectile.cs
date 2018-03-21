using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("Enemy/ShootProjectile")]
    [Help("Creates a projectile and sets the it rigidbody's velocity towards the target")]
	public class ShootProjectile : GOAction
    {
		[InParam("projectile")]
		[Help("Projectile prefab")]
		public Rigidbody projectile;

		[InParam("ShootPoint")]
		[Help("Bullet spawn point")]
		public GameObject shootPoint;


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

				Rigidbody newProjectile;
				if (shootPoint == null) {
					newProjectile = Object.Instantiate (projectile, gameObject.transform.position, gameObject.transform.rotation);
				} else {
					newProjectile = Object.Instantiate(projectile, shootPoint.transform.position, gameObject.transform.rotation);
				}
				newProjectile.velocity = dir * speed;
				return TaskStatus.COMPLETED;
			}

        }
    }
}
