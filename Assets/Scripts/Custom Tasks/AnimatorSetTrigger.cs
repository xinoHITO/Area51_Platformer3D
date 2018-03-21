using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{

	[Action("Animator/SetTrigger")]
    [Help("Activates a trigger parameter in the animator")]
	public class AnimatorSetTrigger : GOAction
    {
		[InParam("Trigger name")]
		[Help("Name of the trigger parameter")]
		public string triggerName;

		private Animator _animator;

		public override void OnStart ()
		{
			base.OnStart ();
			if (_animator == null) {
				_animator = gameObject.GetComponent<Animator> ();
			}
		} 
        public override TaskStatus OnUpdate()
        {
	
			_animator.SetTrigger (triggerName);
			return TaskStatus.COMPLETED;

        }
    }
}
