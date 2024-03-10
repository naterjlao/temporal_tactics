Shader "Custom/GradientMapY_ObjectSpace"
{
    Properties
    {
        [MainTexture] _MainTex ("_MainTex", 2D) = "white" {}

        [Space(25)]
        _MinY ("MinY", Float) = 0
        _MaxY ("MaxY", Float) = 1
        _YOffset("YOffset", Float) = 0
        [MainColor] _Color ("Color", Color) = (1,1,1,1)


        [Space(25)]
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Culling", Float) = 2
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull [_Cull]
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ZTest LEqual

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float _MinY;
            float _MaxY;
            float _YOffset;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = float2(v.vertex.y - _MinY + _YOffset, 0.0) / (_MaxY - _MinY);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed u = saturate(i.uv.x);
                fixed4 col = tex2D(_MainTex, float2(u, i.uv.y));

                col.rgb *= _Color.rgb;
                col.a *= _Color.a;

                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
