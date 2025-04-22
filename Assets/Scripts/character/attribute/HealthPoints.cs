namespace character.attribute
{
    public class HealthPoints : Attribute
    {
        public HealthPoints(float value, float maxValue, float minValue) : base(value, maxValue, minValue)
        {
            this.attributeEnum = AttributeEnum.HP;
            this.description = "health point";
        }
    }
}