# UICore Setup Instructions

## Overview
The `UICore` system manages the Pause, Victory, and Game Over panels from a persistent `OptionsScene`.
The `GameManager` now handles level loading and automatically loads the `OptionsScene` additively.

## Manual Setup Required

1.  **OptionsScene Setup**:
    - Open `OptionsScene`.
    - Create a new GameObject called `UICoreHolder` (or use the Canvas).
    - Add the `UICore` script to it.
    - **Inspector**: Drag your panels (`PausaPanel`, `VictoriaPanel`, `GameOverPanel`) to the `UICore` slots.
    - **Buttons**:
        - `UICore` attempts to auto-find buttons named:
            - `ContinuarBoton`, `MenuBoton`, `SiguienteBoton` (in Pause)
            - `SiguienteBoton`, `MenuBoton` (in Victory)
            - `ReiniciarBoton`, `MenuBoton` (in GameOver)
        - If your buttons are named differently, rename them in the hierarchy OR drag them manually into the `UICore` inspector slots.

2.  **GameManager**:
    - Ensure `GameManager` script is present (commonly in `SelectLevels` or `MainMenu` or initialized via Singleton).
    - It is set to `DontDestroyOnLoad`, so once created, it persists.
    - Verify the `Levels` list in `GameManager` inspector matches your scene names exactly:
        - `Space_One`
        - `Space_Two`
        - `Space_Three`

3.  **Testing**:
    - Start from `MainMenu` -> `SelectLevels` -> `Space_One`.
    - Press Escape -> Pause Panel should appear.
    - Die -> Game Over Panel should appear.
    - Win -> Victory Panel should appear.

## Troubleshooting
- If "OptionsScene" fails to load, check the `optionsSceneName` in `GameManager` inspector.
- If buttons don't work, check the Console logs for `[UICore]` messages.
