Shader "Hidden/PixelatedBlit"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            fixed4 frag(v2f_img i) : SV_Target
            {
                // Slightly shift the UVs by half a texel to better align with the screen pixels
                float2 uv = i.uv - _MainTex_TexelSize.xy * 0.5;
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
