using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RicKit.RDebug.Component
{
    public class RButton : Button, IHaveTag
    {
        public string Tag { get; set; }
        public void Init(string name, UnityAction onClick,int width, int height, int fontSize,
            Color textColor, Color bgColor, Sprite bgSprite)
        {
            var img = gameObject.AddComponent<Image>();
            targetGraphic = img;
            if (bgSprite)
            {
                img.sprite = bgSprite;
                if (bgSprite.border != Vector4.zero)
                    img.type = Image.Type.Sliced;
            }

            img.color = bgColor;
            GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            var txt = new GameObject("Text", typeof(Text)).GetComponent<Text>();
            txt.transform.SetParent(transform, false);
            var rtTxt = txt.GetComponent<RectTransform>();
            rtTxt.anchorMin = Vector2.zero;
            rtTxt.anchorMax = Vector2.one;
            rtTxt.sizeDelta = Vector2.zero;
            txt.text = name;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.alignment = TextAnchor.MiddleCenter;
            txt.color = textColor;
            txt.fontSize = fontSize;
            this.onClick.AddListener(onClick);
        }
    }
}