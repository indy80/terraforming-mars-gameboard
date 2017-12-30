using System;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public UndoManager Instance => this.instance;

    private UndoManager instance;

    private Stack<UndoableCommand> commands = new Stack<UndoableCommand>();

    public void Awake()
    {
        instance = this;
    }
    
    public void Do(UndoableCommand command)
    {
        command.Do();
        this.commands.Push(command);
    }

    public void Undo()
    {
        var command = this.commands.Pop();
        command.Undo();
    }
}

public class UndoableCommand
{
    private readonly Action action;
    private readonly Action undoAction;

    public UndoableCommand(Action action, Action undoAction)
    {
        this.action = action;
        this.undoAction = undoAction;
    }

    public void Do()
    {
        this.action();
    }

    public void Undo()
    {
        this.undoAction();
    }
}


