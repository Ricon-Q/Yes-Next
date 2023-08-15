using System.Collections.Generic;
using UnityEngine;


public class CharacterTraversal : MonoBehaviour
{
    public List<Transform> points; // 지점들의 리스트
    private Transform targetPoint; // 현재 목표 지점
    private int currentPointIndex = -1; // 현재 목표 지점의 인덱스 (-1로 초기화하여 시작)
    private float moveSpeed = 5f; // 캐릭터 이동 속도
    private bool isWaiting = false; // 지점에 도착했을 때 대기 상태인지 여부를 저장하는 변수
    private float waitTime = 5f; // 대기 시간
    private List<Node> path; // 캐릭터의 경로

    private class Node
    {
        public Transform point;
        public float gScore;
        public float hScore;
        public float fScore => gScore + hScore;
        public Node cameFrom;

        public Node(Transform point, float gScore, float hScore)
        {
            this.point = point;
            this.gScore = gScore;
            this.hScore = hScore;
        }
    }

    private void Start()
    {
        SelectRandomTargetPoint();
    }

    private void Update()
    {
        if (isWaiting)
        {
            // 대기 시간이 지났으면 다음 지점 선택
            if (Time.time >= waitTime)
            {
                isWaiting = false;
                SelectRandomTargetPoint();
            }
        }
        else
        {
            // 현재 캐릭터와 목표 지점 간의 거리 계산
            float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);

            // 목표 지점에 도착했다면 대기 상태로 전환
            if (distanceToTarget < 0.1f)
            {
                isWaiting = true;
                waitTime = Time.time + 5f; // 현재 시간에서 5초를 더한 시간으로 대기 종료 시점 설정
            }

            // 경로가 없으면 새로운 경로 계산
            if (path == null || path.Count == 0)
            {
                path = FindPath(transform.position, targetPoint.position);
            }

            // 캐릭터 이동
            if (path != null && path.Count > 0)
            {
                MoveToTargetPoint();
            }
        }
    }

    private void SelectRandomTargetPoint()
    {
        // 모든 지점을 방문한 경우 다시 처음부터 시작
        if (currentPointIndex == points.Count - 1)
        {
            currentPointIndex = -1;
        }

        // 현재 지점과 다른 랜덤한 지점 선택
        int newPointIndex = currentPointIndex;
        while (newPointIndex == currentPointIndex)
        {
            newPointIndex = Random.Range(0, points.Count);
        }

        currentPointIndex = newPointIndex;
        targetPoint = points[currentPointIndex];
        path = null; // 새로운 목표로 이동하므로 경로 초기화
    }

    private void MoveToTargetPoint()
    {
        // 다음 지점으로 이동
        Vector3 nextPointPosition = path[0].point.position;
        float distanceToNextPoint = Vector3.Distance(transform.position, nextPointPosition);
        transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, moveSpeed * Time.deltaTime);

        // 다음 지점에 도착하면 경로에서 제거
        if (distanceToNextPoint < 0.1f)
        {
            path.RemoveAt(0);
        }

        // 캐릭터의 이동 방향 설정
        Vector3 moveDirection = (nextPointPosition - transform.position).normalized;
        // 2D 이므로 Z 축 이동 없음
        moveDirection.z = 0;
        // 캐릭터 이동 방향으로 캐릭터를 바라보게 설정
        transform.right = moveDirection;
    }

    private List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // A* 알고리즘을 사용하여 startPos에서 targetPos까지의 최적 경로를 계산
        Node startNode = new Node(null, 0, Vector3.Distance(startPos, targetPos));
        Node targetNode = new Node(null, float.MaxValue, 0);
        List<Node> openSet = new List<Node> { startNode };
        HashSet<Node> closedSet = new HashSet<Node>();

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fScore < currentNode.fScore || (openSet[i].fScore == currentNode.fScore && openSet[i].hScore < currentNode.hScore))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.point != null && currentNode.point.position == targetPos)
            {
                // 최적 경로를 찾았을 때, 이를 역으로 추적하여 반환
                List<Node> path = new List<Node>();
                while (currentNode != null)
                {
                    path.Insert(0, currentNode);
                    currentNode = currentNode.cameFrom;
                }
                return path;
            }

            foreach (Transform point in points)
            {
                if (point == null || closedSet.Contains(new Node(point, 0, 0)))
                {
                    continue;
                }

                float distanceToNeighbor = Vector3.Distance(currentNode.point.position, point.position);
                float tentativeGScore = currentNode.gScore + distanceToNeighbor;

                Node neighborNode = openSet.Find(node => node.point == point);
                if (neighborNode == null)
                {
                    neighborNode = new Node(point, float.MaxValue, Vector3.Distance(point.position, targetPos));
                    openSet.Add(neighborNode);
                }
                else if (tentativeGScore >= neighborNode.gScore)
                {
                    continue;
                }

                neighborNode.cameFrom = currentNode;
                neighborNode.gScore = tentativeGScore;
            }
        }

        return null;
    }
}