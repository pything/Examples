using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Technologies.Recorder.Extensions.FCIntegration.MP4
{
    [ExecuteInEditMode]
    public class MP4RecorderSettings : BaseFCRecorderSettings
    {
        public UTJ.FrameCapturer.Scripts.Encoder.fcAPI.fcMP4Config m_MP4EncoderSettings = UTJ.FrameCapturer.Scripts.Encoder.fcAPI.fcMP4Config.default_value;
        public bool m_AutoSelectBR;

        MP4RecorderSettings()
        {
            this.m_BaseFileName.pattern = "movie.<ext>";
            this.m_AutoSelectBR = true;
        }

        public override List<Framework.Core.Engine.RecorderInputSetting> GetDefaultInputSettings()
        {
            return new List<Framework.Core.Engine.RecorderInputSetting>()
            {
                this.NewInputSettingsObj<Framework.Inputs.CBRenderTexture.Engine.CBRenderTextureInputSettings>("Pixels") 
            };
        }

        public override Framework.Core.Engine.RecorderInputSetting NewInputSettingsObj(Type type, string title )
        {
            var obj = base.NewInputSettingsObj(type, title);
            if (type == typeof(Framework.Inputs.CBRenderTexture.Engine.CBRenderTextureInputSettings))
            {
                var settings = (Framework.Inputs.CBRenderTexture.Engine.CBRenderTextureInputSettings)obj;
                settings.m_ForceEvenSize = true;
                settings.m_FlipFinalOutput = true;
            }

            return obj ;
        }

        public override bool isPlatformSupported
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
            }
        }

        public override bool SelfAdjustSettings()
        {
            if (this.inputsSettings.Count == 0 )
                return false;

            var adjusted = false;

            if (this.inputsSettings[0] is Framework.Core.Engine.ImageInputSettings)
            {
                var iis = (Framework.Core.Engine.ImageInputSettings)this.inputsSettings[0];
                if (iis.maxSupportedSize != Framework.Core.Engine.EImageDimension.x2160p_4K)
                {
                    iis.maxSupportedSize = Framework.Core.Engine.EImageDimension.x2160p_4K;
                    adjusted = true;
                }
            }
            return adjusted;
        }

    }
}