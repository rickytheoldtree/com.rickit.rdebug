using System;
using System.Collections.Generic;
using RicKit.RDebug.Component;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RicKit.RDebug
{
    public abstract class RDebug : MonoBehaviour
    {
        private bool panelShow;
        private Transform currentTransform;
        protected Color TextColor { get; set; } = Color.white;
        protected Color BgColor { get; set; } = new Color(0, 0f, 0, 0.7f);
        protected Sprite BgSprite { get; set; } = null;

        protected virtual void Awake()
        {
            var layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = false;
            layoutGroup.childControlHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
        }

        private void Start()
        {
            currentTransform = transform;
            CreateButton("Debug", () =>
            {
                panelShow = !panelShow;
                if (panelShow)
                    OnShow();
                else
                    OnHide();
            }, tag: "Debug");
        }

        protected abstract void OnShow();

        public void OnHide()
        {
            panelShow = false;
            foreach (var component in transform.GetComponentsInChildren<RComponent>())
            {
                if(component.Tag == "Debug") continue;
                Destroy(component.gameObject);
            }
        }
        protected T GetComponents<T>(string tag) where T : RComponent
        {
            var components = new List<T>();
            foreach (var component in transform.GetComponentsInChildren<RComponent>())
            {
                if (component.Tag == tag)
                {
                    if (component is T tComponent)
                    {
                        components.Add(tComponent);
                    }
                }
            }
            return components.Count > 0 ? components[0] : null;
        }

        protected void UsingHorizontalLayoutGroup(Action action, int height = 100)
        {
            var lastTransform = currentTransform;
            var layoutGroup = new GameObject("HorizontalLayoutGroup", typeof(RHorizontalLayoutGroup)).GetComponent<RHorizontalLayoutGroup>();
            layoutGroup.Tag = "HorizontalLayoutGroup";
            layoutGroup.transform.SetParent(currentTransform, false);
            layoutGroup.Init(height);
            currentTransform = layoutGroup.transform;
            action.Invoke();
            currentTransform = lastTransform;
        }

        protected void CreateButton(string name, UnityAction onClick, int width = 100,
            int height = 100, int fontSize = 30, string tag = "Button")
        {
            var btn = new GameObject(tag, typeof(RButton)).GetComponent<RButton>();
            btn.Tag = tag;
            btn.transform.SetParent(currentTransform, false);
            btn.Init(name, onClick, width, height, fontSize, TextColor, BgColor, BgSprite);
        }

        protected void CreateInputField(string name, UnityAction<string> onValueChanged, int width = 100, int height = 100, int fontSize = 30,
            string defaultValue = "", string tag = "InputField")
        {
            var inputField = new GameObject(name, typeof(RInputField)).GetComponent<RInputField>();
            inputField.Tag = tag;
            inputField.transform.SetParent(currentTransform, false);
            inputField.Init(name, onValueChanged, width, height, fontSize, defaultValue, TextColor, BgColor, BgSprite);
        }

        protected void CreateLabel(string name, int width = 100, int height = 100, int fontSize = 30, string tag = "Label")
        {
            var label = new GameObject(name, typeof(RLabel)).GetComponent<RLabel>();
            label.Tag = tag;
            label.transform.SetParent(currentTransform, false);
            label.Init(name, width, height, fontSize, TextColor, BgColor, BgSprite);
        }
    }
}