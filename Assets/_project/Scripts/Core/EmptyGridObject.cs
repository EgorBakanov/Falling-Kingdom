﻿using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    public class EmptyGridObject : IGridObject
    {
        public GridObjectType Type => GridObjectType.None;
        public void OnTileHeightChanged(int newHeight, int oldHeight)
        {
            Debug.Log($"{oldHeight} => {newHeight}");
        }

        public void OnTileFall()
        {
            Debug.Log("Fall");
        }
    }
}