# Changelog
## [1.2.2] - 2025-09-26
- Replaced IHaveTag with IRComponent, introducing an OnUpdate method for all custom UI components. Updated RButton, RLabel, RInputField, and RHorizontalLayoutGroup to implement IRComponent and support per-frame updates via OnUpdate. Refactored RDebug to manage and update IRComponent instances, removed tag-based component management, and improved extensibility for UI updates.
## [1.1.5] - 2025-06-06
- Add RButton, RLabel, RInputField, and RHorizontalLayoutGroup components; update RDebug class for improved UI management
## [1.1.0] - 2025-06-04
- refactor RDebug class to manage UI components more effectively
## [1.0.3] - 2025-05-30
- Update README and RDebug class for enhanced customization options
## [1.0.1] - 2025-05-30
- RDebug class for improved button handling and color customization
## [1.0.0] - 2025-05-30
### This is the first release of *com.rickit.rdebug*.
- first commit