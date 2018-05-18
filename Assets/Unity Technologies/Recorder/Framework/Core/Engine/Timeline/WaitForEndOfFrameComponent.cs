using System;
using System.Collections;

namespace Unity_Technologies.Recorder.Framework.Core.Engine.Timeline
{

    /// <summary>
    /// What is it: Signals RecorderPlayableBehaviour when frame is reached.
    /// Motivation: RecorderPlayableBehaviour does does not have a way to detect the end of frame, 
    ///               which is when the RecordFrame signal must be given to the Recording session. Using this component,
    ///               we can use MonoBehaviour coroutines to inform the Recorder of the end of frame.
    /// 
    /// Notes: 
    ///     - There is a 1-to-1 instance count between RecorderPlayableBehaviour and WaitForEndOfFrameComponent
    ///     - This component get's added to a transient GameObject, that lives in the scene in play mode and that is associated
    ///       to the recording session.
    /// </summary>    
    [UnityEngine.ExecuteInEditMode]
    class WaitForEndOfFrameComponent : UnityEngine.MonoBehaviour
    {
        [NonSerialized]
        public RecorderPlayableBehaviour m_playable;

        public IEnumerator WaitForEndOfFrame()
        {
            yield return new UnityEngine.WaitForEndOfFrame();
            if(this.m_playable != null)
                this.m_playable.FrameEnded();
        }

        public void LateUpdate()
        {
            this.StartCoroutine(this.WaitForEndOfFrame());
        }
    }
}