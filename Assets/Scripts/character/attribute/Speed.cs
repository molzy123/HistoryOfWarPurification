namespace character.attribute
{
    public class Speed : Attribute
    {
        public Speed(float value, float maxValue, float minValue) : base(value, maxValue, minValue)
        {
            this.attributeEnum = AttributeEnum.SPD;
            this.description = "speed";
        }
    }
}