/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
 *
*/
using System;
using System.Collections.Generic;

namespace MD.EnumDictionary.Extensions
{
    public static class EnumDictionaryExtension
    {
        /// <summary>
        /// Extension for enum dictionaries<br></br><br></br>
        /// <b>Returns</b> both dictionary value.
        /// </summary>
        public static (Type, Type) GetValuesType<T, T1, T2>(this List<EnumDictionaryBase<T, T1, T2>> list) where T : Enum { return (typeof(T1), typeof(T2)); }

        /// <summary>
        /// Extension for enum dictionaries <br></br><br></br>
        /// <b>Returns</b> the values of types.
        /// </summary>
        /// <param name="key">Enum key</param>
        public static (T1 Value1, T2 Value2) GetValues<T, T1, T2>(this List<EnumDictionaryBase<T, T1, T2>> list, T key) where T : Enum { return (list[(int)(object)key].val1, list[(int)(object)key].val2); }

        /// <summary>
        /// Extension for enum dictionaries <br></br><br></br>
        /// <b>Returns</b> first value type.
        /// </summary>
        /// <param name="key">Enum key</param>
        public static T1 GetFirstValue<T, T1, T2>(this List<EnumDictionaryBase<T, T1, T2>> list, T key) where T : Enum { return list[(int)(object)key].val1; }

        /// <summary>
        /// Extension for enum dictionaries <br></br><br></br>
        /// <b>Returns</b> Second value type.
        /// </summary>
        /// <param name="key">Enum key</param>
        public static T2 GetSecondValue<T, T1, T2>(this List<EnumDictionaryBase<T, T1, T2>> list, T key) where T : Enum { return list[(int)(object)key].val2; }

        /// <summary>
        /// Extension for enum dictionaries <br></br><br></br>
        /// </summary>
        /// <param name="Value">first value</param>
        public static void SetFirstValue<T, T1, T2>(this List<EnumDictionaryBase<T, T1, T2>> list, T key, T1 Value) where T : Enum { list[(int)(object)key].val1 = Value; }

        /// <summary>
        /// Extension for enum dictionaries <br></br><br></br>
        /// </summary>
        /// <param name="Value">Second value</param>
        public static void SetSecondValue<T, T1, T2>(this List<EnumDictionaryBase<T, T1, T2>> list, T key, T2 Value) where T : Enum { list[(int)(object)key].val2 = Value; }


        /// <summary>
        /// Reorder and serialize enum dictionary by enum length <br></br><br></br>
        /// </summary>
        public static void Reorder<T, T1, T2>(this List<EnumDictionaryBase<T, T1, T2>> _T1) where T : Enum
        {
            int EnumLenght()
            {
                return System.Enum.GetNames(typeof(T)).Length;
            }

            void ReOrder()
            {
                while (_T1.Count != EnumLenght())
                {
                    if (_T1.Count > EnumLenght())
                        _T1.RemoveAt(EnumLenght() - 1);
                    else if (_T1.Count < EnumLenght())
                        _T1.Add(new EnumDictionaryBase<T, T1, T2>
                        (_T1[0].key, _T1[0].val1, _T1[0].val2));
                }

                for (int i = 0; i < EnumLenght(); i++)
                {
                    _T1[i].key = (T)(object)i;
                    _T1[i].IsEnumFieldEditable = true;
                    _T1[i].name = " ";
                }
            }

            ReOrder();
        }
    }
}

