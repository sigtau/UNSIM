About UNSIM
===========

UNSIM (Unity Non-Touch Simple Input Manager) is a very simple and lightweight input manager for Unity-based games that require the use of joysticks, mouse/keyboard, or a combination of the two.  In addition to supporting remappable input, it supports profiles as well, allowing users to save profiles or choose profiles of their choice later on.

UNSIM's API is designed to be a drop-in for Unity's built-in input, and has all of the same functions (GetButton, GetAxis, etc).  The source code for UNSIM is released under the 2-clause BSD license, free to use (with attribution) in any project, commercial or noncommercial.

UNSIM was written by Will Preston as a substitute for Unity's built-in input to aid in testing and deployment for a student project at the Savannah College of Art & Design.

Using UNSIM
===========

Usage of UNSIM is very simple:

1. Drop all of the .cs scripts in the source tree in your project's Assets folder.  They can be put anywhere, but I recommend creating a "Scripts" or "Input" folder to keep things organized.
2. On your player object, or whatever you intend to be accepting input, add the InputManager script as a new component by dragging the script onto the object Inspector.
3. In the Inspector, under the **Profiles** section of the Input Manager script, open the **Prototypes** tree by clicking on its name.  Change the "Elements" value to however many controls (buttons, axes, etc.) you intend to have, and press Enter.  This is similar to the **Axes Size** value in Unity's built-in Input Settings.
4. For each of the values that appear within the **Prototypes** tree after doing this, open each of the elements in the list by selecting the element name (**Element 0**, etc.) and set what you want each button or axis to do by default.  

For example, to add a "Crouch" button for the C key on the keyboard, you would set the **Type** to **BUTTON**, set the handle to **"Crouch,"** and set the **Key Code** to **C**.

To set an Axis, you'll first need to verify that the axis is defined in the Unity input manager (this is the only step that requires you work with Unity's built-in input).  By default in Unity, the "Horizontal" and "Vertical" axes are already set as the first joystick axis, WASD, and the arrow keys, so we'll use this as an example.  Set the handle to **"Strafe,"** the type to **AXIS_JOYSTICK**, the Axis Name to **Horizontal**, and the Key Code to **None**.

That's it!  Now, in your code, all you have to do is refer to the InputManager script instead of the Unity Input class.

    InputManager input;
    
    void Start() {
        input = InputManager.getInstance(gameObject);
    }
    
    ...
    
    if (input.GetButton("Crouch")) {
        // The Crouch button is being pressed.
    }
    
Changing Key Binds At Runtime
=============================
UNSIM's biggest strength is the ability to change key binds at runtime, unlike Unity's default input class which requires the entire game be restarted for a change to take effect.  Here's how you do it in code, on the fly--no restart required (see the [Unity Script Reference](http://docs.unity3d.com/ScriptReference/KeyCode.html) for a list of valid KeyCodes):

    BindProfile profile = inputManager.getActiveProfile();
    profile.SetButtonBind("Crouch", KeyCode.LeftControl); // for buttons--uses Unity's KeyCode enum
    profile.SetAxisBind("Strafe", "Horizontal"); // Uses Unity's built-in axis definitions
    
That's all it takes!  No restarts, no code changes, simple as that.

Using Multiple Profiles
=======================

If you wish to have multiple profiles--say you have one tester who prefers to use a gamepad, but another tester who likes to use a keyboard and mouse--you can set them up like so:

1. In the Inspector for the InputManager script, set the **Size** value under **Profiles** to be however many profiles you want there to be.  By default, it's set to 1--however, you can adjust this to 2 or 3 or however many you need.
2. Under each profile, labeled by **Element 0,** **Element 1,** etc. set your key binds and axis binds as defined in the above text (see [Using UNSIM](#using-unsim)).
3. To switch which profile is being used, set the **Active Profile Index** in the InputManager's Inspector to be the Element value of the profile you want.  (For example, to use the first profile, set it to 0, for the second profile, set it to 1, etc.)

You can also switch profiles from code:

    inputManager.activeProfileIndex = 1;
    
That's it!
