using OrderSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomView : MonoBehaviour
{
    public UnityAction ReviseRook = null;
    public UnityAction<RoomItem> SeverRoom = null;

    private Transform parent = null;
    private ObjectPool<RoomItemView> objectPool = null;
    private List<RoomItemView> rooms = new List<RoomItemView>();
    private void Awake()
    {
        parent = this.transform.Find("Content");
        //var 匿名类型
        var prefab = Resources.Load<GameObject>("Prefabs/UI/RoomItem");
        objectPool = new ObjectPool<RoomItemView>(prefab, "RoomPool");

    }

   //刷新显示
   public void UpdateRoom(IList<RoomItem> rooms)
    {
        for (int i = 0; i < this.rooms.Count; i++)
        {
            objectPool.Push(this.rooms[i]);
        }
        this.rooms.AddRange(objectPool.Pop(rooms.Count));
        ResfrshRoom(rooms);
    }
    public void ResfrshRoom(IList<RoomItem> rooms)
    {
        for (int i = 0; i < this.rooms.Count; i++)
        {
            this.rooms[i].transform.SetParent(parent);
            var item = rooms[i];
            this.rooms[i].transform.Find("Id").GetComponent<Text>().text = item.ToString();
            Color color = this.rooms[i].GetComponent<Image>().color;
            if (item.state.Equals(0))
            {
                color = Color.green;
            }
            else if (item.state.Equals(1))
            {
                color = Color.yellow;
                StartCoroutine(Rooming(rooms[i]));
             
            }
            else if (item.state.Equals(2))
            {
                color = Color.red;

            }
            this.rooms[i].GetComponent<Image>().color = color;
        }
    }
    /// <summary>
    /// 主店完成 需要付款
    /// </summary>
    /// <param name="item"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Rooming(RoomItem item,float time=3)
    {
        item.state =-1;
        yield return new WaitForSeconds(time);
       
        item.state = 0;

        SeverRoom.Invoke(item);
    }
}
