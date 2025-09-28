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
        protected RButton btnDebug;
        private IRComponent[] components;
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
            btnDebug = CreateButton("Debug", _ =>
            {
                panelShow = !panelShow;
                if (panelShow)
                {
                    OnShow();
                    components = transform.GetComponentsInChildren<IRComponent>();
                }
                else
                {
                    OnHide();
                }
            });
        }

        protected void Update()
        {
            if (!panelShow) return;
            if (components == null) return;
            foreach (var component in components)
            {
                component.OnUpdate();
            }
        }

        protected abstract void OnShow();

        public void OnHide()
        {
            panelShow = false;
            if (components == null) return;
            foreach (var component in components)
            {
                if (component is RButton rButton && rButton == btnDebug) continue;
                if (component is MonoBehaviour monoBehaviour)
                    Destroy(monoBehaviour.gameObject);
            }

            components = null;
        }

        protected void UsingHorizontalLayoutGroup(Action action, int height = 100)
        {
            var lastTransform = currentTransform;
            var layoutGroup = new GameObject("HorizontalLayoutGroup", typeof(RHorizontalLayoutGroup))
                .GetComponent<RHorizontalLayoutGroup>();
            layoutGroup.transform.SetParent(currentTransform, false);
            layoutGroup.Init(height);
            currentTransform = layoutGroup.transform;
            action.Invoke();
            currentTransform = lastTransform;
        }

        protected RButton CreateButton(string name, Action<RButton> onClick, int width = 100,
            int height = 100, int fontSize = 30, Action<RButton> onUpdate = null)
        {
            var btn = new GameObject(name, typeof(RButton)).GetComponent<RButton>();
            btn.transform.SetParent(currentTransform, false);
            btn.Init(name, onClick, width, height, fontSize, TextColor, BgColor, BgSprite, onUpdate);
            return btn;
        }

        protected RInputField CreateInputField(string name, UnityAction<string> onValueChanged, int width = 100,
            int height = 100, int fontSize = 30,
            string defaultValue = "", Action<RInputField> onUpdate = null)
        {
            var inputField = new GameObject(name, typeof(RInputField)).GetComponent<RInputField>();
            inputField.transform.SetParent(currentTransform, false);
            inputField.Init(name, onValueChanged, width, height, fontSize, defaultValue, TextColor, BgColor, BgSprite, onUpdate);
            return inputField;
        }

        protected RLabel CreateLabel(string name, int width = 100, int height = 100, int fontSize = 30,
            Action<RLabel> onUpdate = null)
        {
            var label = new GameObject(name, typeof(RLabel)).GetComponent<RLabel>();
            label.transform.SetParent(currentTransform, false);
            label.Init(name, width, height, fontSize, TextColor, BgColor, BgSprite, onUpdate);
            return label;
        }
    }
}