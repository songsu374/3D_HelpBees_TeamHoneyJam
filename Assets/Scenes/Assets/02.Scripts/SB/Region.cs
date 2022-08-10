using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Cell정보를 지정
[System.Serializable]
public struct CellInfo
{
    public int arrIndex;
    public Vector3 centerPos;
}

// 실제 맵의 크기를 참고하여 행의 개수와 열의 개수만큼 일정한 크기로 
// 셀(격자)를 생성하도록 제작
[System.Serializable]
public class Region
{
    public Dictionary<int, CellInfo> CellList =
                new Dictionary<int, CellInfo>();

    public float xSize;   // 맵의 x와
    public float zSize;   // z 실제 크기

    public int iRow;      // 행의 개수
    public int iColumn;   // 열의 개수

    float Cellxsize;      // 한개의 cell크기
    float Cellzsize;

    float xStartPos;
    float zStartPos;

    public ushort iMobCount = 0;    // 배치할 몬스커 개수 지정
    public int[] byRandomSeed;    // Random으로 뽑아낼 숫자 0부터 24까지 배열

    public void Init(Transform terrainTrs)
    {
        // Cell의 x와 z크기를 구한다.
        // 실제 맵크기기를 컬럼과 로우로 나누어 한개의 셀 크기를 구한다.
        Cellxsize = xSize / iColumn;
        Cellzsize = zSize / iRow;

        xStartPos = terrainTrs.position.x - xSize * 0.5f;
        zStartPos = terrainTrs.position.z + zSize * 0.5f;

        // 가로개수와 세로 개수와의 번호 배열 
        byRandomSeed = new int[iColumn * iRow];

        Vector3 tmp = Vector3.zero;
        for (int i = 0; i < (iColumn * iRow); i++)
        {
            int nR = i / iColumn;
            int nC = i % iColumn;

            // 셀의 위치를 구한다.
            Vector3 pos = Vector3.zero;
            pos.x = xStartPos + Cellxsize * nC + Cellxsize * 0.5f;
            pos.y = 1f;
            pos.z = zStartPos - Cellzsize * nR - Cellzsize * 0.5f;

            // 위치를 셀 리스트에 저장
            CellInfo stCell = new CellInfo();
            stCell.arrIndex = i;
            stCell.centerPos = pos;
            CellList.Add(i, stCell);

            //            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //            obj.transform.position = pos;

            // 배열에 0부터 24까지의 숫자를 저장
            // 배열안의 값은 셀(타일)의 번호를 의미한다.
            byRandomSeed[i] = i;
        }

        SuffleRandomSeed();
        //        SpawnBymobCount();

    }

    void SuffleRandomSeed()
    {
        // 전체 타일(Cell) 개수
        int tileCount = iRow * iColumn;

        int loopCount = tileCount * 3;

        // Swap알고리즘을 사용하여 배열의 두개의 값을 교환
        // 이의미는 25개 배열에 있는 값을 두수를 교환하면서 값을 섞는 의미이다.
        // 반복문을 사용하여 배열에서 두개의 수를 가져와서 섞게 된다면
        // 0부터 24까지의 배열안의 숫자는 섞이게 된다. 
        for (int i = 0; i < loopCount; i++)
        {
            // 배열에서 두개의 배열 인덱스(방번호)를 랜덤으로 가져와서
            // 두배열 방의 값을 교환한다.
            int R1 = Random.Range(0, tileCount);
            int R2 = Random.Range(0, tileCount);

            // 두수 swap(교체) 알고리즘
            // 하나의 수를 임시 변수에 저장
            int tmp = byRandomSeed[R1];
            // 임시변수에 하나의 수를 저장했으므로 새로운 수로 교체할 수 있다.
            byRandomSeed[R1] = byRandomSeed[R2];
            // 임시변수에 저장된 값을 두번째 수에 저장
            byRandomSeed[R2] = tmp;
        }

    }

    void SpawnBymobCount()
    {
        for (int i = 0; i < iMobCount; i++)
        {
            int iArrIndex = byRandomSeed[i];
            Vector3 vGenPos = CellList[iArrIndex].centerPos;
            GameObject tmpObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            tmpObj.name = "Monster";
            tmpObj.tag = "Monster";
            tmpObj.transform.position = vGenPos;
        }
    }
}

