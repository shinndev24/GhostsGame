Shader "Custom/HaloFade"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Radius ("Radius", Float) = 4
        _Softness ("Softness", Float) = 1.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
                float3 worldPos : TEXCOORD1; 
            };


            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float4 _PlayerPos;
            float _Radius;
            float _Softness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                float d = distance(i.worldPos.xz, _PlayerPos.xz);
                float fade = 1.0 - smoothstep(_Radius - _Softness, _Radius, d);

                col.a *= fade;
                return col;
            }
            ENDCG
        }
    }
}
