using Pada1.BBCore.Framework;
using Pada1.BBCore;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckString")]
    [Help("Checks whether two strings have the same value")]
    public class CheckString : ConditionBase
    {
        [InParam("valueA")]
        [Help("First value to be compared")]
        public string valueA;

        [InParam("valueB")]
        [Help("Second value to be compared")]
		public string valueB;

		public override bool Check()
		{
			return valueA == valueB;
		}
    }
}