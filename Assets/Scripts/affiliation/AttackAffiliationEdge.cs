namespace affiliation
{
    public class AttackAffiliationEdge : AffiliationEdge
    {
        public AttackAffiliationEdge(Affiliation source, Affiliation target) : base(source, target)
        {
            edgeType = AffiliationEdgeEnum.ATTACK;
        }
    }
}