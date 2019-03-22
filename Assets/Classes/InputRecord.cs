using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Models an input record delta, tracking the changes in value of a particular
/// input.
public class InputRecord {

    /// Each legal input is assigned a value for reference.
    public enum LegalInput { 
        Strafe = 0, Forward = 1, Jump = 2, Pause = 3, 
        Reset = 4, MouseX = 5, MouseY = 6, Delay = 7 
    }

    /// The input being stored.
    public LegalInput input;
    
    /// The state the input was changed to.
    public float state;

    /// The time at which the input change occured.
    public float time;

    /// Creates a new InputRecord from it's fields. Time is generated as the
    /// current Unity time since program start.
    public InputRecord(LegalInput input, float state) {
        this.input = input;
        this.state = state;
        this.time = Time.time;
    }

    /// Creates a strafe record from it's axis.
    public static InputRecord Strafe(float state) {
        return new InputRecord(LegalInput.Strafe, state);
    } 

    /// Creates a forward record from it's axis.
    public static InputRecord Forward(float state) {
        return new InputRecord(LegalInput.Forward, state);
    } 

    /// Creates a jump record from it's axis.
    public static InputRecord Jump(float state) {
        return new InputRecord(LegalInput.Jump, state);
    }
    
    /// Creates a pause record from it's axis.
    public static InputRecord Pause(float state) {
        return new InputRecord(LegalInput.Pause, state);
    } 

    /// Creates a rest record from it's axis.
    public static InputRecord Reset(float state) {
        return new InputRecord(LegalInput.Reset, state);
    } 
    
    /// Creates a MouseX record from it's axis.
    public static InputRecord MouseX(float state) {
        return new InputRecord(LegalInput.MouseX, state);
    }
    
    /// Creates a MouseY record from it's axis.
    public static InputRecord MouseY(float state) {
        return new InputRecord(LegalInput.MouseY, state);
    }

    
    /// Creates a delay record from it's axis.
    public static InputRecord Delay(float state) {
        return new InputRecord(LegalInput.Delay, state);
    }

}
