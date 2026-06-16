using UnityEngine;
using RaiNa.IO;

namespace RaiNa.Unity.Tilemaps
{
    public readonly struct Grid
    {
        public readonly int Width;
        public readonly int Height;

        public readonly Vector3 OriginPosition;

        public readonly float CellSize;
        public readonly float CellPadding;

        public int Length => Width * Height;

        public Grid(int width, int height, Vector3 originPos, float cellSize = 1f, float cellPadding = 0f)
        {
            Width = Mathf.Max(1, width);
            Height = Mathf.Max(1, height);

            OriginPosition = originPos != null ? originPos : Vector3.zero;

            CellSize = Mathf.Max(0.01f, cellSize);
            CellPadding = Mathf.Max(0f, cellPadding);
        }

        public int GetIndexFrom(int x, int y) => x + (y * Width);

        public int GetIndexFrom(Vector3 worldPos)
        {
            Vector3 localPos = worldPos - OriginPosition;

            int x = Mathf.FloorToInt(localPos.x / GetStride());
            int y = Mathf.FloorToInt(localPos.y / GetStride());

            return GetIndexFrom(x, y);
        }

        public bool IsValidIndex(int index) => index >= 0 && index < Length;

        public bool IsInBounds(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
        public bool IsInBounds(Vector3 worldPos)
        {
            Vector3 localPos = worldPos - OriginPosition;

            int x = Mathf.FloorToInt(localPos.x / GetStride());
            int y = Mathf.FloorToInt(localPos.y / GetStride());

            return IsInBounds(x, y);
        }

        public Vector2Int GetGridPositionFrom(int index)
        {
            int x = index % Width;
            int y = index / Width;

            return new Vector2Int(x, y);
        }

        public Vector2Int GetGridPositionFrom(Vector3 worldPos)
        {
            Vector3 localPos = worldPos - OriginPosition;

            int x = Mathf.FloorToInt(localPos.x / GetStride());
            int y = Mathf.FloorToInt(localPos.z / GetStride());

            return new Vector2Int(x, y);
        }

        public Vector3 GetWorldPositionFrom(int index)
        {
            Vector2Int position = GetGridPositionFrom(index);

            float stride = GetStride();

            return OriginPosition + new Vector3(position.x * stride, position.y * stride, 0f);
        }

        public Vector3 GetWorldPositionFrom(int x, int y)
        {
            float stride = GetStride();

            return OriginPosition + new Vector3(x * stride, y * stride, 0f);
        }

        private float GetStride() => CellSize + CellPadding;
    }
}
