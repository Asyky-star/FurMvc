using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        RoomProxy roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy; 
     //   Order order = notification.Body as Order;
        roomProxy.Rooming();
    }
}
