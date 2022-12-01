using System.Collections.Generic;
using UnityEngine;

namespace Map.Island
{
    public class Island
    {
        private int id;
        private int width;
        private int height;
        private int waterPercent;

        private Dictionary<int, Vector2> groundTilesDictionary;
        private Dictionary<int, Vector2> waterTilesDictionary;

    }
}