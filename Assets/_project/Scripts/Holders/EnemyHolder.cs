﻿using System.Collections;
using DG.Tweening;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Holders
{
    public class EnemyHolder : GridObjectHolder<Enemy>, IPointerEnterHandler, IPointerExitHandler
    {
        protected override void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        protected override void OnTileFall()
        {
            UnsubscribeOnTile(TileHolder.Tile);
            GameManager.Instance.EnemyManager.DestroyEnemy(this);
        }

        public IEnumerator Move(TileHolder tile, float jumpPower, float duration)
        {
            TileHolder = tile;
            yield return transform.DOJump(GetPlacementPosition(tile), jumpPower, 1, duration).WaitForCompletion();
            GridObject.Move(tile.Tile);
        }

        public IEnumerator Attack(TileHolder tile, Vector3 offset, float duration, float power, float elasticity,
            int vibrato)
        {
            GameManager.Instance.AudioManager.PlayEnemyAttack();
            yield return transform.DOPunchPosition((GetPlacementPosition(tile) + offset - transform.position) * power,
                duration, vibrato, elasticity).WaitForCompletion();
            tile.Tile.Height--;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!GameManager.Instance.SelectionManager.WaitForPlayerSelection)
                return;

            var target = TileHolder.GridHolder.TileHolders[GridObject.MoveIntention.Index];
            GameManager.Instance.SelectionManager.AddToEnemyTarget(target.gameObject);
            GameManager.Instance.UiManager.ShowDescriptor(GridObject.Preset.Name,UiManager.DescriptorTag.Enemy,GridObject.Preset.Description);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!GameManager.Instance.SelectionManager.WaitForPlayerSelection)
                return;

            var target = TileHolder.GridHolder.TileHolders[GridObject.MoveIntention.Index];
            GameManager.Instance.SelectionManager.RemoveFromEnemyTarget(target.gameObject);
            GameManager.Instance.UiManager.HideDescriptor();
        }

        public IEnumerator TurnTo(TileHolder targetHolder, float duration)
        {
            yield return visualRoot.DOLookAt(targetHolder.transform.position,duration,AxisConstraint.Y).WaitForCompletion();
        }
    }
}