using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using Random = UnityEngine.Random;
using Utils;

public class Stage : MonoBehaviour
{
    [SerializeField] private int colBrick, rowBrick;
    [SerializeField] private Transform startPos;
    [SerializeField] private Vector3 offset;
    

    private int maxBrick;
    private int maxCharacter;
    private List<ColorType> listColors = new List<ColorType>();
    private bool HasEmptyPoint => listEmptyPoint.Count > 0;
    private List<Vector3> listEmptyPoint = new List<Vector3>();
    private List<PlatformBrick> listBricks = new List<PlatformBrick>();

    public void Awake()
    {
        maxBrick = colBrick * rowBrick;
    }

    public void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        GetEmptyPoints();
        SpawnAllBrick();
        
    }
    
    private void SpawnAllBrick()
    {
        foreach (ColorType colorType in listColors)
        {
            SpawnBrickByColor(colorType);
        }
    }

    private void GetEmptyPoints()
    {
        for (int i = 0; i < colBrick; i++)
        {
            for (int j = 0; j < rowBrick; j++)
            {
                Vector3 pos = new Vector3(startPos.position.x + offset.x * j, startPos.position.y, startPos.position.z - offset.z * i);
                listEmptyPoint.Add(pos);
            }
        }
    }

    private void SpawnBrickRandPos(ColorType colorType)
    {
        if (HasEmptyPoint)
        {
            int randomIndex = Random.Range(0, listEmptyPoint.Count);
            Vector3 pos = listEmptyPoint[randomIndex];
            PlatformBrick brick = SimplePool.Spawn<PlatformBrick>(
                PoolType.PlatformBrick, 
                pos, 
                BrickUtils.ROTATION
            );
            brick.OnDespawnEvent += OnDespawnBrickEvent;
            brick.transform.SetParent(transform);
            brick.ChangeColor(colorType);
            
            listEmptyPoint.Remove(pos);
            listBricks.Add(brick);
        }
    }

    private ColorType GetRandomColor()
    {
        while (true)
        {
            ColorType colorType = listColors[Random.Range(0, listColors.Count)];
            if (!MaxBrickByColor(colorType))
            {
                return colorType;
            }
            if (MaxAllBrick())
            {
                return ColorType.None;
            }
        }
    }
    
    private int GetCountBrickByColor(ColorType colorType)
    {
        int count = 0;
        foreach (var brick in listBricks)
        {
            if (brick.color == colorType)
            {
                count++;
            }
        }
        return count;
    }
    
    private bool MaxBrickByColor(ColorType colorType)
    {
        return GetCountBrickByColor(colorType) > maxBrick / maxCharacter + 1;
    }
    
    private bool MaxAllBrick()
    {
        foreach (ColorType colorType in listColors)
        {
            if (!MaxBrickByColor(colorType))
            {
                return false;
            }
        }
        return true;
    }
    
    private void SpawnBrickByColor(ColorType colorType)
    {
        while(!MaxBrickByColor(colorType) && HasEmptyPoint)
        {
            SpawnBrickRandPos(colorType);
        }
    }

    private void OnDespawnBrickEvent(PlatformBrick brick)
    {
        listBricks.Remove(brick);
        brick.OnDespawnEvent -= OnDespawnBrickEvent;
        StartCoroutine(SpawnNextBrick(brick));
    }
    
    private IEnumerator SpawnNextBrick(PlatformBrick brick)
    {
        Vector3 pos = brick.transform.position;
        yield return new WaitForSeconds(Constants.RESPAWN_BRICK_TIME);
        listEmptyPoint.Add(pos);
        SpawnBrickRandPos(GetRandomColor());
    }
    
    public List<Vector3> GetListPosBrickTakeable(ColorType color)
    {
        List<Vector3> listPos = new List<Vector3>();
          
        foreach (var brick in listBricks)
        {
            if (brick.color == color)
            {
                listPos.Add(brick.transform.position);
            }
        }

        return listPos;
    }
    
    public void SetListColor(List<ColorType> listColors)
    {
        this.listColors = listColors;
    }

    public void OnCharacterEnter(Character character)
    {
        listColors.Add(character.color);
        SpawnBrickByColor(character.color);
    }

    public void SetMaxCharacter(int maxCharacter)
    {
        this.maxCharacter = maxCharacter;
    }
}
