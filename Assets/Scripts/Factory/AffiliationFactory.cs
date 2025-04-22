using affiliation;
using DefaultNamespace;

namespace Factory
{
    public class AffiliationFactory
    {
        public Affiliation createAffiliation(AffiliationEnum affiliationEnum)
        {
            switch (affiliationEnum)
            {
                case AffiliationEnum.BLUE:
                    return new BlueAffiliation();
                case AffiliationEnum.RED:
                    return new RedAffiliation();
                case AffiliationEnum.NEUTRAL:
                    return NatureAffiliation.instance;
                default:
                    return null;
            }
        }

        public AffiliationEdge createAffiliationEdge(Affiliation source, Affiliation target,
            AffiliationEdgeEnum affiliationEdgeEnum)
        {
            switch (affiliationEdgeEnum)
            {
                case AffiliationEdgeEnum.ATTACK:
                    return new AttackAffiliationEdge(source, target);
                default:
                    return null;
            }
        }

    }
}