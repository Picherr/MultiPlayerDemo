using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRestoreObjects : MonoBehaviour
{
    public GameObject[] Objs;
    // public Transform[] spawnPos;
    public Dictionary<int, GameObject> Indexs = new Dictionary<int, GameObject>();

    public SpawnRestoreRequest spawnRestoreRequest;

    // Start is called before the first frame update
    void Start()
    {
        /*Bloods[0] = Resources.Load("Prefabs/Restore/Stew") as GameObject;
        Bloods[1] = Resources.Load("Prefabs/Restore/Ham") as GameObject;
        Blues[0] = Resources.Load("Prefabs/Restore/PotionSmall") as GameObject;
        Blues[1] = Resources.Load("Prefabs/Restore/PotionBig") as GameObject;*/

        int i;
        for(i = 0; i < Objs.Length; i++)
        {
            Indexs.Add(i, Objs[i]);
        }

        SpawnThings();
    }

    private void SpawnThings()
    {
        MainPack pack = new MainPack();
        RestoreBeingsPack restoreBeingsPack = new RestoreBeingsPack();

        // 将药品的信息录入到发送服务端的包中
        for(int i = 0; i < Objs.Length; i++)
        {
            restoreBeingsPack.Index = i;
            restoreBeingsPack.Restoretype = Objs[i].gameObject.GetComponent<RestoreItem>().restoreType;
            pack.Restorebeingspack.Add(restoreBeingsPack);
        }
        spawnRestoreRequest.SendRequest(pack);
    }
}
