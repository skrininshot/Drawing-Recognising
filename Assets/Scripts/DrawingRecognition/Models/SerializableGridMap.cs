using System.Collections.Generic;
using UnityEngine;

namespace DrawingRecognition.Models
{
    [System.Serializable]
    public class SerializableGridMap
    {
        [SerializeField] private List<SerializableFloatList> data = new();
        [SerializeField] private int width = 0;
        [SerializeField] private int height = 0;

        public SerializableGridMap(int width, int height)
        {
            Initialize(width, height);
        }

        public void Initialize(int width, int height)
        {
            this.width = width;
            this.height = height;

            data.Clear();

            for (int y = 0; y < height; y++)
            {
                var row = new SerializableFloatList();

                for (int x = 0; x < width; x++)
                {
                    row.values.Add(0f);
                }

                data.Add(row);
            }
        }

        public float this[int x, int y]
        {
            get => data[y].values[x];
            set => data[y].values[x] = value;
        }

        public float[,] To2DArray()
        {
            var array = new float[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    array[x, y] = data[y].values[x];
                }
            }

            return array;
        }

        public static implicit operator float[,](SerializableGridMap grid)
        {
            return grid.To2DArray();
        }
    }
}