using character.solider;

namespace character
{
    public class SoliderDirector
    {
        private readonly SoliderBuilder _builder;
        
        public SoliderDirector(SoliderBuilder builder)
        {
            _builder = builder;
        }
        
        public Solider construct()
        {
            _builder.setAffiliation();
            _builder.setSoliderUI();
            _builder.setCharacterAttribute();
            _builder.setMoveController();
            _builder.executeInitialize();
            _builder.addSoliderToManager();
            return _builder.getSolider();
        }
        
    }
}