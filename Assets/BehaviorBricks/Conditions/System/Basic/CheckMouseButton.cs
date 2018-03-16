using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckMouseButton")]
    [Help("Checks whether a mouse button is pressed")]
    public class CheckMouseButton : ConditionBase
    {

        public enum MouseButton {left = 0, right = 1, center = 2};
        
        [InParam("button", DefaultValue = MouseButton.left)]
        [Help("Mouse button expected to be pressed")]
        public MouseButton button = MouseButton.left;

        
        public enum MouseAction {down, up, during}
        [InParam("mouseAction", DefaultValue = MouseAction.during)]
        public MouseAction mouseAction = MouseAction.during;

        public override bool Check()
        {
			bool didClick = false;
            switch (mouseAction)
            {
			case MouseAction.down:
				didClick = Input.GetMouseButtonDown ((int)button);
				break;
                case MouseAction.up:
					didClick = Input.GetMouseButtonUp((int)button);
				break;
                case MouseAction.during:
					didClick = Input.GetMouseButton((int)button);
				break;
            }

			return didClick;
		}
    }
}