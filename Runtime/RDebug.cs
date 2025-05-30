using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RicKit.RDebug
{
    public abstract class RDebug : MonoBehaviour
    {
        protected virtual void Awake()
        {
            var layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = false;
            layoutGroup.childControlHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
        }

        private Button btnDebug;

        public void Init()
        {
            currentTransform = transform;
            btnDebug = CreateButton( "Debug", () =>
            {
                panelShow = !panelShow;
                if (panelShow)
                    OnShow();
                else
                {
                    foreach (Transform child in transform)
                    {
                        if (child == btnDebug.transform) continue;
                        Destroy(child.gameObject);
                    }
                }
            });
        }
        
        private bool panelShow;
        protected abstract void OnShow();
        private Transform currentTransform;
        protected void UsingHorizontalLayoutGroup(Action action, int height = 100)
        {
            var lastTransform = currentTransform;
            var layoutGroup =
                new GameObject("HorizontalLayoutGroup", typeof(RectTransform), typeof(HorizontalLayoutGroup))
                    .GetComponent<HorizontalLayoutGroup>();
            currentTransform = layoutGroup.transform;
            layoutGroup.transform.SetParent(transform, false);
            layoutGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
            layoutGroup.childControlWidth = false;
            layoutGroup.childControlHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
            action.Invoke();
            currentTransform = lastTransform;
        }

        protected Button CreateButton(string name, UnityAction onClick, int width = 100,
            int height = 100, int fontSize = 30)
        {
            var button =
                new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button)).GetComponent<Button>();
            button.targetGraphic.color = new Color(1, 1, 1, 0.7f);
            button.transform.SetParent(currentTransform, false);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            var txt = new GameObject("Text", typeof(Text)).GetComponent<Text>();
            txt.transform.SetParent(button.transform, false);
            var rtTxt = txt.GetComponent<RectTransform>();
            rtTxt.anchorMin = Vector2.zero;
            rtTxt.anchorMax = Vector2.one;
            rtTxt.sizeDelta = Vector2.zero;
            txt.text = name;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.alignment = TextAnchor.MiddleCenter;
            txt.color = Color.black;
            txt.fontSize = fontSize;
            button.onClick.AddListener(onClick);
            return button;
        }

        protected InputField CreateInputField(string name,
            UnityAction<string> onValueChanged, int width = 100, int height = 100, int fontSize = 30, string defaultValue = "")
        {
            var inputField = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(InputField))
                .GetComponent<InputField>();
            inputField.targetGraphic.color = new Color(1, 1, 1, 0.7f);
            inputField.transform.SetParent(currentTransform, false);
            inputField.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            var txt = new GameObject("Text", typeof(Text)).GetComponent<Text>();
            inputField.textComponent = txt;
            txt.transform.SetParent(inputField.transform, false);
            var rtTxt = txt.GetComponent<RectTransform>();
            rtTxt.anchorMin = Vector2.zero;
            rtTxt.anchorMax = Vector2.one;
            rtTxt.sizeDelta = new Vector2(-10, 0);
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.alignment = TextAnchor.MiddleLeft;
            txt.color = Color.black;
            txt.fontSize = fontSize;
            var placeholder = new GameObject("Placeholder", typeof(Text)).GetComponent<Text>();
            inputField.placeholder = placeholder;
            placeholder.transform.SetParent(inputField.transform, false);
            var rtPlaceholder = placeholder.GetComponent<RectTransform>();
            rtPlaceholder.anchorMin = Vector2.zero;
            rtPlaceholder.anchorMax = Vector2.one;
            rtPlaceholder.sizeDelta = new Vector2(-10, 0);
            placeholder.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            placeholder.alignment = TextAnchor.MiddleLeft;
            placeholder.color = Color.gray;
            placeholder.fontSize = fontSize;
            placeholder.text = $"{name}";
            inputField.text = defaultValue;
            inputField.onValueChanged.AddListener(onValueChanged);
            return inputField;
        }
    }
}