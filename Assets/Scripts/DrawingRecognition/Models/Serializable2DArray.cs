using System.Collections.Generic;
using UnityEngine;

namespace DrawingRecognition.Models
{
    [System.Serializable]
    public class Serializable2DArray
    {
        [SerializeField] private List<SerializableFloatList> data = new();

        public void Initialize(int rows, int columns)
        {
            data.Clear();

            for (int i = 0; i < rows; i++)
            {
                var row = new SerializableFloatList();

                for (int j = 0; j < columns; j++)
                {
                    row.values.Add(0f);
                }

                data.Add(row);
            }
        }

        public float[] this[int row]
        {
            get
            {
                var result = new float[data[row].values.Count];

                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = data[row].values[i];
                }

                return result;
            }
        }

        public float[][] ToJaggedArray()
        {
            var array = new float[data.Count][];

            for (int i = 0; i < data.Count; i++)
            {
                array[i] = new float[data[i].values.Count];

                for (int j = 0; j < data[i].values.Count; j++)
                {
                    array[i][j] = data[i].values[j];
                }
            }

            return array;
        }

        public static implicit operator float[][](Serializable2DArray array)
        {
            return array.ToJaggedArray();
        }
    }
}