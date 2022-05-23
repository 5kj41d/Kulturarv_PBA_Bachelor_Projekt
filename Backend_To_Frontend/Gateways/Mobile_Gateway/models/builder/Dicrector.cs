using System; 
using System.Collections.Generic;

namespace builder
{
    public class Director
    {
        BuilderIF builderIF = new Recieve_Model(); 
        public IEnumerable<string> Make_Get_All_Recieve_Model()
        {
            //TODO: Use builderIF to create reviece model. 
            return null; 
        }
        public static IEnumerable<string> Make_Get_By_Region_Model()
        {
            return null; 
        }
        public static IEnumerable<string> Make_Get_By_Type_Model()
        {
            return null; 
        }
        public static IEnumerable<string> Make_Get_By_TimeAge_Model()
        {
            return null; 
        }
    }
}