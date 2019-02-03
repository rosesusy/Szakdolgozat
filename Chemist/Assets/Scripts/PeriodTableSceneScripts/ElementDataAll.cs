using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chemist
{
    public class ElementDataAll
    {
        public List<ElementData> elements;

        public static ElementDataAll FromJSON(string json)
        {
            return JsonUtility.FromJson<ElementDataAll>(json);
        }
    }
}
