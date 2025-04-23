using System.Text;
using DefaultNamespace;
using game_core;
using Game.Core.Network;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Protobuf;
using TMPro;
using ui.frame;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoginView : ViewBase
    {
        [Binding] Button _btnPlay { get; set; }
        [Binding] Button _btnSave { get; set; }
        
        protected override void onShow()
        {
            base.onShow();
            onClick(_btnPlay, onClickPlay);
            onClick(_btnSave, onClickSave);
        }
        
        private void onClickPlay()
        {
            Locator.fetch<GameMain>().switchState(GameStateEnum.GAME);
        }
        
        private void onClickSave()
        {
            Debug.Log("OnClickSave");
        }
    }
}