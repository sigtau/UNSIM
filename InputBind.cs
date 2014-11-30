using UnityEngine;
using System.Collections;

// Wrapper class for Unity's key bind system that allows for switching
// input binds on the fly.

// UNSIM (Unity Non-Touch Simple Input Manager)
// Copyright (c) 2015, Will Preston.
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
// 1. Redistributions of source code must retain the above copyright notice, this
// list of conditions and the following disclaimer.
// 
// 2. Redistributions in binary form must reproduce the above copyright notice, this
// list of conditions and the following disclaimer in the documentation and/or other
// materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
// DAMAGE.
public sealed class InputBind {
	
	// The default BindType
	private BindType type = BindType.BUTTON;
	
	// The button used for this bind, if one exists.
	private KeyCode button = KeyCode.None;
	
	// The axis used for this bind, if one exists.
	private string axis = null;
	
	// The formal name of the bind ("Run" or "Jump" etc.)
	private string handle = null;
	
	// Whether or not this keybind is able to accept input.
	private bool enabled = true;
	
	// Constructor for button-based binds.
	public InputBind(BindType type, KeyCode button, string handle) {
		this.type = type;
		this.button = button;
		this.handle = handle;
	}
	
	// Constructor for axis-based binds.
	public InputBind(BindType type, string axis, string handle) {
		this.type = type;
		this.axis = axis;
		this.handle = handle;
	}
	
	// Allows you to change the key bind at runtime.  Does nothing if this is an axis bind.
	public void setButton(KeyCode button) {
		if (type != BindType.BUTTON) { return; }
		this.button = button;
	}
	
	// Allows you to change the axis bind at runtime.  Does nothing if this is a button bind.
	public void setAxis(string axis) {
		if (type == BindType.BUTTON) { return; }
		this.axis = axis;
	}
	
	// Returns the handle of this bind.
	public string getHandle() {
		return handle;
	}
	
	// Returns the KeyCode of this bind (if one is set).
	public KeyCode getButton() {
		return button;
	}
	
	// Returns this bind's type.
	public BindType getType() {
		return type;
	}
	
	// Enables or disables this bind.
	public void setEnabled(bool e) {
		enabled = e;
	}
	
	// Returns whether or not this bind is enabled.
	public bool isEnabled() {
		return enabled;
	}
	
	// Returns the axis of this bind (if one is set).
	public string getAxisName() {
		return axis;
	}
	
	// Polls to see if the button is pressed for this frame.
	public bool isPressed() {
		if (button == KeyCode.None || type != BindType.BUTTON || !enabled) { return false; }
		return Input.GetKey(button);
	}
	
	// Returns true on the first frame that the button is pressed, but not again until the button is released and pressed again.
	public bool isPressedDown() {
		if (button == KeyCode.None || type != BindType.BUTTON || !enabled) { return false; }
		return Input.GetKeyDown(button);
	}
	
	// Returns true on the first frame that a button is released after having been pressed.
	public bool isReleased() {
		if (button == KeyCode.None || type != BindType.BUTTON || !enabled) { return false; }
		return Input.GetKeyUp(button);
	}
	
	
	// Returns the value of the specified axis for this frame.
	public float getAxisValue() {
		if (type == BindType.BUTTON || string.IsNullOrEmpty(axis) || !enabled) { return 0f; }
		try {
			return Input.GetAxis (axis);
		} catch (UnityException) {
			return 0f;
		}
	}
	
	// Returns the raw value of the specified axis for this frame.
	public float getAxisRawValue() {
		if (type == BindType.BUTTON || string.IsNullOrEmpty(axis) || !enabled) { return 0f; }
		try {
			return Input.GetAxisRaw (axis);
		} catch (UnityException) {
			return 0f;
		}
	}
	
}
