Shader "Custom/Shader_baru"
{
    Properties
    {
        [MainColor] _Warna ("warna", Color) = (255, 161, 247, 1)
        
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "Unlit2d"
            Tags { "LightMode" = "Universal2D" }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            CBUFFER_START(UnityPerMaterial)
                float4 _Warna;
            CBUFFER_END

            struct Attributes
            {
                float4 positionObjek : POSITION;
            };

            struct keFragment
            {
                float4 posisiClip : SV_POSITION;
            };


            keFragment vert(Attributes IN)
            {
                keFragment OUT;
                OUT.posisiClip = TransformObjectToHClip(IN.positionObjek.xyz);
                return OUT;
            }

            half4 frag(keFragment IN) : SV_Target
            {
                return (half4)_Warna;
            }
            ENDHLSL
        }
    }
}
