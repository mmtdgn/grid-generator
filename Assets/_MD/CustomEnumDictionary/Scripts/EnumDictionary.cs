/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
 *
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MD.EnumDictionary
{
    public abstract class EnumDictionary<T, T1, T2> : MonoBehaviour where T : Enum, IConvertible
    {
        [HideInInspector] private T key;
        [HideInInspector] private T1 val1;
        [HideInInspector] private T2 val2;

        public List<EnumDictionaryBase<T, T1, T2>> _T1;

        /// <summary>
        /// Base class method for enum dictionary<br></br><br></br>
        /// <b>Returns</b> Enum length
        /// </summary>
        public int EnumLenght()
        {
            return System.Enum.GetNames(typeof(T)).Length;
        }

        /// <summary>
        /// Generate empty dictionary by enum length<br></br><br></br>
        /// </summary>
        public void ReOrder()
        {
            _T1.Clear();

            for (int i = 0; i < EnumLenght(); i++)
            {
                _T1.Add(new EnumDictionaryBase<T, T1, T2>(key, val1, val2));
                SetterUp(i);
            }
        }

        /// <summary>
        /// Checks enum lenghts and updates<br></br><br></br>
        /// </summary>
        public void DictionaryLenghtCheck()
        {
            while (_T1.Count != EnumLenght())
            {
                if (_T1.Count > EnumLenght())
                    _T1.RemoveAt(EnumLenght() - 1);
                else if (_T1.Count < EnumLenght())
                    _T1.Add(new EnumDictionaryBase<T, T1, T2>(key, val1, val2));
            }
            for (int i = 0; i < _T1.Count; i++)
            {
                SetterUp(i);
            }
        }

        /// <summary>
        /// Set default dictionary values<br></br><br></br>
        /// </summary>
        private void SetterUp(int index)
        {
            _T1[index].key = (T)(object)index;
            _T1[index].IsEnumFieldEditable = true;
            _T1[index].name = " ";//Clear Element index name/ (R&D: `String.Empty` Not working?)
        }

        /// <summary>
        /// Set default dictionary values<br></br><br></br>
        /// </summary>
        public virtual void OnValidate()
        {
            if (EnumLenght() == 0)
                ReOrder();
            DictionaryLenghtCheck();
        }
    }
}