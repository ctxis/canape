
namespace CANAPE.Utils
{    
    /// <summary>details of one difference.</summary>
    public struct DiffItem
    {
        /// <summary>Start Line number in Data A.</summary>
        public int StartA;
        /// <summary>Start Line number in Data B.</summary>
        public int StartB;

        /// <summary>Number of changes in Data A.</summary>
        public int deletedA;
        /// <summary>Number of changes in Data B.</summary>
        public int insertedB;
    }  
}
