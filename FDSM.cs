
using System.Collections.Generic;
public class FDSM
{
    //string inputAlphabet;
    State startState;
    Dictionary<string, State> states = new Dictionary<string, State>();
    public FDSM(/*string _inputAlphabet*/)
    {
        //Parsing arguments
        //inputAlphabet = _inputAlphabet;

        //Initializing

    }

    public void SetStart(string _name, StateType _stateType)
    {
        State _state = new State(_name, _stateType, this);
        _state.name = _name;
        states.Add(_state.name, _state);
        startState = _state;
    }
    
    public void AddTransition(string _state0, string _state1, char _input)
    {
        State state0;
        State state1;
        states.TryGetValue(_state0, out state0);
        states.TryGetValue(_state1, out state1);
        state0.AddTransition(_input, state1);
    }

    public State InternalValidateInput(string _input)
    {
        //bool validInput;
        //Check if input has valid chars
        //foreach(char c in _input)
        //{
        //    foreach(char y in inputAlphabet)
        //    {
        //        if(c == y) validInput = true;
        //    }
        //}

        State currentState = startState;
        foreach(char c in _input)
        {
            currentState = currentState.Traverse(c);
            if(currentState == null) return null;
        }
        return currentState;
    }

    public bool ValidateInput(string _input)
    {
        State currentState = InternalValidateInput(_input);
        if(currentState == null) return false;
        return currentState.stateType.Equals(StateType.ACCEPTED);
    }

    public string ValidateInputGetName(string _input)
    {
        State currentState = InternalValidateInput(_input);
        if(currentState == null) return null;
        return currentState.name;
    }

    public void NewState(string _input, StateType _stateType)
    {
        //Create a new State and add it to states
        states.Add(_input, new State(_input, _stateType, this));
    }
}