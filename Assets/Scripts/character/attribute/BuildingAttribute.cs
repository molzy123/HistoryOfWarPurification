namespace character.attribute
{
    public class BuildingAttribute : AttributeCollection
    {

        

        public bool isLive()
        {
            return isLessThanMinValue(AttributeEnum.HP);
        }
        
        
    }
}