using DefaultNamespace;

namespace affiliation
{
    public class NatureAffiliation : Affiliation
    {

        NatureAffiliation()
        {
            affiliation = AffiliationEnum.NEUTRAL;
        }
        
        public static NatureAffiliation instance { get; } = new NatureAffiliation();
        
    }
}