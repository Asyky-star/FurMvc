using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using OrderSystem;

public class RoomProxy : Proxy
{
    public new const string NAME = "RoomProxy";//名字
   
    public IList<RoomItem> Rooms //存储房间数据的集合
    {
        get { return (IList<RoomItem>)base.Data; } //从父级获取
    }

    public RoomProxy():base(NAME,new List<RoomItem>())
    {
        AddRoom(new RoomItem(1, "花好月圆", 0));
        AddRoom(new RoomItem(2, "百年好合", 0));
        AddRoom(new RoomItem(3, "憬栾殿", 0));
    }

    public void AddRoom(RoomItem item) //往集合中添加东西
    {
        Rooms.Add(item);
    }
    /// <summary>
    /// 从集合中删除某个信息
    /// </summary>
    /// <param name="item"></param>
    public void RemoveRoom(RoomItem item)
    {
        Rooms.Remove(item);
    }
    /// <summary>
    /// 随机入住房间
    /// </summary>
   public void Rooming()
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            if(Rooms[i].state==0)//找到没有人改变其状态
            {
                Rooms[i].state++;
                SendNotification(OrderSystemEvent.ReshRooms);
                return;
            }
        }
    }
}
