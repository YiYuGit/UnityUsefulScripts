using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : CommandManager.ICommand
{
    private Vector3Int m_From;
    private Vector3Int m_To;

    public MoveCommand(Vector3Int start, Vector3Int end) 
    {
        m_From = start;
        m_To = end;
    }
    
    public void Execute()
    {
        var unit = Gameboard.Instance.GetUnit(m_From);
        if (unit != null)
        {
            Gameboard.Instance.MoveUnit(unit, m_To);
            Gameboard.Instance.SwitchTeam();
        }
    }

    public void Undo()
    {
        var unit = Gameboard.Instance.GetUnit(m_To);
        if (unit != null)
        {
            Gameboard.Instance.MoveUnit(unit, m_From);
        }

        //we switch back which team is playing, as we undid a move, it go back to previous team
        Gameboard.Instance.SwitchTeam();
    }
}
