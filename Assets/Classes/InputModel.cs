using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Represents all of the expected input keys for processing. Used to decode the
/// delta model stored in <c>InputRecord</c>s
public class InputModel {

	/// Represents the users strafing movement keys.
	public float strafe;
	
	/// Represents forwards and backwards movement keys.
	public float forward;

	/// Represents the jump key being held or not.
	public float jump;

	/// Represents the pause key and the click used to unpause.
	public float pause;

	/// Represents the reset key.
	public float reset;

	/// Represents the mouse X position.
	public float mouseX;

	/// Represents the mouse Y position.
	public float mouseY;


	/// Represents the delay adjustment keys.
	public float delay;

	/// Creates a 'no input' initialized <c>InputModel</c>
	public InputModel() {
		strafe = 0;
		forward = 0;
		jump = 0;
		pause = 0;
		reset = 0;
		mouseX = 0;
		mouseY = 0;
		delay = 0;
	}

	/// Sets a value in the <c>InputModel<c> based on the <c>LegalInput</c> from
	/// a <c>InputRecord</c>.
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
			case InputRecord.LegalInput.Delay:
				return setDelay(value);
		}
		return false;
	}

	/// Sets the strafe in the input controller, rounding <c>value</c>.
	public bool setStrafe(float value) {
		value = Mathf.Round(value);
		if (value != strafe) {
			strafe = value;
			return true;
		} else {
			return false;
		}
	}

	/// Sets the forward in the input controller, rounding <c>value</c>.
	public bool setForward(float value) {
		value = Mathf.Round(value);
		if (value != forward) {
			forward = value;
			return true;
		} else {
			return false;
		}
	}

	/// Sets the jump in the input controller, rounding <c>value</c>.
	public bool setJump(float value) {
		value = Mathf.Round(value);
		if (value != jump) {
			jump = value;
			return true;
		} else {
			return false;
		}
	}
	
	/// Sets the pause in the input controller, rounding <c>value</c>.
	public bool setPause(float value) {
		value = Mathf.Round(value);
		if (value != pause) {
			pause = value;
			return true;
		} else {
			return false;
		}
	}
	
	/// Sets the reset in the input controller, rounding <c>value</c>.
	public bool setReset(float value) {
		value = Mathf.Round(value);
		if (value != reset) {
			reset = value;
			return true;
		} else {
			return false;
		}
	}
	
	/// Sets the mouse x in the input controller, rounding <c>value</c>.
	public bool setMouseX(float value) {
		if (value != mouseX) {
			mouseX = value;
			return true;
		} else {
			return false;
		}
	}

	/// Sets the mouse y in the input controller, rounding <c>value</c>.
	public bool setMouseY(float value) {
		if (value != mouseY) {
			mouseY = value;
			return true;
		} else {
			return false;
		}
	}

	/// Sets the delay in the input controller, rounding <c>value</c>.
	public bool setDelay(float value) {
		value = Mathf.Round(value);
		if (value != delay) {
			delay = value;
			return true;
		} else {
			return false;
		}
	}
}
