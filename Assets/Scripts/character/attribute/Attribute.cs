using System;
using System.Text.RegularExpressions;

namespace character.attribute
{
    public class Attribute
    {
        private const float TOLERANCE = 0.0001f;
        
        public AttributeEnum attributeEnum;

        private float _value;
        
        public delegate void OnValueChange(float value);
        public event OnValueChange onValueChange, onValueMax, onValueMin;
        
        public float value
        {
            get => this._value;
            set
            {
                if (Math.Abs(value - this._value) > TOLERANCE)
                {
                    if (value <= minValue)
                    {
                        _value = minValue;
                        onValueMin?.Invoke(_value);
                    }else if (value >= maxValue)
                    {
                        _value = maxValue;
                        onValueMax?.Invoke(_value);
                    }
                    else
                    {
                        _value = value;
                    }
                    onValueChange?.Invoke(_value);
                }
            }
        }

        public string description;
        
        public float maxValue;
        
        public float minValue;
        
        public Attribute(float value, float maxValue, float minValue)
        {
            this.attributeEnum = AttributeEnum.NULL;
            this.description = "description";
            this._value = value;
            this.maxValue = maxValue;
            this.minValue = minValue;
        }
        
        public virtual void increaseValue(float increaseValue)
        {
            value += increaseValue;
        }
        
        public virtual void decreaseValue(float decreaseValue)
        {
            value -= decreaseValue;
        }
        
        public bool isLessThanMinValue()
        {
            return value <= minValue;
        }
        
        
    }
}