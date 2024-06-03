using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cloud, ground, cat;
    List<GameObject> clouds = new List<GameObject> ();

    float score;
    float screenHeightHalf, screenWidthHalf;
    float cloudHeightInterval;

    Vector3 cloudPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        // 화면 사이즈
        screenHeightHalf = Camera.main.orthographicSize;
        screenWidthHalf = screenHeightHalf * Camera.main.aspect;

        cloudHeightInterval = screenHeightHalf * (1f / 2f);

        SetCatPosition(-screenWidthHalf * (3f / 4f), -screenHeightHalf * (3f / 4f));

        cloudPos.y = ground.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (cloudPos.y <= cat.transform.position.y + screenHeightHalf * 2)
        {
            SpawnCloud();
            DestroyCloud();
        }
    }

    void SpawnCloud()
    {
        // 구름 생성 위치
        cloudPos.x = Random.Range(-(screenWidthHalf / 2f), screenWidthHalf / 2f);
        cloudPos.y += cloudHeightInterval;

        clouds.Add(Instantiate(cloud, cloudPos, transform.rotation));
    }

    void DestroyCloud()
    {
        for (int i = 0; i <clouds.Count; i++)
        {
            if(Camera.main.WorldToScreenPoint(cloud.transform.position).y < 0f)
            {
                Destroy(clouds[i]);
                clouds.RemoveAt(i);
                break;
            }
        }
    }

    void SetCatPosition(float x, float y)
    {
        Vector3 pos = new Vector3 (x, y, 0);
        cat.transform.position = pos;
    }
}
