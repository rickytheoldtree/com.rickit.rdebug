# RicKit RDebug

[![openupm](https://img.shields.io/npm/v/com.rickit.rdebug?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.rickit.rdebug/)

## 简介

RicKit RDebug 是一个基于 Unity 的调试面板工具，支持快速创建自定义调试 UI。通过继承 `RDebug` 抽象类，可以方便地添加按钮、输入框等控件，实现运行时调试和参数调整。

---

## 主要特性

- 一键生成调试面板
- 支持按钮、输入框等常用控件
- 支持自定义布局（垂直/水平）
- 适用于 Unity MonoBehaviour

---

## 快速开始

1. 新建一个类继承 `RDebug`，实现 `OnShow()` 方法：

```csharp
using RicKit.RDebug;
using UnityEngine;

public class MyDebugPanel : RDebug
{
    protected override void OnShow()
    {
        UsingHorizontalLayoutGroup(() =>
        {
            CreateButton("打印日志", () => Debug.Log("点击了按钮"));
            CreateInputField("输入内容", value => Debug.Log($"输入: {value}"));
        });
    }
}
```

2. 在场景中挂载该组件，并调用 `Init()` 初始化：

```csharp
public class DebugEntry : MonoBehaviour
{
    void Start()
    {
        var panel = gameObject.AddComponent<MyDebugPanel>();
        panel.Init();
    }
}
```

---

## API 说明

### 继承点

- `protected abstract void OnShow()`
  - 实现自定义面板内容

### 常用方法

- `CreateButton(string name, UnityAction onClick, int width = 100, int height = 100, int fontSize = 30)`
  - 创建按钮

- `CreateInputField(string name, UnityAction<string> onValueChanged, int width = 100, int height = 100, int fontSize = 30, string defaultValue = "")`
  - 创建输入框

- `UsingHorizontalLayoutGroup(Action action, int height = 100)`
  - 在水平方向布局一组控件

---

## 注意事项

- 需在 Unity 环境下使用
- 需挂载在场景对象上
- 控件样式可根据需求自定义

---

## 开源协议

Apache License

---

## 相关链接

- [GitHub 仓库](https://github.com/rickytheoldtree/com.rickit.rdebug)
- [OpenUPM 页面](https://openupm.com/packages/com.rickit.rdebug/)
