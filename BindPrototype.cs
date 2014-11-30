using UnityEngine;

// A simple data container for (what will eventually become) an InputBind,
// as determined by a BindProfile.

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
public sealed class BindPrototype {
	
	// The type of bind this input bind will be.
	public BindType type = BindType.BUTTON;
	public string handle = "Button";
	public string axisName = "";
	public KeyCode keyCode = KeyCode.None;
	
	public BindPrototype(BindType type, string handle, string axisName) {
		this.type = type;
		this.handle = handle;
		this.axisName = axisName;
	}
	
	public BindPrototype(BindType type, string handle, KeyCode keyCode) {
		this.type = type;
		this.handle = handle;
		this.keyCode = keyCode;
	}
	
}
