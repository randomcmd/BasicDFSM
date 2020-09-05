using System.Collections.Generic;
public class State
{
    FDSM included;
    public StateType stateType; 
    public string name;
    Dictionary<char, State> transitions = new Dictionary<char, State>();
    public State(string _name, StateType _stateType, FDSM _included)
    {
        name = _name;
        stateType = _stateType;
        included = _included;
    }

    public State Traverse(char _input)
    {
        State _return;
        transitions.TryGetValue(_input, out _return);
        return _return;
    }

    public void AddTransition(char _input, State _state)
    {
        transitions.Add(_input,_state);
    }
}