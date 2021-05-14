using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System;
using OrderSystem;

public class RoomMediator : Mediator  //mediator 中介
{
    private RoomProxy roomProxy = null;
    public new const string NAME = "RoomMediator";

    public RoomView RoomView
    {
        get { return (RoomView)base.ViewComponent; }
    }

    public RoomMediator(RoomView view) : base(NAME, view)
    {
        RoomView.SeverRoom += item => { SendNotification(OrderSystemEvent.Pay_rooms, item); };//发送消息 该收费了
    }

    public override void OnRegister() //注册
    {
        base.OnRegister();
        roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
        if (null == roomProxy)
            throw new Exception(RoomProxy.NAME + "is null");
        RoomView.UpdateRoom(roomProxy.Rooms);
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(OrderSystemEvent.CALL_Room);
        notifications.Add(OrderSystemEvent.ReshRooms);
        notifications.Add(OrderSystemEvent.Pay_rooms);
        return notifications ;
    }

    public override void HandleNotification(INotification notification)//处理通知
    {
        switch(notification.Name)
        {
            case OrderSystemEvent.CALL_Room:
                //选择一个房间入住
                //--------------------------------
                //自己填的
                SendNotification(OrderSystemEvent.Rooming);
                //--------
                break;
            case OrderSystemEvent.ReshRooms:
                //刷新房间状态
                roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
                Debug.Log("刷新房间的状态");
                if (null == roomProxy)
                    throw new Exception(RoomProxy.NAME + "is null");
                RoomView.ResfrshRoom(roomProxy.Rooms);
                break;
            case OrderSystemEvent.Pay_rooms:
                roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
                Debug.Log("刷新房屋状态");
                if (null == roomProxy)
                    throw new Exception(RoomProxy.NAME + "is null.");
                RoomView.ResfrshRoom(roomProxy.Rooms);
                break;
        }
        base.HandleNotification(notification);

    }
}
