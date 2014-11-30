using UnityEngine;
using System.Collections.Generic;

// A profile system wherein the user can set keybinds to a profile
// that can be swapped in and out automatically at runtime, allowing users to,
// for instance, plug in a controller and use that instead after the game has
// started.

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
[System.Serializable]
public sealed class BindProfile {
	// All of the binds in this profile
	private List<InputBind> binds = new List<InputBind>();
	
	// All of the requested binds in this profile.
	public List<BindPrototype> prototypes = new List<BindPrototype>();
	
	// The name of this profile.
	public string profileName = "Default Profile Name";
	
	// Whether this bind is active or not.
	public bool enabled = true;
	
	// Called as soon as the scene starts--I recommend it be called from
	// Awake() instead of Start() because this guarantees input will be ready
	// before ANYTHING in the scene is instantiated!
	public void initialize() {
		foreach (BindPrototype prototype in prototypes) {
			convertPrototypeToBind(prototype);
		}
	}
	
	// Called every frame to verify the key binds are still valid.
	// Warning: While the overhead of iterating through a list of controls once every 
	public void updateBinds() {
		if (!enabled) { return; }
		
		// Make sure the prototype list and the actual list match.
		// For performance reasons, this block only executes when the prototype list is found to be different in size
		// from the actual bind list, so we aren't iterating (twice) over two different lists every frame.
		// Quadruple the overhead if we don't perform this check.
		if (binds.Count != prototypes.Count) {
			Debug.LogWarning ("Detected discrepancy in the input prototype and bind caches.  Syncing cache...");
			
			// Check for binds without corresponding prototype, and remove binds if the prototype doesn't exist.
			foreach (InputBind bind in binds) {
				bool foundHandle = false;
				foreach (BindPrototype prototype in prototypes) {
					if (prototype.handle.Equals(bind.getHandle())) {
						foundHandle = true;
					}
				}
				
				if (!foundHandle) {
					Debug.LogWarning ("Input bind '" + bind.getHandle() + "' does not have a corresponding entry in the bind prototypes for this profile.  Removing from bind list.");
					binds.Remove (bind);
				}
			}
			
			// Check for prototypes without corresponding binds, and add binds if they don't exist.
			foreach (BindPrototype prototype in prototypes) {
				bool foundBind = false;
				foreach (InputBind bind in binds) {
					if (bind.getHandle().Equals(prototype.handle)) {
						foundBind = true;
					}
				}
				
				if (!foundBind) {
					Debug.LogWarning ("Creating new input bind to correspond with bind prototype '" + prototype.handle + "'...");
					convertPrototypeToBind(prototype);
				}
			}
		}
	}
	
	// Used internally to take data stored in a BindPrototype and turn it into an actual key/button/axis bind.
	private void convertPrototypeToBind(BindPrototype prototype) {
		if (prototype.type == BindType.BUTTON) {
			binds.Add (new InputBind(prototype.type, prototype.keyCode, prototype.handle));
		} else {
			binds.Add (new InputBind(prototype.type, prototype.axisName, prototype.handle));
		}
	}
	
	// Returns true if the requested bind type is an axis.
	// Useful for cases where you might bind a shoulder button with variable values (which Unity treats as an axis)
	// to something that a mouse button or keyboard button could easily do, such as the right trigger on an XBox controller
	// aiming down the sights of a gun vs. right click doing the same.  Either case would require polling the bind with
	// two different functions (GetButton vs GetAxis), so this function lets you check before polling the bind.
	public bool isAxis(string handle) {
		InputBind bind = null;
		foreach (InputBind b in binds) {
			if (b.getHandle().Equals (handle)) {
				bind = b;
				break;
			}
		}
		
		if (bind == null || !enabled) {
			Debug.LogWarning ("Attempted to check isAxis() for bind '" + handle + "' which does not exist in the current bind profile.  Ignoring.");
			return false;
		}
		
		return (bind.getType() != BindType.BUTTON);
	}
	
	// Returns true if the requested bind exists.
	// Useful for making sure the user didn't accidentally unbind something important like character movement so the console
	// doesn't get bogged down with warnings.
	
	// Functions exactly the same as Unity's GetButton.
	public bool GetButton(string handle) {
		InputBind bind = null;
		foreach (InputBind b in binds) {
			if (b.getHandle().Equals (handle)) {
				bind = b;
				break;
			}
		}
		
		if (bind == null || !enabled) {
			Debug.LogWarning ("Attempted to poll for bind '" + handle + "' which does not exist in the current bind profile.  Ignoring.");
			return false;
		}
		
		return bind.isPressed();
	}
	
	// Functions exactly the same as Unity's GetButtonDown.
	public bool GetButtonDown(string handle) {
		InputBind bind = null;
		foreach (InputBind b in binds) {
			if (b.getHandle().Equals (handle)) {
				bind = b;
				break;
			}
		}
		
		if (bind == null || !enabled) {
			Debug.LogWarning ("Attempted to poll for bind '" + handle + "' which does not exist in the current bind profile.  Ignoring.");
			return false;
		}
		
		return bind.isPressedDown();
	}
	
	// Functions exactly the same as Unity's GetButtonUp.
	public bool GetButtonUp(string handle) {
		InputBind bind = null;
		foreach (InputBind b in binds) {
			if (b.getHandle().Equals (handle)) {
				bind = b;
				break;
			}
		}
		
		if (bind == null || !enabled) {
			Debug.LogWarning ("Attempted to poll for bind '" + handle + "' which does not exist in the current bind profile.  Ignoring.");
			return false;
		}
		
		return bind.isReleased();
	}
	
	// Functions exactly the same as Unity's GetAxis.
	public float GetAxis(string handle) {
		InputBind bind = null;
		foreach (InputBind b in binds) {
			if (b.getHandle().Equals (handle)) {
				bind = b;
				break;
			}
		}
		
		if (bind == null || !enabled) {
			Debug.LogWarning ("Attempted to poll for bind '" + handle + "' which does not exist in the current bind profile.  Ignoring.");
			return 0f;
		}
		
		return bind.getAxisValue();
	}
	
	// Functions exactly the same as Unity's GetAxisRaw.
	public float GetAxisRaw(string handle) {
		InputBind bind = null;
		foreach (InputBind b in binds) {
			if (b.getHandle().Equals (handle)) {
				bind = b;
				break;
			}
		}
		
		if (bind == null || !enabled) {
			Debug.LogWarning ("Attempted to poll for bind '" + handle + "' which does not exist in the current bind profile.  Ignoring.");
			return 0f;
		}
		
		return bind.getAxisRawValue();
	}
	
}
