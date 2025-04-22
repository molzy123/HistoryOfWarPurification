namespace character.attribute
{
    public class Attack : Attribute
    {
        public Attack(float value, float maxValue, float minValue) : base(value, maxValue, minValue)
        {
            this.attributeEnum = AttributeEnum.ATK;
            this.description = "attack";
        }
    }
}