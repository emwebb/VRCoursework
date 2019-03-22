using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModel {

	public float strafe;
	public float forward;
	public float jump;
	public float pause;
	public float reset;

	public float mouseX;

	public float mouseY;

	public InputModel() {
		strafe = 0;
		forward = 0;
		jump = 0;
		pause = 0;
		reset = 0;
		mouseX = 0;
		mouseY = 0;
	}

	public bool set(InputRecord.LegalInput input, float value) {
		switch (input) {
			case InputRecord.LegalInput.Strafe:
				return setStrafe(value);
			case InputRecord.LegalInput.Forward:
				return setForward(value);
			case InputRecord.LegalInput.Jump:
				return setJump(value);
			case InputRecord.LegalInput.Pause:
				return setPause(value);
			case InputRecord.LegalInput.Reset:
				return setReset(value);
			case InputRecord.LegalInput.MouseX:
				return setMouseX(value);
			case InputRecord.LegalInput.MouseY:
				return setMouseY(value);
		}
		return false;
	}

	public bool setStrafe(float value) {
		value = Mathf.Round(value);
		if (value != strafe) {
			strafe = value;
			return true;
		} else {
			return false;
		}
	}
	
	public bool setForward(float value) {
		value = Mathf.Round(value);
		if (value != forward) {
			forward = value;
			return true;
		} else {
			return false;
		}
	}

	public bool setJump(float value) {
		value = Mathf.Round(value);
		if (value != jump) {
			jump = value;
			return true;
		} else {
			return false;
		}
	}
	
	public bool setPause(float value) {
		value = Mathf.Round(value);
		if (value != pause) {
			pause = value;
			return true;
		} else {
			return false;
		}
	}
	
	public bool setReset(float value) {
		value = Mathf.Round(value);
		if (value != reset) {
			reset = value;
			return true;
		} else {
			return false;
		}
	}
	
	public bool setMouseX(float value) {
		if (value != mouseX) {
			mouseX = value;
			return true;
		} else {
			return false;
		}
	}

	public bool setMouseY(float value) {
		if (value != mouseY) {
			mouseY = value;
			return true;
		} else {
			return false;
		}
	}
}
