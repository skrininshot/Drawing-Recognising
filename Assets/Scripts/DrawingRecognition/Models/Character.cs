using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawingRecognition.Models
{
/*
Character Class - Stores a bitmap and a name to classify to the bitmap
*/

    [Serializable]
    public class Character
    {
        public Bitmap bitmap;

        public string name;

        // Basic Constructor 
        public Character(Bitmap bitmap, string name)
        {
            this.bitmap = bitmap;
            this.name = name;
        }

    }
}
