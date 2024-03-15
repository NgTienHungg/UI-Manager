using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Base.Singleton;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
//using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Debug = UnityEngine.Debug;

namespace Base.UI.Panel
{
    public class PanelManager : Singleton<PanelManager>
    {
        [ShowInInspector]
        private readonly List<Panel> _stackPanels = new List<Panel>();


        public T GetPanel<T>(string panelName) where T : Panel
        {
            return (T)_stackPanels.Find(panel => panel.PanelName == panelName);
        }


        public Type CurrentPanelType => _stackPanels.Count > 0
            ? _stackPanels.Last().GetType()
            : null;


        protected override void OnAwake()
        {
            //// check back button in Android by UniRX
            //Observable.EveryUpdate()
            //    .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            //    .Subscribe(_ => TryCloseCurrentPanel())
            //    .AddTo(this);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TryCloseCurrentPanel();
        }


        public async UniTask<Panel> CreatePanel<T>(string panelName, bool canBack, Action<T> onSetup = null, bool autoOpen = true) where T : Panel
        {
            // create panel async
            var startFrameCount = Time.frameCount;
            var stopwatch = Stopwatch.StartNew();
            var panel = (await Addressables.InstantiateAsync(panelName, transform)).GetComponent<T>();
            stopwatch.Stop();

            // setup panel
            Debug.Log($"[PanelManager] Created {panelName.Color("lime")} in {stopwatch.ElapsedMilliseconds}ms (frame: {Time.frameCount - startFrameCount})");
            panel.Init(panelName, canBack);
            _stackPanels.Add(panel);

            // invoke setup callback
            onSetup?.Invoke(panel);

            // play open animation
            if (autoOpen) panel.Open();

            return panel;
        }


        public async UniTask OpenPanel(string panelName, bool waitOpenCompleted = false)
        {
            // find panel of type by name
            var panel = _stackPanels.Find(panel => panel.PanelName == panelName);
            if (panel == null)
            {
                Debug.LogWarning("[PanelManager] Cannot find panel " + panelName.Color("red"));
                return;
            }

            // play open animation
            panel.Open();

            // wait until close completed
            if (waitOpenCompleted)
                await UniTask.Delay(TimeSpan.FromSeconds(panel.openAnimationDuration));
        }


        public async UniTask ClosePanel(string panelName, bool immediately = false, bool waitCloseCompleted = false)
        {
            // find panel of type by name
            var panel = _stackPanels.Find(panel => panel.PanelName == panelName);
            if (panel == null)
            {
                Debug.LogWarning("[PanelManager] Cannot find panel " + panelName.Color("red"));
                return;
            }

            // play close animation (if not immediately)
            if (immediately)
                panel.CloseImmediately();
            else
                panel.Close();

            // wait until close completed
            if (waitCloseCompleted)
                await UniTask.WaitUntil(() => panel == null);
        }


        public void ReleasePanel(Panel panelClosed)
        {
            Debug.Log("[PanelManager] Released " + panelClosed.PanelName.Color("green"));
            _stackPanels.Remove(panelClosed);
        }


        private void TryCloseCurrentPanel()
        {
            if (_stackPanels.Count == 0)
            {
                Debug.LogWarning("[PanelManager] Stack is empty");
                return;
            }

            if (!_stackPanels.Last().CanBack)
            {
                Debug.LogWarning("[PanelManager] Cannot back");
                return;
            }

            Debug.Log("[PanelManager] Close " + CurrentPanelType.Name.Color("cyan"));
            var panelInTop = _stackPanels.Last();
            _stackPanels.Remove(panelInTop);
            panelInTop.OnCloseButton();
        }
    }
}