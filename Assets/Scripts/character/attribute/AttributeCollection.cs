using System.Collections.Generic;

namespace character.attribute
{
    public class AttributeCollection
    {
        private readonly Dictionary<AttributeEnum, Attribute> _attributeMap = new Dictionary<AttributeEnum, Attribute>();

        public void setOrAddAttribute(Attribute attribute)
        {
            if (!_attributeMap.ContainsKey(attribute.attributeEnum))
            {
                _attributeMap.Add(attribute.attributeEnum, attribute);
            }
            else
            {
                _attributeMap[attribute.attributeEnum].increaseValue(attribute.value);
            }
        }

        public void removeAttribute(AttributeEnum attributeEnum)
        {
            _attributeMap.Remove(attributeEnum);
        }

        public float getAttributeValue(AttributeEnum attributeEnum)
        {
            if (getAttribute(attributeEnum) == null)
            {
                return 0;
            }

            return _attributeMap[attributeEnum].value;
        }

        public Attribute getAttribute(AttributeEnum attributeEnum)
        {
            if (_attributeMap.ContainsKey(attributeEnum) == false)
            {
                return null;
            }

            return _attributeMap[attributeEnum];
        }

        public bool isLessThanMinValue(AttributeEnum attributeEnum)
        {
            if (_attributeMap.ContainsKey(attributeEnum) == false)
            {
                return false;
            }

            return _attributeMap[attributeEnum].isLessThanMinValue();
        }
    }
}