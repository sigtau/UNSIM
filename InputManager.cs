using UnityEngine;

// The master MonoBehaviour class that can be polled for input exactly as one would
// with Unity's default Input class.

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
public sealed class InputManager : MonoBehaviour {
	
	// The index for the below array that determines which profile is currently active.
	public int activeProfileIndex = 0;
	
	// A list of profiles active in this input manager.  This allows you to have
	// more than one profile (kind of like how Halo has multiple settings, i.e.
	// Recon, Hayabusa, etc.)
	public BindProfile[] profiles = new BindProfile[1];
	
	// Called on scene load, before instantiating objects in the scene.
	void Awake() {
		foreach (BindProfile profile in profiles) {
			profile.initialize();
		}
	}
	
	// Called every frame
	void Update() {
		if (validateIndex()) {
			profiles[activeProfileIndex].updateBinds();
		}
	}
	
	// Exactly the same as Unity's Input.GetButton()
	public bool GetButton(string bind) {
		if (validateIndex()) {
			return profiles[activeProfileIndex].GetButton (bind);
		} else {
			return false;
		}
	}
	
	// Exactly the same as Unity's Input.GetButtonDown()
	public bool GetButtonDown(string bind) {
		if (validateIndex()) {
			return profiles[activeProfileIndex].GetButtonDown(bind);
		} else {
			return false;
		}
	}
	
	// Exactly the same as Unity's Input.GetButtonUp()
	public bool GetButtonUp(string bind) {
		if (validateIndex()) {
			return profiles[activeProfileIndex].GetButtonUp(bind);
		} else {
			return false;
		}
	}
	
	// Exactly the same as Unity's Input.GetAxis()
	public float GetAxis(string bind) {
		if (validateIndex()) {
			return profiles[activeProfileIndex].GetAxis (bind);
		} else {
			return 0f;
		}
	}
	
	// Exactly the same as Unity's Input.GetAxisRaw()
	public float GetAxisRaw(string bind) {
		if (validateIndex()) {
			return profiles[activeProfileIndex].GetAxisRaw(bind);
		} else {
			return 0f;
		}
	}
	
	// Prevents IndexOutOfBoundsExceptions from being thrown within this script in case some idiot sets
	// the profile index to be a ridiculous value or in case someone sets the profile array to zero-length.
	private bool validateIndex() {
		return !(activeProfileIndex < 0 || activeProfileIndex >= profiles.Length || profiles.Length == 0);
	}
	
	// Not really necessary, but a nice alias to have when you don't want to have
	// to write out GetComponent every time.
	public static InputManager getInstance(GameObject obj) {
		return obj.GetComponent<InputManager>();
	}
	
	// Returns the active profile.
	public BindProfile getActiveProfile() {
		return validateIndex() ? profiles[activeProfileIndex] : null;
	}
	
	// Returns whether the specified bind is an axis.
	public bool isAxis(string handle) {
		if (validateIndex()) {
			return profiles[activeProfileIndex].isAxis (handle);
		} else {
			return false;
		}
	}
	
}
