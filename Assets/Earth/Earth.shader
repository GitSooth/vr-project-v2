Shader "My/Earth"
{
    Properties
    {
        _Day    ("Texture Day", 2D) = "white" {}
        _Night  ("Texture Night", 2D) = "white" {}
        _Spec   ("Texture Specular", 2D) = "white" {}
        _Cloud  ("Texture Clouds", 2D) = "white" {}
        _Range  ("Range", float) = 0.5
        _Shininess("Shininess", int) = 128
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
                float3 normal : NORMAL;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv     : TEXCOORD0;
                float3 ldir   : TEXCOORD1;
                float3 world  : TEXCOORD2;
            };

            uniform sampler2D _Day;
            uniform sampler2D _Night;
            uniform sampler2D _Spec;
            uniform sampler2D _Cloud;
            uniform float _Range;
            uniform int _Shininess;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = normalize(mul(float4(v.normal, 0), unity_WorldToObject).xyz);
                o.uv = v.uv;
                o.ldir = normalize(_WorldSpaceLightPos0.xyz);
                o.world = mul(UNITY_MATRIX_M, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.normal = normalize(i.normal);
                float3 eye = normalize(_WorldSpaceCameraPos.xyz - i.world);
                float2 muv = i.uv + float2(.5, 0) /** _Time*/;

                float3 h = normalize(i.ldir + eye);
                float ndoth = max(0.0, dot(i.normal, h));

                float ndotl = dot(i.normal, i.ldir);
                float4 colorDay = tex2D(_Day, muv);
                float4 colorNight = tex2D(_Night, muv);

                float t = smoothstep(-_Range, _Range, ndotl);
                float4 color = lerp(colorNight, colorDay, t); //(1-t)*a + (t)*b

                float isSpec = tex2D(_Spec, muv).r;
                float4 specular = float4(1,1,1,1) * pow(ndoth, _Shininess) * isSpec;

                float2 cuv = i.uv + float2(1, 0) * _Time * 0.2f;
                float isClouds = tex2D(_Cloud, cuv).r;
                float4 clouds = lerp(color, float4(1,1,1,1), isClouds * max(t, 0.1));

                return clouds + specular;
            }
            ENDCG
        }
    }
}
