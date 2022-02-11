Shader "My/UV"
{
    Properties
    {
    }
    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv *= 50;
                i.uv = frac(i.uv);

                if (i.uv.x > 0.1f && i.uv.y > 0.1f) discard;

                fixed4 col = float4(i.uv, 0, 1);
                return col;
            }
            ENDCG
        }
    }
}
