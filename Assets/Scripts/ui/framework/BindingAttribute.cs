using System;

namespace ui.frame
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BindingAttribute : Attribute
    {
        public BindingAttribute(string uiName = null)
        {
            this.uiName = uiName;
        }

        public string uiName { get; }
    }
}