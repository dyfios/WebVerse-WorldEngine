using FiveSQD.WebVerse.WorldEngine.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
#if WV_VR_ENABLED
using UnityEngine.XR.Interaction.Toolkit.UI;
#endif
using Vuplex.WebView;

namespace FiveSQD.WebVerse.WorldEngine.Entity
{
    public class HTMLUIElementEntity : UIElementEntity
    {
        /// <summary>
        /// Messaging API setup for JavaScript.
        /// </summary>
        private readonly string messagingAPI =
            "function postWorldMessage(message) {" +
            "  window.vuplex.postMessage(message);" +
            "}";

        /// <summary>
        /// Action to invoke when a world message is received from the HTML entity.
        /// </summary>
        public Action<string> onWorldMessage;

        /// <summary>
        /// WebView Prefab instance.
        /// </summary>
        private CanvasWebViewPrefab canvasWebViewPrefab;

        /// <summary>
        /// Queue of URLs to load.
        /// </summary>
        private Queue<string> urlLoadQueue;

        /// <summary>
        /// Queue of HTML documents to load.
        /// </summary>
        private Queue<string> htmlLoadQueue;

        /// <summary>
        /// Queue of JavaScript execution scripts.
        /// </summary>
        private Queue<Tuple<string, Action<string>>> javascriptExecuteQueue;

        /// <summary>
        /// Initialize this entity. This should only be called once.
        /// </summary>
        /// <param name="idToSet">ID to apply to the entity.</param>
        /// <param name="parentCanvas">Canvas to place the button entity on.</param>
        public override void Initialize(Guid idToSet, CanvasEntity parentCanvas)
        {
            base.Initialize(idToSet, parentCanvas);

            GameObject canvasWebView = Instantiate(WorldEngine.ActiveWorld.entityManager.canvasWebViewPrefab);
            canvasWebView.transform.SetParent(transform);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            canvasWebViewPrefab = canvasWebView.GetComponent<CanvasWebViewPrefab>();
            canvasWebViewPrefab.LogConsoleMessages = true;
            RectTransform rt = canvasWebView.GetComponent<RectTransform>();
            if (rt == null)
            {
                rt = canvasWebView.gameObject.AddComponent<RectTransform>();
            }
            uiElementRectTransform = rt;
            urlLoadQueue = new Queue<string>();
            htmlLoadQueue = new Queue<string>();
            javascriptExecuteQueue = new Queue<Tuple<string, Action<string>>>();

            if (parentCanvas != null)
            {
                SetParent(parentCanvas);
            }

            MakeHidden();

#if WV_VR_ENABLED
            canvasWebView.AddComponent<TrackedDeviceGraphicRaycaster>();
#endif
        }

        public override bool SetSizePercent(Vector2 percent, bool synchronize = true)
        {
            RectTransform rt = uiElementRectTransform;
            if (rt == null)
            {
                LogSystem.LogWarning("[HTMLUIElementEntity->SetSizePercent] No rect transform.");
                return false;
            }

            CanvasEntity parentCanvasEntity = GetParentCanvasEntity();
            if (parentCanvasEntity == null)
            {
                LogSystem.LogError("[HTMLUIElementEntity->SetSizePercent] No parent canvas entity.");
                return false;
            }

            RectTransform parentRT = parentCanvasEntity.GetComponent<RectTransform>();
            if (parentRT == null)
            {
                LogSystem.LogError("[HTMLUIElementEntity->SetSizePercent] No parent canvas entity rect transform.");
                return false;
            }

            Vector2 originalPosition = GetPositionPercent();

            Vector2 worldSize = new Vector2(parentRT.sizeDelta.x * percent.x, parentRT.sizeDelta.y * percent.y);

            rt.sizeDelta = worldSize;
            SetPositionPercent(originalPosition, false);

            return true;
        }

        public override bool SetPositionPercent(Vector2 percent, bool synchronize = true)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;

            RectTransform rt = uiElementRectTransform;
            if (rt == null)
            {
                LogSystem.LogWarning("[HTMLUIElementEntity->SetPositionPercent] No rect transform.");
                return false;
            }

