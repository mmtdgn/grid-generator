/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
 *
*/
using System;

namespace MD.EnumDictionary
{
    [Serializable]
    public class EnumDictionaryBase<Tkey, Tval1, Tval2> where Tkey : Enum
    {
        public string name;
        public Tkey key;
        public Tval1 val1;
        public Tval2 val2;
        public bool IsEnumFieldEditable;

        /// <summary>
        /// Constructor for dictionary element, defines key and values.<br></br><br></br>
        /// </summary>
        /// <param name="tKey">Key for get the value(Enum)</param>
        /// <param name="tval1">Dictionary first value</param>
        /// <param name="tval2">Dictionary Second value</param>
        public EnumDictionaryBase(Tkey tkey, Tval1 tval1, Tval2 tval2)
        {
            name = " ";
            this.key = tkey;
            this.val1 = tval1;
            this.val2 = tval2;
            IsEnumFieldEditable = false;
        }
    }
}