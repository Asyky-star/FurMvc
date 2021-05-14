using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItem 
{
    

    public int id { get; set; }

    public string name { get; set; }

    public int state { get; set; }

    public string rooming { get; set; }
    public RoomItem(int id, string name, int state, string rooming="")
    {
        this.id = id;
        this.name = name;
        this.state = state;
        this.rooming = rooming;
    }

    public override string ToString()
    {
        return id + "号房间\n" + name + "\n" + resultState();
    }

    private string resultState()
    {
        if(state.Equals(0))
            return "没有人";
        return "忙碌中";
    }
}
