using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	public Transform player;
	public Transform camera;

	public bool mouseLag = false;

	public float lag = 0;
	private InputModel inputModel;
	private InputModel laggedInputModel;
	private Queue<InputRecord> inputLagQueue;
	private bool pauseState;
	private PlayerMove playerMoveScript;

	private MouseLook mouseLookScript;

	void Start () {
		inputModel = new InputModel();
		laggedInputModel = new InputModel();
		inputLagQueue = new Queue<InputRecord>();
		playerMoveScript = player.gameObject.GetComponent<PlayerMove>();
		mouseLookScript = camera.gameObject.GetComponent<MouseLook>();
		pauseState = false;
		playerMoveScript.Unpause();
		mouseLookScript.Unpause();
	}
	
	// Update is called once per frame
	void Update () {
		float strafe = Input.GetAxis("Strafe");
		float forward = Input.GetAxis("Forward");
		float jump = Input.GetAxis("Jump");
		float pause = Input.GetAxis("Pause");
		float reset = Input.GetAxis("Reset");
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		if (inputModel.setStrafe(strafe)) {
			inputLagQueue.Enqueue(InputRecord.Strafe(strafe));
		}
		if (inputModel.setForward(forward)) {
			inputLagQueue.Enqueue(InputRecord.Forward(forward));
		}
		if (inputModel.setJump(jump)) {
			inputLagQueue.Enqueue(InputRecord.Jump(jump));
		}
		if (inputModel.setPause(pause)) {
			inputLagQueue.Enqueue(InputRecord.Pause(pause));
		}
		if (inputModel.setReset(reset)) {
			inputLagQueue.Enqueue(InputRecord.Reset(reset));
		}
		if (mouseLag) {
			if (inputModel.setMouseX(mouseX)) {
				inputLagQueue.Enqueue(InputRecord.MouseX(mouseX));
			}
			if (inputModel.setMouseY(mouseY)) {
				inputLagQueue.Enqueue(InputRecord.MouseY(mouseY));
			}
		}
		while (inputLagQueue.Count > 0) {
			if (inputLagQueue.Peek().time + lag > Time.time) {
				break;
			}
			InputRecord input = inputLagQueue.Dequeue();
			laggedInputModel.set(input.input, input.state);
		}

		if (laggedInputModel.pause > 0) {
			pauseState = true;
			playerMoveScript.Pause();
			mouseLookScript.Pause();
		}
		if (laggedInputModel.pause < 0) {
			pauseState = false;
			playerMoveScript.Unpause();
			mouseLookScript.Unpause();
		}
		
		if (!pauseState) {
			playerMoveScript.Move(laggedInputModel.forward, laggedInputModel.strafe, laggedInputModel.jump);
			if (mouseLag) {
				mouseLookScript.Look(laggedInputModel.mouseX, laggedInputModel.mouseY);
			} else {
				mouseLookScript.Look(mouseX, mouseY);
			}
		}

	}
}