            CanvasEntity parentCanvasEntity = GetParentCanvasEntity();
            if (parentCanvasEntity == null)
            {
                LogSystem.LogError("[HTMLUIElementEntity->SetPositionPercent] No parent canvas entity.");
                return false;
            }

            RectTransform parentRT = parentCanvasEntity.GetComponent<RectTransform>();
            if (parentRT == null)
            {
                LogSystem.LogError("[HTMLUIElementEntity->SetPositionPercent] No parent canvas entity rect transform.");
                return false;
            }
            
            Vector3 worldPos = new Vector3(parentRT.sizeDelta.x * percent.x, -1 * parentRT.sizeDelta.y * percent.y);
            
            rt.anchorMin = rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.localPosition = Vector3.zero;
            rt.anchoredPosition = worldPos;

            return true;
        }

        /// <summary>
        /// Load content from a URL.
        /// </summary>
        /// <param name="url">URL to load content from.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public bool LoadFromURL(string url)
        {
            if (canvasWebViewPrefab == null)
            {
                LogSystem.LogError("[HTMLUIElementEntity->LoadFromURL] Invalid HTML UI Element entity.");
                return false;
            }

            urlLoadQueue.Enqueue(url);

            return true;
        }

        /// <summary>
        /// Load content from a URL.
        /// </summary>
        /// <param name="url">URL to load content from.</param>
        /// <returns>Whether or not the setting was successful.</returns>
        public bool LoadHTML(string html)
        {
            if (canvasWebViewPrefab == null)
            {
                LogSystem.LogError("[HTMLEntity->LoadHTML] Invalid HTML UI Element entity.");
                return false;
            }

            htmlLoadQueue.Enqueue(html);

            return true;
        }

        /// <summary>
        /// Get the current URL.
        /// </summary>
        /// <returns>The current URL, or null.</returns>
        public string GetURL()
        {
            if (canvasWebViewPrefab == null || canvasWebViewPrefab.WebView == null)
            {
                LogSystem.LogError("[HTMLEntity->GetURL] Invalid HTML UI Element entity.");
                return null;
            }

            return canvasWebViewPrefab.WebView.Url;
        }

        /// <summary>
        /// Execute JavaScript logic.
        /// </summary>
        /// <param name="logic">Logic to execute.</param>
        /// <param name="onComplete">Action to invoke upon completion. Provides return
        /// from JavaScript as string.</param>
        /// <returns>Whether or not the operation was successful.</returns>
        public bool ExecuteJavaScript(string logic, Action<string> onComplete)
        {
            if (canvasWebViewPrefab == null)
            {
                LogSystem.LogError("[HTMLEntity->ExecuteJavaScript] Invalid HTML UI Element entity.");
                return false;
            }

            javascriptExecuteQueue.Enqueue(new Tuple<string, Action<string>>(logic, onComplete));

            return false;
        }

        /// <summary>
        /// Set up the messaging API.
        /// </summary>
        private void SetUpMessagingAPI()
        {
            canvasWebViewPrefab.WebView.PageLoadScripts.Add(messagingAPI);
            canvasWebViewPrefab.WebView.MessageEmitted += (sender, e) =>
            {
                if (onWorldMessage != null)
                {
                    onWorldMessage.Invoke(e.Value);
                }
            };
        }

        private void Update()
        {
            if (canvasWebViewPrefab == null || canvasWebViewPrefab.WebView == null)
            {
                return;
            }

            if (urlLoadQueue.Count > 0)
            {
                string urlToLoad = urlLoadQueue.Dequeue();
                SetUpMessagingAPI();
                canvasWebViewPrefab.WebView.LoadUrl(urlToLoad);
            }

            if (htmlLoadQueue.Count > 0)
            {
                string htmlToLoad = htmlLoadQueue.Dequeue();
                SetUpMessagingAPI();
                canvasWebViewPrefab.WebView.LoadHtml(htmlToLoad);
            }

            if (javascriptExecuteQueue.Count > 0)
            {
                Tuple<string, Action<string>> javascriptToExecute = javascriptExecuteQueue.Dequeue();
                SetUpMessagingAPI();
                
                if (javascriptToExecute != null)
                {
                    canvasWebViewPrefab.WebView.ExecuteJavaScript(javascriptToExecute.Item1, javascriptToExecute.Item2);
                }
            }
        }
    }
}