using UnityEngine;
using UnityEngine.UI;

namespace RicKit.RDebug.Component
{
    public class RHorizontalLayoutGroup : HorizontalLayoutGroup, IHaveTag
    {
        public string Tag { get; set; }
        public void Init(float height)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
            childControlWidth = false;
            childControlHeight = false;
            childForceExpandWidth = false;
            childForceExpandHeight = false;
        }
    }
}