#if true
using UnityEngine;

#endif

namespace Nara.MFGJS2020.Core
{
    public class EmptyGridObject : IGridObject
    {
        public GridObjectType Type => GridObjectType.None;
        public int PathScore => 100;

        public void OnTileHeightChanged(int newHeight, int oldHeight)
        {
#if UNITY_EDITOR
            //Debug.Log($"{oldHeight} => {newHeight}");
#endif
        }

        public void OnTileFall()
        {
#if UNITY_EDITOR
            //Debug.Log("Fall");
#endif
        }
    }
}