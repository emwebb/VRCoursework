using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecord {
    public enum LegalInput { Strafe = 0, Forward = 1, Jump = 2, Pause = 3, Reset = 4, MouseX = 5, MouseY = 6 }
    public LegalInput input;
    public float state;
    public float time;

    public InputRecord(LegalInput input, float state) {
        this.input = input;
        this.state = state;
        this.time = Time.time;
    }

    public static InputRecord Strafe(float state) {
        return new InputRecord(LegalInput.Strafe, state);
    } 
    public static InputRecord Forward(float state) {
        return new InputRecord(LegalInput.Forward, state);
    } 
    public static InputRecord Jump(float state) {
        return new InputRecord(LegalInput.Jump, state);
    } 
    public static InputRecord Pause(float state) {
        return new InputRecord(LegalInput.Pause, state);
    } 
    public static InputRecord Reset(float state) {
        return new InputRecord(LegalInput.Reset, state);
    } 

    public static InputRecord MouseX(float state) {
        return new InputRecord(LegalInput.MouseX, state);
    }

    public static InputRecord MouseY(float state) {
        return new InputRecord(LegalInput.MouseY, state);
    }

}
