using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// The <c>InputController</c> is in charge of managing inputs to the program
/// and properly applying the configured input delay.
public class InputController : MonoBehaviour {
	/// The player, from which the movement script is taken.
	public Transform player;

	/// The camera, from which the camera look script is taken.
	public Transform playerCamera;

	/// The generator, from which the map generation script is taken.
	public Transform platformGenerator;

	/// A text area, into which the current delay is displayed.
	public UnityEngine.UI.Text textArea;

    /// The Sleep Deprivation Controller
    public SleepDeprivationController sleepDeprivationController;

	/// Whether or not to apply mouse lag.
	public bool mouseLag = true;

	/// The current lag applied in seconds.
	public float lag = 0;

	/// The model of currently pressed keys.
	private InputModel inputModel;

	/// The model of which keys were pressed <c>lag</c> seconds ago.
	private InputModel laggedInputModel;

	/// The queue of input deltas to be applied as time passes.
	private Queue<InputRecord> inputLagQueue;

	/// Whether or not the game is paused.
	private bool pauseState;

	/// The script which moves the player.
	private PlayerMove playerMoveScript;

	/// The script which controls the cameras look angle.
	private MouseLook mouseLookScript;

	/// The script which controls platform generation.
	private LoadPlatforms loadPlatforms;

	/// Initializes the Input controller. Instantiating it's fields.
	void Start () {
		inputModel = new InputModel();
		laggedInputModel = new InputModel();
		inputLagQueue = new Queue<InputRecord>();
		playerMoveScript = player.gameObject.GetComponent<PlayerMove>();
		mouseLookScript = playerCamera.gameObject.GetComponent<MouseLook>();
		loadPlatforms = platformGenerator.gameObject
			.GetComponent<LoadPlatforms>();
        sleepDeprivationController = player.GetComponent<SleepDeprivationController>();
		pauseState = false;
		playerMoveScript.Unpause();
		mouseLookScript.Unpause();
	}
	
	/// Called once per frame to update the models and call methods based on
	/// input.
	void Update () {
		// Acquire all the inputs.
		float strafe = Input.GetAxis("Strafe");
		float forward = Input.GetAxis("Forward");
		float jump = Input.GetAxis("Jump");
		float pause = Input.GetAxis("Pause");
		float reset = Input.GetAxis("Reset");
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		float delay = Mathf.Round(Input.GetAxis("Delay"));

		// Update the delay and reset the game if <c>delay</c> is pressed.
		if (inputModel.setDelay(delay)) {
            // Do not Enqueue, no requirement to delay delay alterations.
            sleepDeprivationController.sleepDeprivationHours += (int) delay * 3;
			textArea.text = "Lag: ";
			reset = 1;
		}
		// Update and queue lag for all controls.
		if (inputModel.setStrafe(strafe)) {
			inputLagQueue.Enqueue(InputRecord.Strafe(strafe));
		}
		if (inputModel.setForward(forward)) {
			inputLagQueue.Enqueue(InputRecord.Forward(forward));
		}
		if (inputModel.setJump(jump)) {
			inputLagQueue.Enqueue(InputRecord.Jump(jump));
		}
		// Pause and reset do not require queuing.
		inputModel.setPause(pause);
		inputModel.setReset(reset);
		
		// If mouse lag is enabled Set and Queue the mouse movements.
		if (mouseLag) {
			if (inputModel.setMouseX(mouseX)) {
				inputLagQueue.Enqueue(InputRecord.MouseX(mouseX));
			}
			if (inputModel.setMouseY(mouseY)) {
				inputLagQueue.Enqueue(InputRecord.MouseY(mouseY));
			}
		}
		// If the game has not been won, and the game is not paused, show the
		// current lag.
		if (!textArea.text.StartsWith("WINNER WINNER") && !pauseState) {
			textArea.text = string.Format("Lag: {0:0.00}", lag);
		}
		// Consume the delay queue up to the current time adjusted for lag.
		while (inputLagQueue.Count > 0) {
			if (inputLagQueue.Peek().time + lag > Time.time) {
				break;
			}
			InputRecord input = inputLagQueue.Dequeue();
			laggedInputModel.set(input.input, input.state);
		}
		// If the game is to be paused, signal other components.
		if (inputModel.pause > 0) {
			pauseState = true;
			playerMoveScript.Pause();
			mouseLookScript.Pause();
		}
		// If the game is to be resumed, signal other components.
		if (inputModel.pause < 0) {
			pauseState = false;
			playerMoveScript.Unpause();
			mouseLookScript.Unpause();
		}
		// If the game is to be reset, trigger the reset.
		if (inputModel.reset > 0) {
			playerMoveScript.Reload();
			mouseLookScript.Reload();
			// note: this does not reload the arena, it simply sets the camera
			// to look back at the win location.
			loadPlatforms.Reload();
			inputLagQueue.Clear();
		}
		
		// If the game is not paused, update all movement.
		if (!pauseState) {
			playerMoveScript.Move(
				laggedInputModel.forward, 
				laggedInputModel.strafe, 
				laggedInputModel.jump
			);
			if (mouseLag) {
				mouseLookScript.Look(
					laggedInputModel.mouseX, 
					laggedInputModel.mouseY
				);
			} else {
				mouseLookScript.Look(mouseX, mouseY);
			}
		}

	}

}
