using Pada1.BBCore;
using UnityEngine;
namespace BBUnity.Conditions
{
	[Condition("Enemy/Is hurt")]
    [Help("Checks wether the enemy is in knockback or dead")]
	public class IsEnemyHurt : GOCondition
    {
		
		private EnemyAI _aiScript;

		public override bool Check()
        {
			if (_aiScript == null) {
				_aiScript = gameObject.GetComponent<EnemyAI> ();
			}

			if (_aiScript.IsHurt ()) {
				Debug.Log ("enemy is hurt");
				return true;
			} else {
				Debug.Log ("enemy is NOT hurt");
				return false;
			}

        }
    }
}
