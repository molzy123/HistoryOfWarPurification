using character.solider;

namespace character
{
    public interface ISoliderBuilder
    {

        /**
         * Set the affiliation of the solider
         */
        void setAffiliation();
        
        /**
         * Set the UI of the solider
         */
        void setSoliderUI();
        
        /*
         * Set the character attribute of the solider
         */
        void setCharacterAttribute();
        
        /*
         * Execute the initialize method of the solider
         */
        void executeInitialize();
        
        /**
         * Set the move controller of the solider
         */
        void setMoveController();
        
        /**
         * Add the solider to the manager
         */
        void addSoliderToManager();
        
        
        
        /*
         * Get the solider
         */
        Solider getSolider();
    }
}