using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Cell������ ����
[System.Serializable]
public struct CellInfo
{
    public int arrIndex;
    public Vector3 centerPos;
}

// ���� ���� ũ�⸦ �����Ͽ� ���� ������ ���� ������ŭ ������ ũ��� 
// ��(����)�� �����ϵ��� ����
[System.Serializable]
public class Region
{
    public Dictionary<int, CellInfo> CellList =
                new Dictionary<int, CellInfo>();

    public float xSize;   // ���� x��
    public float zSize;   // z ���� ũ��

    public int iRow;      // ���� ����
    public int iColumn;   // ���� ����

    float Cellxsize;      // �Ѱ��� cellũ��
    float Cellzsize;

    float xStartPos;
    float zStartPos;

    public ushort iMobCount = 0;    // ��ġ�� ��Ŀ ���� ����
    public int[] byRandomSeed;    // Random���� �̾Ƴ� ���� 0���� 24���� �迭

    public void Init(Transform terrainTrs)
    {
        // Cell�� x�� zũ�⸦ ���Ѵ�.
        // ���� ��ũ��⸦ �÷��� �ο�� ������ �Ѱ��� �� ũ�⸦ ���Ѵ�.
        Cellxsize = xSize / iColumn;
        Cellzsize = zSize / iRow;

        xStartPos = terrainTrs.position.x - xSize * 0.5f;
        zStartPos = terrainTrs.position.z + zSize * 0.5f;

        // ���ΰ����� ���� �������� ��ȣ �迭 
        byRandomSeed = new int[iColumn * iRow];

        Vector3 tmp = Vector3.zero;
        for (int i = 0; i < (iColumn * iRow); i++)
        {
            int nR = i / iColumn;
            int nC = i % iColumn;

            // ���� ��ġ�� ���Ѵ�.
            Vector3 pos = Vector3.zero;
            pos.x = xStartPos + Cellxsize * nC + Cellxsize * 0.5f;
            pos.y = 1f;
            pos.z = zStartPos - Cellzsize * nR - Cellzsize * 0.5f;

            // ��ġ�� �� ����Ʈ�� ����
            CellInfo stCell = new CellInfo();
            stCell.arrIndex = i;
            stCell.centerPos = pos;
            CellList.Add(i, stCell);

            //            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //            obj.transform.position = pos;

            // �迭�� 0���� 24������ ���ڸ� ����
            // �迭���� ���� ��(Ÿ��)�� ��ȣ�� �ǹ��Ѵ�.
            byRandomSeed[i] = i;
        }

        SuffleRandomSeed();
        //        SpawnBymobCount();

    }

    void SuffleRandomSeed()
    {
        // ��ü Ÿ��(Cell) ����
        int tileCount = iRow * iColumn;

        int loopCount = tileCount * 3;

        // Swap�˰����� ����Ͽ� �迭�� �ΰ��� ���� ��ȯ
        // ���ǹ̴� 25�� �迭�� �ִ� ���� �μ��� ��ȯ�ϸ鼭 ���� ���� �ǹ��̴�.
        // �ݺ����� ����Ͽ� �迭���� �ΰ��� ���� �����ͼ� ���� �ȴٸ�
        // 0���� 24������ �迭���� ���ڴ� ���̰� �ȴ�. 
        for (int i = 0; i < loopCount; i++)
        {
            // �迭���� �ΰ��� �迭 �ε���(���ȣ)�� �������� �����ͼ�
            // �ι迭 ���� ���� ��ȯ�Ѵ�.
            int R1 = Random.Range(0, tileCount);
            int R2 = Random.Range(0, tileCount);

            // �μ� swap(��ü) �˰���
            // �ϳ��� ���� �ӽ� ������ ����
            int tmp = byRandomSeed[R1];
            // �ӽú����� �ϳ��� ���� ���������Ƿ� ���ο� ���� ��ü�� �� �ִ�.
            byRandomSeed[R1] = byRandomSeed[R2];
            // �ӽú����� ����� ���� �ι�° ���� ����
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

