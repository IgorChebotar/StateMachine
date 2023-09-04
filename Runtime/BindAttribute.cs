using System;

namespace SimpleMan.StateMachine
{
    public class BindAttribute : Attribute
    {
        public readonly Type bindedType;

        public BindAttribute(Type bindedType)
        {
            this.bindedType = bindedType;
        }
    }
}


