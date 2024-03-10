Shader "Unlit/GradientY"
{
    Properties
    {
        _ColorTop ("Top Color", Color) = (1,1,1,1)
        _ColorBottom ("Bottom Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _ColorTop;
            float4 _ColorBottom;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.y * 0.5 + 0.5; // Map Y position to UV space [0, 1]
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Interpolate color between top and bottom based on UV coordinate
                fixed4 topColor = _ColorTop;
                fixed4 bottomColor = _ColorBottom;
                
                // Adjust transparency based on top color alpha
                float topAlpha = topColor.a;
                if (topAlpha <= 0.0)
                    discard; // Fully transparent

                // Interpolate colors
                fixed4 finalColor = lerp(bottomColor, topColor, i.uv.y);

                // Apply transparency
                finalColor.a *= lerp(bottomColor.a, topAlpha, i.uv.y);

                return finalColor;
            }
            ENDCG
        }
    }
}
