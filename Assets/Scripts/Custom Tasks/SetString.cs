using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
namespace BBUnity.Actions
{

    [Action("Basic/SetString")]
    [Help("Sets a value to a string variable")]
    public class SetString : BasePrimitiveAction
    {
        [OutParam("var")]
        [Help("output variable")]
        public string var;

        [InParam("value")]
        [Help("Value")]
		public string value;

        public override void OnStart()
        {
            var = value;
        }

        public override TaskStatus OnUpdate()
        {
			Debug.Log ("set state to:" + value);
            return TaskStatus.COMPLETED;
        }
    }
}
