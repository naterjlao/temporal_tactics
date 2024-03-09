Shader "Custom/GradientMapY_World"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        [Space(25)]
        _MinY ("MinY", Float) = 0
        _MaxY ("MaxY", Float) = 0
        _YOffset("YOffset", Float) = 0

        [Space(25)]
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("_Cull", Float) = 2
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull [_Cull]
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off // Disable Z-write for transparency
        ZTest LEqual // Set Z-test to less or equal for transparency

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
                float4 vertex : SV_POSITION;
                float3 wPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
                UNITY_FOG_COORDS(3) // Define fog coordinates
            };

            sampler2D _MainTex;
            float _MinY;
            float _MaxY;
            float _YOffset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex); // Transfer fog data
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed u = (i.wPos.y - _MinY + _YOffset) / (_MaxY - _MinY);
                u = saturate(u);
                // Posterize
                fixed4 col = tex2D(_MainTex, fixed2(u, 0.5));
                UNITY_APPLY_FOG(i.fogCoord, col); // Apply fog

                return col;
            }
            ENDCG
        }
    }
}
