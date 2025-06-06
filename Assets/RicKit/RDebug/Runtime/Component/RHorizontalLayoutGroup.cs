using System;
using UnityEngine;
using UnityEngine.UI;

namespace RicKit.RDebug.Component
{
    public class RHorizontalLayoutGroup : RComponent
    {
        public void Init(float height)
        {
            var layoutGroup = gameObject.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
            layoutGroup.childControlWidth = false;
            layoutGroup.childControlHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
        }
    }
}